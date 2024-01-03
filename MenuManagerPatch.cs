using BepInEx.Configuration;
using HarmonyLib;
using LCSoundTool;
using System.Collections.Generic;

namespace AIO_Memepack
{
    [HarmonyPatch(typeof(MenuManager))]
    internal class MenuManagerPatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        public static void Start()
        {
            if (ConfigManager._overrideSoundToolConfig.Value)
            {
                ICollection<ConfigDefinition> keys = SoundTool.Instance.Config.Keys;

                foreach (ConfigDefinition key in keys)
                {
                    switch (key.Key)
                    {
                        case "EnableNetworking":
                        case "SyncUnityRandomSeed":
                            SoundTool.Instance.Config[key].BoxedValue = true;
                            break;
                    }
                }
            }
        }
    }
}
