using HarmonyLib;
using System.Collections;
using UnityEngine;

namespace AIO_Memepack
{
    [HarmonyPatch(typeof(StartOfRound))]
    internal class StartOfRoundPatch
    {
        [HarmonyPatch("playersFiredGameOver")]
        [HarmonyPrefix]
        private static void playersFiredGameOver(ref StartOfRound __instance)
        {
            string[] deathSounds = { "outro-song.mp3", "subway-surfer-bass-boosted.mp3", "in-hamburg-sagt-man-tschuss.mp3" };

            AudioClip clip = AudioManager.getAudioClip(deathSounds[Random.Range(0, deathSounds.Length)]);

            __instance.alarmSFX = clip;
            __instance.firedVoiceSFX = AudioManager.getRandomClip("monte_maybe_next_time.mp3", "traut-euch.mp3", "was-zitterstn-so.mp3");
        }

        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        private static void Start_prefix()
        {
            ItemManager.RegisterScrap();
            ItemManager.PatchItems();
        }
    }
}
