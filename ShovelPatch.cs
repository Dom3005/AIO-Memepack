using HarmonyLib;
using UnityEngine;

namespace AIO_Memepack
{
    [HarmonyPatch(typeof(Shovel))]
    internal class ShovelPatch
    {
        [HarmonyPatch("HitShovel")]
        [HarmonyPostfix]
        private static void HitShovel(ref Shovel __instance)
        {
            AudioClip[] shovelHitSFX = AudioManager.getAllClips("bonk.mp3", "tf2-bonk.mp3", "discord-notification.mp3");
            RoundManager.PlayRandomClip(__instance.shovelAudio, shovelHitSFX);
        }
    }
}
