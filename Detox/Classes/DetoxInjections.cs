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

namespace Detox.Classes
{
    using DetoxAPI;
    using DetoxAPI.Extensions;
    using Microsoft.Xna.Framework;
    using Mono.Cecil;
    using Mono.Cecil.Cil;
    using System;
    using System.Collections.Generic;

    public static class DetoxInjections
    {
        /// <summary>
        /// Injection method to apply hooks for Terraria events.
        /// </summary>
        [Injection("TerrariaInjection", "Creates hooks to fire Terraria events.")]
        public static void TerrariaInjection(AssemblyDefinition asm)
        {
            var mod = asm.MainModule;
            //
            // Terraria.Main.Ctor
            //
            var ctor = asm.GetMethod("Main", ".ctor");
            ctor.InsertEnd(
                Instruction.Create(OpCodes.Ldarg_0),
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Terraria).GetMethod("TerrariaConstructor", Detox.BindFlags))));
            //
            // Terraria.Main.NewText
            //
            var newText = asm.GetMethod("Main", "NewText");
            newText.InsertStart(
                Instruction.Create(OpCodes.Ldarg_0),
                Instruction.Create(OpCodes.Ldarg_1),
                Instruction.Create(OpCodes.Ldarg_2),
                Instruction.Create(OpCodes.Ldarg_3),
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Terraria).GetMethod("TerrariaNewText", Detox.BindFlags))),
                Instruction.Create(OpCodes.Ret));
            //
            // Terraria.Main.SetTitle
            //
            var setTitle = asm.GetMethod("Main", "SetTitle");
            setTitle.InsertStart(
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Terraria).GetMethod("TerrariaSetTitle", Detox.BindFlags))),
                Instruction.Create(OpCodes.Ret));
            //
            // Terraria.Main.GetInputText
            //
            var getInputText = asm.GetMethod("Main", "GetInputText");
            var editSign = asm.GetField("Main", "editSign");
            var netMode = asm.GetField("Main", "netMode");
            getInputText.InsertStart(
                Instruction.Create(OpCodes.Ldsfld, netMode),
                Instruction.Create(OpCodes.Brfalse_S, getInputText.Body.Instructions[0]),
                Instruction.Create(OpCodes.Ldsfld, editSign),
                Instruction.Create(OpCodes.Brtrue_S, getInputText.Body.Instructions[0]),
                Instruction.Create(OpCodes.Ldarg_0),
                Instruction.Create(OpCodes.Ret));
            //
            // Terraria.Main.DrawInterface
            //
            var drawInterface = asm.GetMethod("Main", "DrawInterface");
            drawInterface.InsertStart(
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Events.TerrariaMainEvents).GetMethod("InvokePreDrawInterface", Detox.BindFlags))));
            drawInterface.InsertEnd(
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Events.TerrariaMainEvents).GetMethod("InvokePostDrawInterface", Detox.BindFlags))));
        }

        /// <summary>
        /// Injection method to apply hooks for Xna events.
        /// </summary>
        [Injection("XnaInjection", "Creates hooks to fire Xna events.")]
        public static void XnaInjection(AssemblyDefinition asm)
        {
            var mod = asm.MainModule;
            //
            // Terraria.Main.Initialize
            //
            var initialize = asm.GetMethod("Main", "Initialize");
            initialize.InsertStart(
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Events.XnaEvents).GetMethod("InvokePreInitialize", Detox.BindFlags))));
            initialize.InsertEnd(
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Events.XnaEvents).GetMethod("InvokePostInitialize", Detox.BindFlags))));
            //
            // Terraria.Main.LoadContent
            //
            var loadContent = asm.GetMethod("Main", "LoadContent");
            loadContent.InsertStart(
                Instruction.Create(OpCodes.Ldarg_0),
                Instruction.Create(OpCodes.Callvirt, mod.Import(typeof(Game).GetMethod("get_Content"))),
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Events.XnaEvents).GetMethod("InvokePreLoadContent", Detox.BindFlags))));
            loadContent.InsertEnd(
                Instruction.Create(OpCodes.Ldarg_0),
                Instruction.Create(OpCodes.Callvirt, mod.Import(typeof(Game).GetMethod("get_Content"))),
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Events.XnaEvents).GetMethod("InvokePostLoadContent", Detox.BindFlags))));
            //
            // Terraria.Main.Update
            //
            var update = asm.GetMethod("Main", "Update");
            update.InsertStart(
                Instruction.Create(OpCodes.Ldarg_1),
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Events.XnaEvents).GetMethod("InvokePreUpdate", Detox.BindFlags))));
            update.InsertEnd(
                Instruction.Create(OpCodes.Ldarg_1),
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Events.XnaEvents).GetMethod("InvokePostUpdate", Detox.BindFlags))));

            // Force mouse to stay visible..
            for (var x = 0; x < update.Body.Instructions.Count; x++)
            {
                var inst = update.Body.Instructions[x];
                if (inst.Operand != null && inst.Operand.ToString().ToLower().Contains("set_ismousevisible"))
                    update.Body.Instructions[x - 1].OpCode = OpCodes.Ldc_I4_1;
            }

            // Add input-blocking callback..
            var blockInputPattern = new[] { OpCodes.Ldc_I4_1, OpCodes.Stsfld, OpCodes.Call, OpCodes.Stsfld };
            var blockInputOffset = update.ScanForPattern(0, blockInputPattern);
            update.Insert(blockInputOffset + blockInputPattern.Length,
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Terraria).GetMethod("TerrariaUpdateBlockInput", Detox.BindFlags))));
            //
            // Terraria.Main.Draw
            //
            var draw = asm.GetMethod("Main", "Draw");
            draw.InsertStart(
                Instruction.Create(OpCodes.Ldarg_1),
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Events.XnaEvents).GetMethod("InvokePreDraw", Detox.BindFlags))));
            draw.InsertEnd(
                Instruction.Create(OpCodes.Ldarg_1),
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Events.XnaEvents).GetMethod("InvokePostDraw", Detox.BindFlags))));
        }

        /// <summary>
        /// Injection method to apply hooks for Terraria.Player events.
        /// </summary>
        /// <param name="asm"></param>
        [Injection("PlayerInjection", "Creates hooks to fire Player events.")]
        public static void PlayerInjection(AssemblyDefinition asm)
        {
            var mod = asm.MainModule;
            //
            // Terraria.Player.UpdatePlayer
            //
            var updatePlayer = asm.GetMethod("Player", "UpdatePlayer");
            updatePlayer.InsertStart(
                Instruction.Create(OpCodes.Ldarg_0),
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Events.PlayerEvents).GetMethod("InvokePreUpdatePlayer", Detox.BindFlags))));
            updatePlayer.InsertEnd(
                Instruction.Create(OpCodes.Ldarg_0),
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Events.PlayerEvents).GetMethod("InvokePostUpdatePlayer", Detox.BindFlags))));
            //
            // Terraria.Player.PlayerFrame
            //
            var playerFrame = asm.GetMethod("Player", "PlayerFrame");
            playerFrame.InsertStart(
                Instruction.Create(OpCodes.Ldarg_0),
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Events.PlayerEvents).GetMethod("InvokePrePlayerFrame", Detox.BindFlags))));
            playerFrame.InsertEnd(
                Instruction.Create(OpCodes.Ldarg_0),
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Events.PlayerEvents).GetMethod("InvokePostPlayerFrame", Detox.BindFlags))));
        }

        /// <summary>
        /// Injection method to apply hooks for Terraria.Recipe events.
        /// </summary>
        /// <param name="asm"></param>
        [Injection("RecipeInjection", "Creates hooks to fire Recipe events.")]
        public static void RecipeInjection(AssemblyDefinition asm)
        {
            var mod = asm.MainModule;
            //
            // Terraria.Recipe.FindRecipes
            //
            var findRecipe = asm.GetMethod("Recipe", "FindRecipes");
            findRecipe.InsertStart(
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Events.RecipeEvents).GetMethod("InvokePreFindRecipes", Detox.BindFlags))),
                Instruction.Create(OpCodes.Brfalse_S, findRecipe.Body.Instructions[0]),
                Instruction.Create(OpCodes.Ret));
            findRecipe.InsertEnd(
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Events.RecipeEvents).GetMethod("InvokePostFindRecipes", Detox.BindFlags))));
            //
            // Terraria.Recipe.SetupRecipes
            //
            var setupRecipes = asm.GetMethod("Recipe", "SetupRecipes");
            setupRecipes.InsertStart(
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Events.RecipeEvents).GetMethod("InvokePreSetupRecipes", Detox.BindFlags))),
                Instruction.Create(OpCodes.Brfalse_S, setupRecipes.Body.Instructions[0]),
                Instruction.Create(OpCodes.Ret));
            setupRecipes.InsertEnd(
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Events.RecipeEvents).GetMethod("InvokePostSetupRecipes", Detox.BindFlags))));
        }

        /// <summary>
        /// Injection method to apply hooks for Terraria.Lighting events.
        /// </summary>
        /// <param name="asm"></param>
        [Injection("LightingInjection", "Creates hooks to fire Lighting events.")]
        public static void LightingInjection(AssemblyDefinition asm)
        {
            var mod = asm.MainModule;
            //
            // Terrari.Lighting.Brightness
            //
            var brightness = asm.GetMethod("Lighting", "Brightness");
            brightness.Body.Variables.Add(new VariableDefinition("brightnessRet", brightness.ReturnType));
            brightness.InsertStart(
                Instruction.Create(OpCodes.Ldarg_0),
                Instruction.Create(OpCodes.Ldarg_1),
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Events.LightingEvents).GetMethod("InvokeBrightness", Detox.BindFlags))),
                Instruction.Create(OpCodes.Stloc_2),
                Instruction.Create(OpCodes.Ldloc_2),
                Instruction.Create(OpCodes.Ldc_R4, -1.0f),
                Instruction.Create(OpCodes.Beq, brightness.Body.Instructions[0]),
                Instruction.Create(OpCodes.Ldloc_2),
                Instruction.Create(OpCodes.Ret));
            //
            // Terraria.Lighting.LightColor
            //
            var lightColor = asm.GetMethod("Lighting", "LightColor");
            lightColor.InsertStart(
                Instruction.Create(OpCodes.Ldarg_0),
                Instruction.Create(OpCodes.Ldarg_1),
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Events.LightingEvents).GetMethod("InvokeLightColor", Detox.BindFlags))),
                Instruction.Create(OpCodes.Brfalse_S, lightColor.Body.Instructions[0]),
                Instruction.Create(OpCodes.Ret));
            //
            // Terraria.Lighting.GetColor
            //
            var getColor1 = asm.GetMethod("Lighting", "GetColor", new List<Type> { typeof(int), typeof(int) });
            var getColor2 = asm.GetMethod("Lighting", "GetColor", new List<Type> { typeof(int), typeof(int), typeof(Color) });
            getColor1.InsertStart(
                Instruction.Create(OpCodes.Ldarg_0),
                Instruction.Create(OpCodes.Ldarg_1),
                Instruction.Create(OpCodes.Ldloca_S, getColor1.Body.Variables[5]),
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Events.LightingEvents).GetMethod("InvokeGetColor1", Detox.BindFlags))),
                Instruction.Create(OpCodes.Brfalse_S, getColor1.Body.Instructions[0]),
                Instruction.Create(OpCodes.Ldloc_S, getColor1.Body.Variables[5]),
                Instruction.Create(OpCodes.Ret));
            getColor2.InsertStart(
                Instruction.Create(OpCodes.Ldarg_0),
                Instruction.Create(OpCodes.Ldarg_1),
                Instruction.Create(OpCodes.Ldarg_2),
                Instruction.Create(OpCodes.Ldloca_S, getColor2.Body.Variables[2]),
                Instruction.Create(OpCodes.Call, mod.Import(typeof(Events.LightingEvents).GetMethod("InvokeGetColor2", Detox.BindFlags))),
                Instruction.Create(OpCodes.Brfalse_S, getColor2.Body.Instructions[0]),
                Instruction.Create(OpCodes.Ldloc_S, getColor2.Body.Variables[2]),
                Instruction.Create(OpCodes.Ret));
        }
    }
}
