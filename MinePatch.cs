using HarmonyLib;

namespace AIO_Memepack
{
    [HarmonyPatch(typeof(Landmine))]
    internal class MinePatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        public static void Start(ref Landmine __instance)
        {
            __instance.mineDetonate = AudioManager.getRandomClip("discord-notification.mp3", "vine-boom.mp3", "bonk.mp3");
            __instance.mineDetonateFar = __instance.mineDetonate;
            __instance.minePress = AudioManager.getRandomClip("was-zitterstn-so.mp3", "bomb-planted.mp3");
        }
    }
}
