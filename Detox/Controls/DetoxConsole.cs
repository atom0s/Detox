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

namespace Detox.Controls
{
    using Classes;
    using DetoxAPI;
    using DetoxAPI.EventArgs;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TomShane.Neoforce.Controls;
    using Console = TomShane.Neoforce.Controls.Console;
    using ConsoleMessage = TomShane.Neoforce.Controls.ConsoleMessage;

    public class DetoxConsole : Panel
    {
        /// <summary>
        /// Main console object to attach to our control.
        /// </summary>
        private readonly Console _console;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="manager"></param>
        public DetoxConsole(Manager manager)
            : base(manager)
        {
            // Initialize this control..
            base.Init();
            base.Left = base.Top = 0;
            base.Name = "frmConsole";
            base.Width = Terraria.MainGame.Window.ClientBounds.Width;
            base.Height = 250;
            base.Alpha = 255;
            base.Resizable = false;
            //
            //
            //
            this._console = new TomShane.Neoforce.Controls.Console(manager)
                {
                    Anchor = Anchors.All,
                    Name = "DetoxConsole",
                    Width = base.ClientArea.Width,
                    Height = base.ClientArea.Height,
                    ChannelsVisible = false,
                    MessageFormat = ConsoleMessageFormats.TimeStamp,
                    TextColor = Color.White
                };
            this._console.Channels.AddRange(new[]
                {
                    new ConsoleChannel(0, "About", Color.Chartreuse),
                    new ConsoleChannel(1, "Default", Color.White),
                    new ConsoleChannel(2, "Warning", Color.Yellow),
                    new ConsoleChannel(3, "Error", Color.Red)
                });
            this._console.SelectedChannel = 1;
            this._console.MessageSent += (sender, e) =>
                {
                    // Ensure the command is valid..
                    if (string.IsNullOrEmpty(e.Message.Text) || !e.Message.Text.StartsWith("/"))
                        return;

                    // Attempt to handle the command..
                    e.Handled = ConsoleCommands.ProcessCommand(e.Message.Text);
                    if (!e.Handled)
                    {
                        this.LogConsoleMessage(new DetoxAPI.ConsoleMessage("Invalid command or command error occurred.", ConsoleMessageType.Error));
                        e.Handled = true;
                    }
                };
            this._console.Init();
            //
            //
            //
            base.Add(this._console);
            manager.Add(this);

            // Attach device settings event to resize console..
            manager.DeviceSettingsChanged += args =>
                {
                    // Adjust the base panel size..
                    base.Width = Terraria.MainGame.Window.ClientBounds.Width;
                    base.Height = 250;

                    // Adjust the console size..
                    this._console.Width = base.ClientArea.Width;
                    this._console.Height = base.ClientArea.Height;
                };

            // Locate the console textbox..
            var consoleEdit = this._console.Controls.SingleOrDefault(c => c.GetType() == typeof(TextBox));
            if (consoleEdit == null) return;

            consoleEdit.KeyDown += (sender, e) =>
                {
                    // Prevent tilde from being processed..
                    if (e.Key == Keys.OemTilde)
                        e.Handled = true;

                    // Force escape to remove focus from the console..
                    if (e.Key == Keys.Escape)
                    {
                        consoleEdit.Focused = false;
                        e.Handled = true;
                    }
                };
            consoleEdit.KeyPress += (sender, e) =>
                {
                    if (e.Key == Keys.OemTilde)
                        e.Handled = true;
                };
            consoleEdit.KeyUp += (sender, e) =>
                {
                    if (e.Key == Keys.OemTilde)
                        e.Handled = true;
                };
            consoleEdit.TextChanged += (sender, e) =>
                {
                    consoleEdit.Suspended = true;
                    consoleEdit.Text = consoleEdit.Text.Replace("`", "");
                    consoleEdit.Suspended = false;
                    e.Handled = true;
                };
            consoleEdit.FocusGained += (sender, e) => Terraria.SetMainField("chatMode", false);
            consoleEdit.FocusLost += (sender, e) => Terraria.SetMainField("chatMode", false);

            // Subscribe to the API console message event..
            DetoxAPI.Console.PrintConsoleMessage += (sender, e) => this.LogConsoleMessage(e);

            // Register some basic console commands..
            DetoxAPI.ConsoleCommands.AddCommnd(new List<string> { "help", "info", "h", "?" }, "Prints a list of registered console commands.", OnHelpCommand);
            DetoxAPI.ConsoleCommands.AddCommnd(new List<string> { "exit", "close", "terminate" }, "Exits Terraria immediately.", args => System.Windows.Forms.Application.Exit());
            DetoxAPI.ConsoleCommands.AddCommnd(new List<string> { "plugin", "extension", "p" }, "Handles a plugin command.", OnPluginCommand);
        }

