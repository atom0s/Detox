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
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ConsoleCommand
    {
        /// <summary>
        /// Internal list of alias' this command responds to.
        /// </summary>
        private readonly List<string> _alias;

        /// <summary>
        /// Internal callback to invoke for this command.
        /// </summary>
        private ConsoleCommands.ConsoleCommandDelegate _cmdCallback;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="cmdCallback"></param>
        public ConsoleCommand(List<string> alias, ConsoleCommands.ConsoleCommandDelegate cmdCallback)
        {
            this._alias = alias;
            this._cmdCallback = cmdCallback;
        }

        /// <summary>
        /// Invokes the console commands callback.
        /// </summary>
        /// <param name="raw"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool Invoke(string raw, List<string> args)
        {
            try
            {
                var e = new ConsoleCommandArgs(raw, args);
                this._cmdCallback(e);
                return e.Handled;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Determines if this command has the given alias.
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public bool HasAlias(string alias)
        {
            return this._alias.Any(a => a.ToLower() == alias);
        }

        /// <summary>
        /// Gets the main alias of this command.
        /// </summary>
        public string Name
        {
            get { return this._alias[0]; }
        }

        /// <summary>
        /// Gets the full list of names of this command.
        /// </summary>
        public List<string> Names
        {
            get { return this._alias; }
        }

        /// <summary>
        /// Gets or sets the description of this command.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the callback of this command.
        /// </summary>
        public ConsoleCommands.ConsoleCommandDelegate Callback
        {
            get { return this._cmdCallback; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                this._cmdCallback = value;
            }
        }
    }
}
