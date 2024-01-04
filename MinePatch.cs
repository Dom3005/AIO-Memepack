using HarmonyLib;
using Unity.Netcode;
using UnityEngine;

namespace AIO_Memepack
{
    [HarmonyPatch(typeof(Landmine))]
    internal class MinePatch
    {
        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        public static void Start(ref Landmine __instance)
        {
            if(__instance.NetworkManager.IsHost)
            {
                SetSoundsClientRpc(__instance, AudioManager.getRandomClip("discord-notification.mp3", "vine-boom.mp3", "bonk.mp3"), AudioManager.getRandomClip("was-zitterstn-so.mp3", "bomb-planted.mp3"));
            }
        }

        [ClientRpc]
        private static void SetSoundsClientRpc(Landmine __instance, AudioClip clip1, AudioClip clip2)
        {
            __instance.mineDetonate = clip1;
            __instance.mineDetonateFar = __instance.mineDetonate;
            __instance.minePress = clip2;
        }
    }
}
