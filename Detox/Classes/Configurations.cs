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
    using System.Collections.Generic;

    public class Configurations
    {
        /// <summary>
        /// Internal singleton instance of this class.
        /// </summary>
        private static Configurations _instance;

        /// <summary>
        /// Internal Detox configuration object.
        /// </summary>
        private DetoxConfigurations _configurations = new DetoxConfigurations();

        /// <summary>
        /// Gets the singleton instance of this class.
        /// </summary>
        public static Configurations Instance
        {
            get { return _instance ?? (_instance = new Configurations()); }
        }

        /// <summary>
        /// Loads a Detox configuration file.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool LoadConfig(string file)
        {
            this._configurations = DetoxAPI.Configuration.ReadConfiguration<DetoxConfigurations>(file);
            if (this._configurations == null)
            {
                this._configurations = new DetoxConfigurations();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Saves the current configurations to the given file.
        /// </summary>
        /// <param name="file"></param>
        public bool SaveConfig(string file)
        {
            return DetoxAPI.Configuration.WriteConfiguration(file, this._configurations);
        }

        /// <summary>
        /// Gets the current Detox configurations.
        /// </summary>
        public DetoxConfigurations Current
        {
            get { return this._configurations; }
        }
    }

    /// <summary>
    /// Configuration settings for graphics related purposes.
    /// </summary>
    public class DetoxGraphicsConfigurations
    {
        /// <summary>
        /// Gets or sets the game window width at startup.
        /// </summary>
        [ConfigurationInformation("WindowWidth", "Sets the game windows width at startup.")]
        public int StartupWindowWidth = 800;

        /// <summary>
        /// Gets or sets the game window height at startup.
        /// </summary>
        [ConfigurationInformation("WindowHeight", "Sets the game windows height at startup.")]
        public int StartupWindowHeight = 600;

        /// <summary>
        /// Gets or sets the UI skin file to load.
        /// </summary>
        [ConfigurationInformation("Skin", "Sets the skin file to load for the Detox UI.")]
        public string Skin = "Terraria";
    }

    /// <summary>
    /// Configuration settings for custom object related purposes.
    /// </summary>
    public class DetoxCustomObjectsConfigurations
    {
        /// <summary>
        /// Gets or sets if Detox should override Terraria's item backgrounds.
        /// </summary>
        [ConfigurationInformation("UseCustomBackgrounds", "Sets if Detox should override Terraria's item backgrounds.")]
        public bool UseCustomBackgrounds = false;

        /// <summary>
        /// Gets or sets if Detox should override Terraria's fonts.
        /// </summary>
        [ConfigurationInformation("UseCustomFonts", "Sets if Detox should override Terraria's fonts.")]
        public bool UseCustomFonts = false;

        /// <summary>
        /// Gets or sets if Detox should override Terraria's hp/mp icons.
        /// </summary>
        [ConfigurationInformation("UseCustomHpMpIcons", "Sets if Detox should override Terraria's hp/mp icons.")]
        public bool UseCustomHpMpIcons = false;
    }

    /// <summary>
    /// Configuration settings for Steam related purposes.
    /// </summary>
    public class DetoxSteamConfigurations
    {
        /// <summary>
        /// Gets or sets the Steam initialization flag.
        /// </summary>
        [ConfigurationInformation("InitializeSteam", "Sets if Detox should initialize Steam on startup.")]
        public bool InitializeSteam = false;
    }

    /// <summary>
    /// Configuration settings for plugin related purposes.
    /// </summary>
    public class DetoxPluginConfigurations
    {
        /// <summary>
        /// Gets or sets the list of plugins to auto-load at startup.
        /// </summary>
        [ConfigurationInformation("AutoLoadPlugins", "List of plugins to auto-load when Detox starts.")]
        public List<string> AutoLoadPlugins = new List<string>();
    }

    /// <summary>
    /// Detox Configurations
    /// </summary>
    public class DetoxConfigurations
    {
        /// <summary>
        /// Internal Steam configurations object.
        /// </summary>
        private DetoxSteamConfigurations _steam;

        /// <summary>
        /// Internal graphics configurations object.
        /// </summary>
        private DetoxGraphicsConfigurations _graphics;

        /// <summary>
        /// Internal custom object configurations object.
        /// </summary>
        private DetoxCustomObjectsConfigurations _custom;

        /// <summary>
        /// Internal plugin configurations object.
        /// </summary>
        private DetoxPluginConfigurations _plugins;

        /// <summary>
        /// Gets or sets the Detox Steam configurations.
        /// </summary>
        public DetoxSteamConfigurations Steam
        {
            get { return _steam ?? (_steam = new DetoxSteamConfigurations()); }
            set { _steam = value; }
        }

        /// <summary>
        /// Gets or sets the Detox graphics configurations.
        /// </summary>
        public DetoxGraphicsConfigurations Graphics
        {
            get { return _graphics ?? (_graphics = new DetoxGraphicsConfigurations()); }
            set { _graphics = value; }
        }

        /// <summary>
        /// Gets or sets the Detox custom configurations.
        /// </summary>
        public DetoxCustomObjectsConfigurations CustomObjects
        {
            get { return _custom ?? (_custom = new DetoxCustomObjectsConfigurations()); }
            set { _custom = value; }
        }

        /// <summary>
        /// Gets or sets the Detox plugin configurations.
        /// </summary>
        public DetoxPluginConfigurations Plugins
        {
            get { return _plugins ?? (_plugins = new DetoxPluginConfigurations()); }
            set { _plugins = value; }
        }
    }
}
