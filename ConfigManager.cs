using System.IO;
using BepInEx;
using BepInEx.Configuration;

namespace AIO_Memepack
{
    internal static class ConfigManager
    {
        private const string CONFIG_FILE_NAME = "aio_memepack.cfg";

        private static ConfigFile _config;

        public static ConfigEntry<float> _flashlightStrength;
        public static ConfigEntry<int> _shovelStrength;

        public static ConfigEntry<int> _dropshipFirstWait;
        public static ConfigEntry<int> _dropshipNormalWait;

        public static ConfigEntry<bool> _enableBoomboxSpawner;
        public static ConfigEntry<bool> _enableInfiniteSprint;
        public static ConfigEntry<bool> _enableFlashlightColors;
        public static ConfigEntry<bool> _disableEarlyLeaveVote;
        public static ConfigEntry<bool> _overrideSoundToolConfig;


        public static void Init()
        {
            _config = new ConfigFile(Path.Combine(Paths.ConfigPath, CONFIG_FILE_NAME), true);

            _flashlightStrength = _config.Bind("Config", "flashlightIntensity", 5.0f, "Basegame uses 1.0");
            _shovelStrength = _config.Bind("Config", "shovelStrength", 2, "Damage the shovel does\nBasegame uses 1.0");

            _dropshipFirstWait = _config.Bind("Config", "dropshipFirstWait", 5, "Basegame uses 20");
            _dropshipNormalWait = _config.Bind("Config", "dropshipNormalWait", 5, "Basegame uses 40");

            _enableBoomboxSpawner = _config.Bind("Config", "enableBoomboxSpawner", true, "Spawns a boombox on the first run of the game");
            _enableInfiniteSprint = _config.Bind("Config", "enableInfiniteSprint", true, "Allows the player to sprint indefinetly");
            _enableFlashlightColors = _config.Bind("Config", "enableFlashlightColors", true, "Makes each flashlight have a random color");
            _disableEarlyLeaveVote = _config.Bind("Config", "disableEarlyLeaveVote", true, "Removes the feature for dead players to vote to leave early");
            _overrideSoundToolConfig = _config.Bind("Config", "overrideSoundToolConfig", true, "Overrides the network config variables of the LC Soundtool mod, which make this mod work");

        }

    }
}
