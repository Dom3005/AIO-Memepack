using HarmonyLib;

namespace AIO_Memepack
{
    [HarmonyPatch(typeof(JesterAI))]
    internal class JesterPatch
    {
        [HarmonyPatch(nameof(JesterAI.Start))]
        [HarmonyPostfix]
        public static void Start(ref JesterAI __instance)
        {
            __instance.popGoesTheWeaselTheme = AudioManager.getAudioClip("freebird-0.mp3");
            __instance.popUpSFX = AudioManager.getAudioClip("freebird-1.mp3");
        }
    }
}