        /// <summary>
        /// Logs a message to the console.
        /// </summary>
        /// <param name="msg"></param>
        public void LogConsoleMessage(DetoxAPI.ConsoleMessage msg)
        {
            this._console.MessageBuffer.Add(new ConsoleMessage
                {
                    Channel = (byte)msg.MessageType,
                    Text = msg.Message,
                    Time = DateTime.Now
                });
        }

        /// <summary>
        /// Update override to enforce chatmode block.
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            // Allow base control to update..
            base.Update(gameTime);

            // Locate the console textbox..
            var consoleEdit = this._console.Controls.SingleOrDefault(c => c.GetType() == typeof(TextBox));
            if (consoleEdit == null) return;

            // Set the chat mode..
            Terraria.SetMainField("chatMode", consoleEdit.Focused);
        }

        /// <summary>
        /// Gets or sets if the console has input focus.
        /// </summary>
        public bool HasInputFocus
        {
            get
            {
                var consoleEdit = this._console.Controls.SingleOrDefault(c => c.GetType() == typeof(TextBox));
                return consoleEdit != null && consoleEdit.Focused;
            }
            set
            {
                var consoleEdit = this._console.Controls.SingleOrDefault(c => c.GetType() == typeof(TextBox));
                if (consoleEdit == null)
                    return;
                consoleEdit.Focused = value;
            }
        }

        /// <summary>
        /// Handles the help console command.
        /// </summary>
        /// <param name="e"></param>
        private void OnHelpCommand(ConsoleCommandArgs e)
        {
            // Sort the console commands by primary name..
            var cmds = DetoxAPI.ConsoleCommands.Commands.OrderBy(c => c.Name).ToList();

            // Print each command..
            this.LogConsoleMessage(new DetoxAPI.ConsoleMessage("Registered console commands:", ConsoleMessageType.About));
            this.LogConsoleMessage(new DetoxAPI.ConsoleMessage("----------------------------------------------------", ConsoleMessageType.About));
            cmds.ForEach(c => this.LogConsoleMessage(new DetoxAPI.ConsoleMessage(string.Format("    /{0} - {1} [{2}]", c.Name, c.Description, string.Join(", ", c.Names)), ConsoleMessageType.Warning)));
            this.LogConsoleMessage(new DetoxAPI.ConsoleMessage(string.Format("{0} total console commands.", cmds.Count), ConsoleMessageType.About));

            e.Handled = true;
        }

        /// <summary>
        /// Handles the plugin console command.
        /// </summary>
        /// <param name="e"></param>
        private void OnPluginCommand(ConsoleCommandArgs e)
        {
            if (e.Arguments.Count == 3 && e.Arguments[1].ToLower() == "load")
            {
                // Attempt to load the given plugin..
                var ret = DetoxPluginManager.LoadPlugin(e.Arguments[2].ToLower());
                if (ret != 0)
                {
                    this.LogConsoleMessage(new DetoxAPI.ConsoleMessage(string.Format(
                        "Failed to load plugin: {0} -- {1}",
                        e.Arguments[2].ToLower(), DetoxPluginErrorReason.ErrorStrings[(int)ret]), ConsoleMessageType.Error));
                }
            }
            else if (e.Arguments.Count == 2 && e.Arguments[1].ToLower() == "list")
            {
                var plugins = DetoxPluginManager.Plugins.OrderBy(p => p.Plugin.Name).ToList();
                this.LogConsoleMessage(new DetoxAPI.ConsoleMessage("Loaded plugins:", ConsoleMessageType.About));
                this.LogConsoleMessage(new DetoxAPI.ConsoleMessage("----------------------------------------------------", ConsoleMessageType.About));
                plugins.ForEach(p => this.LogConsoleMessage(new DetoxAPI.ConsoleMessage(string.Format(
                    " {0} v{1} by {2} -- {3}", p.Plugin.Name, p.Plugin.Version, p.Plugin.Author, p.Plugin.Description), ConsoleMessageType.Warning)));
                this.LogConsoleMessage(new DetoxAPI.ConsoleMessage(string.Format("{0} loaded plugins.", plugins.Count), ConsoleMessageType.About));
            }
            else
            {
                this.LogConsoleMessage(new DetoxAPI.ConsoleMessage("Invalid command syntax; valid plugin commands are:", ConsoleMessageType.Error));
                this.LogConsoleMessage(new DetoxAPI.ConsoleMessage("  /plugin load [pluginname]", ConsoleMessageType.Error));
                this.LogConsoleMessage(new DetoxAPI.ConsoleMessage("  /plugin list", ConsoleMessageType.Error));
            }

            e.Handled = true;
        }
    }
}
