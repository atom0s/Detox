// -- Detox License ------------------------------------------------------
//
//    This file is part of Detox.
//
//    Detox is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    Detox is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with Detox.  If not, see <http://www.gnu.org/licenses/>.
// -----------------------------------------------------------------------

namespace DetoxAPI.Extensions
{
    using Mono.Cecil;
    using Mono.Cecil.Cil;
    using System.Collections.Generic;
    using System.Linq;

    public static class MethodDefinitionExtensions
    {
        /// <summary>
        /// A list of all possible short branching opcodes and their long versions.
        /// </summary>
        private static readonly Dictionary<OpCode, OpCode> BranchMap = new Dictionary<OpCode, OpCode>
        {
            { OpCodes.Beq_S, OpCodes.Beq },
            { OpCodes.Bge_S, OpCodes.Bge },
            { OpCodes.Bge_Un_S, OpCodes.Bge_Un },
            { OpCodes.Bgt_S, OpCodes.Bgt },
            { OpCodes.Bgt_Un_S, OpCodes.Bgt_Un },
            { OpCodes.Ble_S, OpCodes.Ble },
            { OpCodes.Ble_Un_S, OpCodes.Ble_Un },
            { OpCodes.Blt_S, OpCodes.Blt },
            { OpCodes.Blt_Un_S, OpCodes.Blt_Un },
            { OpCodes.Bne_Un_S, OpCodes.Bne_Un },
            { OpCodes.Br_S, OpCodes.Br },
            { OpCodes.Brfalse_S, OpCodes.Brfalse },
            { OpCodes.Brtrue_S, OpCodes.Brtrue },
            { OpCodes.Leave_S, OpCodes.Leave }
        };

        /// <summary>
        /// Resets the methods instruction offsets.
        /// 
        /// Thanks to Mono.Cecil.Rocks for this function.
        /// </summary>
        /// <param name="md"></param>
        public static void ComputeOffsets(this MethodDefinition md)
        {
            var offset = 0;
            foreach (var i in md.Body.Instructions)
            {
                i.Offset = offset;
                offset += i.GetSize();
            }
        }

        /// <summary>
        /// Optimizes a methods branches.
        /// 
        /// Thanks to Mono.Cecil.Rocks for this function.
        /// </summary>
        /// <param name="md"></param>
        public static void OptimizeBranches(this MethodDefinition md)
        {
            foreach (var i in from i in md.Body.Instructions where i.OpCode.OperandType == OperandType.InlineBrTarget let offset = ((Instruction)i.Operand).Offset - (i.Offset + i.OpCode.Size + 4) where offset >= - 128 && offset <= 127 select i)
            {
                i.OpCode = MethodDefinitionExtensions.BranchMap[i.OpCode];
                md.ComputeOffsets();
            }
        }

        /// <summary>
        /// Inserts the given instructions at the desired offset.
        /// </summary>
        /// <param name="md"></param>
        /// <param name="offset"></param>
        /// <param name="instructions"></param>
        public static void Insert(this MethodDefinition md, int offset, params Instruction[] instructions)
        {
            var ilp = md.Body.GetILProcessor();
            var inst = ilp.Body.Instructions[offset];
            foreach (var i in instructions.Reverse())
                ilp.InsertAfter(inst, i);
        }

        /// <summary>
        /// Inserts the given instructions at the desired location.
        /// </summary>
        /// <param name="md"></param>
        /// <param name="location"></param>
        /// <param name="instructions"></param>
        public static void Insert(this MethodDefinition md, InsertLocation location, params Instruction[] instructions)
        {
            if (location == InsertLocation.Start)
                md.InsertStart(instructions);
            else
                md.InsertEnd(instructions);
            md.OptimizeBranches();
        }

        /// <summary>
        /// Inserts the given instructions at the desired location.
        /// </summary>
        /// <param name="md"></param>
        /// <param name="location"></param>
        /// <param name="target"></param>
        /// <param name="instructions"></param>
        public static void Insert(this MethodDefinition md, InsertLocation location, Instruction target, params Instruction[] instructions)
        {
            var ilp = md.Body.GetILProcessor();
            if (location == InsertLocation.Start)
            {
                foreach (var i in instructions)
                    ilp.InsertBefore(target, i);
            }
            else
            {
                foreach (var i in instructions.Reverse())
                    ilp.InsertAfter(target, i);
            }

            md.OptimizeBranches();
        }

        /// <summary>
        /// Inserts the given instructions at the start of the given method.
        /// </summary>
        /// <param name="md"></param>
        /// <param name="instructions"></param>
        public static void InsertStart(this MethodDefinition md, params Instruction[] instructions)
        {
            var ilp = md.Body.GetILProcessor();
            var inst = md.Body.Instructions[0];
            foreach (var i in instructions)
                ilp.InsertBefore(inst, i);
        }

        /// <summary>
        /// Inserts the given instructions at the end of the given method.
        /// </summary>
        /// <param name="md"></param>
        /// <param name="instructions"></param>
        public static void InsertEnd(this MethodDefinition md, params Instruction[] instructions)
        {
            // Locate the return instruction..
            for (var x = md.Body.Instructions.Count - 1; x >= 0; x--)
            {
                // Skip if we are not a return..
                if (md.Body.Instructions[x].OpCode != OpCodes.Ret)
                    continue;

                // Insert the new instructions..
                var ilp = md.Body.GetILProcessor();
                ilp.InsertAfter(md.Body.Instructions[x], Instruction.Create(OpCodes.Ret));
                foreach (var i in instructions.Reverse())
                    ilp.InsertAfter(md.Body.Instructions[x], i);

                // Nop the original instruction..
                md.Body.Instructions[x].OpCode = OpCodes.Nop;
            }

            // Optimize the short branches..
            md.OptimizeBranches();
        }

        /// <summary>
        /// Scans the given method for the opcode pattern.
        /// </summary>
        /// <param name="md"></param>
        /// <param name="offset"></param>
        /// <param name="opcodes"></param>
        /// <returns></returns>
        public static int ScanForPattern(this MethodDefinition md, int offset, params OpCode[] opcodes)
        {
            var ilp = md.Body.GetILProcessor();
            for (var x = offset; x < ilp.Body.Instructions.Count - opcodes.Length; x++)
            {
                if (opcodes.TakeWhile((o, y) => ilp.Body.Instructions[x + y].OpCode == o).Where((t, y) => y == opcodes.Length - 1).Any())
                    return x;
            }
            return -1;
        }
    }

    /// <summary>
    /// Insert location enumeration.
    /// </summary>
    public enum InsertLocation
    {
        /// <summary>
        /// States that inserts will happen at the start of a processor.
        /// </summary>
        Start = 0,

        /// <summary>
        /// States that inserts will happen at the end of a processor.
        /// </summary>
        End = 1
    }
}
