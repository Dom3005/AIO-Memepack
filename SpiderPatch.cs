using HarmonyLib;
using UnityEngine;

namespace AIO_Memepack
{
    [HarmonyPatch(typeof(SandSpiderAI))]
    internal class SpiderPatch
    {
        [HarmonyPatch(nameof(SandSpiderAI.TriggerChaseWithPlayer))]
        [HarmonyPostfix]
        public static void TriggerChase(ref SandSpiderAI __instance)
        {
            AudioSource audio = __instance.creatureSFX;
            audio.PlayOneShot(AudioManager.getAudioClip("ich-bin-eine-spinne.mp3"));
        }
    }
}
