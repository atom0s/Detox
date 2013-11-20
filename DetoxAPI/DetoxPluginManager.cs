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
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public static class DetoxPluginManager
    {
        /// <summary>
        /// The current API version of the DetoxAPI file.
        /// </summary>
        public static Version DetoxAPIVersion = new Version(1, 0);

        /// <summary>
        /// Internal list of loaded plugins.
        /// </summary>
        private static readonly List<DetoxPluginContainer> _plugins;

        /// <summary>
        /// Default Constructor
        /// </summary>
        static DetoxPluginManager()
        {
            _plugins = new List<DetoxPluginContainer>();
        }

        /// <summary>
        /// Loads the given plugin.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DetoxPluginErrorReason.ErrorTypes LoadPlugin(string name)
        {
            // Append .dll to name if needed..
            var pluginName = name;
            if (!name.EndsWith(".dll", StringComparison.InvariantCultureIgnoreCase))
                pluginName += ".dll";

            // Ensure the plugin file exists..
            if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DetoxLibs\\", pluginName)))
                return DetoxPluginErrorReason.ErrorTypes.FileNotFound;

            // Load the assembly..
            var pluginPath = AppDomain.CurrentDomain.BaseDirectory + "DetoxLibs\\" + pluginName;
            var assembly = Assembly.Load(File.ReadAllBytes(pluginPath));

            // Locate the plugin type..
            var pluginBase = assembly.GetTypes().SingleOrDefault(t => t.BaseType == typeof(DetoxPlugin));
            if (pluginBase == null)
                return DetoxPluginErrorReason.ErrorTypes.FileNotPlugin;

            // Locate the interface version.
            var pluginInterface = pluginBase.GetCustomAttributes(typeof(DetoxApiVersionAttribute), false);
            if (pluginInterface.Length == 0)
                return DetoxPluginErrorReason.ErrorTypes.FileMissingAttribute;

            // Ensure the version is valid..
            var interfaceVersion = (DetoxApiVersionAttribute)pluginInterface[0];
            if (interfaceVersion.Version != DetoxPluginManager.DetoxAPIVersion)
                return DetoxPluginErrorReason.ErrorTypes.InvalidApiVersion;

            // Plugin is valid.. attempt to initialize..
            var plugin = (DetoxPlugin)Activator.CreateInstance(pluginBase);
            if (!plugin.Initialize())
                return DetoxPluginErrorReason.ErrorTypes.FailedInitialization;

            // Add the plugin..
            _plugins.Add(new DetoxPluginContainer(pluginPath, plugin));
            DetoxAPI.Console.PrintConsole(string.Format("{0} v{1} -- by {2} : {3}", plugin.Name, plugin.Version, plugin.Author, plugin.Description), ConsoleMessageType.Warning);

            return DetoxPluginErrorReason.ErrorTypes.Success;
        }

        /// <summary>
        /// Gets the list of loaded plugins.
        /// </summary>
        public static List<DetoxPluginContainer> Plugins
        {
            get { return _plugins; }
        }
    }
}
