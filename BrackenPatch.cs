using HarmonyLib;

namespace AIO_Memepack
{
    [HarmonyPatch(typeof(FlowermanAI))]
    internal class BrackenPatch
    {
        [HarmonyPatch(nameof(FlowermanAI.Start))]
        [HarmonyPostfix]
        public static void Start(ref FlowermanAI __instance)
        {
            __instance.crackNeckSFX = AudioManager.getRandomClip("bluetooth.mp3");
        }
    }
}
