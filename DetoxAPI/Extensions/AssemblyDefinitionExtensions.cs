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
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class AssemblyDefinitionExtensions
    {
        /// <summary>
        /// Gets a field definition from the given type.
        /// </summary>
        /// <param name="asm"></param>
        /// <param name="parentType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static FieldDefinition GetField(this AssemblyDefinition asm, string parentType, string name)
        {
            return asm.MainModule.Types.First(t => t.Name == parentType).Fields.First(f => f.Name == name);
        }

        /// <summary>
        /// Gets a method definition from the given type.
        /// </summary>
        /// <param name="asm"></param>
        /// <param name="parentType"></param>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static MethodDefinition GetMethod(this AssemblyDefinition asm, string parentType, string name, List<Type> parameters = null)
        {
            if (parameters == null || parameters.Count == 0)
                return asm.MainModule.Types.First(t => t.Name == parentType).Methods.First(m => m.Name == name);

            var methods = asm.MainModule.Types.First(t => t.Name == parentType).Methods.Where(m => m.Name == name && m.Parameters.Count == parameters.Count);
            return (from m in methods let x = parameters.Where((t1, y) => t1.FullName == m.Parameters[y].ParameterType.FullName).Count() where x == parameters.Count select m).FirstOrDefault();
        }

        /// <summary>
        /// Gets a property definition from the given type.
        /// </summary>
        /// <param name="asm"></param>
        /// <param name="parentType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static PropertyDefinition GetProperty(this AssemblyDefinition asm, string parentType, string name)
        {
            return asm.MainModule.Types.First(t => t.Name == parentType).Properties.First(p => p.Name == name);
        }

        /// <summary>
        /// Gets a type definition from the main assembly.
        /// </summary>
        /// <param name="asm"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TypeDefinition GetType(this AssemblyDefinition asm, string name)
        {
            return asm.MainModule.Types.First(t => t.Name == name);
        }
    }
}
