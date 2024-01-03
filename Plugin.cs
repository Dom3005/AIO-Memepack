using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace AIO_Memepack
{
    [BepInPlugin(mod_id, mod_name, mod_version)]
    public class MemepackBase : BaseUnityPlugin
    {
        private const string mod_id = "dom3005.aio_memepack";
        private const string mod_name = "AIO Memepack";
        private const string mod_version = "1.1.0";

        private readonly Harmony harmony = new Harmony(mod_id);

        public static MemepackBase instance;

        internal ManualLogSource logger;

        void Awake()
        {
            instance = this;

            logger = BepInEx.Logging.Logger.CreateLogSource(mod_id);
            logger.LogMessage($"Loaded {mod_name} version {mod_version}!");

            ConfigManager.Init();

            harmony.PatchAll();
        }
    }
}
