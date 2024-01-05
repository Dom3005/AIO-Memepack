using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace AIO_Memepack
{
    [BepInPlugin(mod_guid, mod_name, mod_version)]
    public class MemepackBase : BaseUnityPlugin
    {
        private const string mod_guid = "dom3005.aio_memepack";
        private const string mod_name = "AIO Memepack";
        private const string mod_version = "1.3.0";

        private readonly Harmony harmony = new Harmony(mod_guid);

        public static MemepackBase instance;

        internal static ManualLogSource logger;

        void Awake()
        {
            instance = this;

            logger = BepInEx.Logging.Logger.CreateLogSource(mod_guid);
            logger.LogMessage($"Loaded {mod_name} version {mod_version}!");

            ConfigManager.Init();

            harmony.PatchAll();
        }
    }
}
