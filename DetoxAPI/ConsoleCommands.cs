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

namespace DetoxAPI
{
    using EventArgs;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class ConsoleCommands
    {
        /// <summary>
        /// Console command delegate used for command callbacks.
        /// </summary>
        /// <param name="e"></param>
        public delegate void ConsoleCommandDelegate(ConsoleCommandArgs e);

        /// <summary>
        /// List of currently registered console commands.
        /// </summary>
        public static List<ConsoleCommand> Commands = new List<ConsoleCommand>();

        /// <summary>
        /// Parses the incoming raw message for arguments.
        /// </summary>
        /// <param name="raw"></param>
        /// <returns></returns>
        public static List<string> ParseCommand(string raw)
        {
            // Validate the incoming command..
            if (string.IsNullOrEmpty(raw) || !raw.StartsWith("/"))
                return null;

            // Parse the command for arguments..
            var args = new List<string>();
            Regex.Matches(raw, @"(?<match>[\w\.\!\-]+)|\""(?<match>[\w\s\.\!\-]*)""")
                .Cast<Match>()
                .Select(m => m.Groups["match"].Value)
                .ToList()
                .ForEach(args.Add);
            return args;
        }

        /// <summary>
        /// Processes the incoming command.
        /// </summary>
        /// <param name="raw"></param>
        /// <returns></returns>
        public static bool ProcessCommand(string raw)
        {
            // Validate the incoming command..
            if (string.IsNullOrEmpty(raw) || !raw.StartsWith("/"))
                return false;

            // Parse the command for arguments..
            var args = ConsoleCommands.ParseCommand(raw);
            if (args == null || args.Count == 0)
                return false;

            try
            {
                // Attemt to locate and invoke the owning command..
                var cmd = ConsoleCommands.Commands.SingleOrDefault(c => c.HasAlias(args[0]));
                return cmd != null && cmd.Invoke(raw, args);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Registers a console command to the console command list.
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="description"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static bool AddCommnd(List<string> alias, string description, ConsoleCommandDelegate callback)
        {
            // Validate the parameters..
            if (alias.Count == 0 || string.IsNullOrEmpty(description) || callback == null)
                return false;

            // Ensure no other command has any alias defined..
            if (ConsoleCommands.Commands.Any(c => alias.Any(c.HasAlias)))
                return false;

            // Add the new command..
            var cmd = new ConsoleCommand(alias, callback) { Description = description };
            ConsoleCommands.Commands.Add(cmd);
            return true;
        }

        /// <summary>
        /// Removes a console command by its alias.
        /// </summary>
        /// <param name="alias"></param>
        public static void RemoveCommand(string alias)
        {
            ConsoleCommands.Commands.RemoveAll(c => c.HasAlias(alias));
        }
    }
}
