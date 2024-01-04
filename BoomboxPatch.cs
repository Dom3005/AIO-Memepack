using GameNetcodeStuff;
using HarmonyLib;
using System.Reflection;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AIO_Memepack
{
    [HarmonyPatch(typeof(BoomboxItem))]
    internal class BoomboxPatch
    {
        private static int currentSongIndex = 0;
        private static PlayerActions playerActions;

        [HarmonyPatch(nameof(BoomboxItem.UseUpBatteries))]
        [HarmonyPrefix]
        public static bool depleteBattery(ref BoomboxItem __instance)
        {
            __instance.insertedBattery.charge = 1.0f;
            return false;
        }

        [HarmonyPatch("StartMusic")]
        [HarmonyPrefix]
        public static bool StartMusic(ref bool startMusic, ref BoomboxItem __instance)
        {
            if(startMusic)
            {
                __instance.boomboxAudio.clip = NextSong(__instance);
                __instance.boomboxAudio.pitch = 1f;
                __instance.boomboxAudio.Play();
                __instance.isBeingUsed = true;
                __instance.isPlayingMusic = true;
                return false;
            }
            return true;
        }

        [HarmonyPatch(nameof(BoomboxItem.Update))]
        [HarmonyPrefix]
        public static void Update(ref BoomboxItem __instance)
        {
            __instance.insertedBattery.charge = 1.0f;
        }

        [HarmonyPatch(nameof(BoomboxItem.Start))]
        [HarmonyPrefix]
        public static void Start(ref BoomboxItem __instance)
        {
            BoomboxItem inst = __instance;
            playerActions = new PlayerActions();

            playerActions.Movement.QEItemInteract.performed += ctx => PlayNextSong(ctx, inst);
            playerActions.Movement.Enable();
        }

        public static void PlayNextSong(InputAction.CallbackContext context, BoomboxItem __instance)
        {
            if (__instance.playerHeldBy == null || __instance.playerHeldBy.playerClientId != StartOfRound.Instance.localPlayerController.playerClientId) return;
            AudioClip nextClip;
            if (context.control.path.EndsWith("q")) nextClip = NextSong(__instance);
            else return;

            __instance.UseItemOnClient(true);

            //PlayNextServerRpc(__instance, nextClip);
        }

        [ServerRpc(RequireOwnership = false)]
        public static void PlayNextServerRpc(BoomboxItem __instance, AudioClip nextClip)
        {
            __instance.boomboxAudio.clip = nextClip;
            __instance.boomboxAudio.pitch = 1f;
            __instance.boomboxAudio.Play();
            __instance.isBeingUsed = true;
            __instance.isPlayingMusic = true;

            PlayNextClient(__instance, nextClip);
        }

        [ClientRpc]
        public static void PlayNextClient(BoomboxItem __instance, AudioClip clip)
        {
            __instance.boomboxAudio.clip = clip;
            __instance.boomboxAudio.pitch = 1f;
            __instance.boomboxAudio.Play();
            __instance.isBeingUsed = true;
            __instance.isPlayingMusic = true;
        }

        public static AudioClip NextSong(BoomboxItem __instance)
        {
            int next = currentSongIndex++;
            if (currentSongIndex >= __instance.musicAudios.Length) currentSongIndex = 0;

            return __instance.musicAudios[next];
        }

    }
}
