using HarmonyLib;
using System.Collections;
using Unity.Netcode;
using UnityEngine;

namespace AIO_Memepack
{
    [HarmonyPatch(typeof(StartMatchLever))]
    internal class LeverPatch
    {
        private static bool enableBoomboxSpawner;

        private static bool hasSpawnedBoombox;

        [HarmonyPatch(nameof(StartMatchLever.PullLever))]
        [HarmonyPrefix]
        public static bool PullLever(ref StartMatchLever __instance)
        {
            int x = Random.Range(1, 19);
            __instance.playersManager.openingHangarDoorAudio = AudioManager.getAudioClip($"weis ({x}).mp3");
            __instance.playersManager.shipDoorAudioSource.maxDistance *= 3f;
            __instance.playersManager.shipDoorAudioSource.volume *= 1.5f;

            if (__instance.leverHasBeenPulled)
            {
                __instance.StartGame();

                enableBoomboxSpawner = ConfigManager._enableBoomboxSpawner.Value;
                GameObject ship = GameObject.Find("/Environment/HangarShip");
                hasSpawnedBoombox = ship.GetComponentsInChildren<BoomboxItem>().Length > 0;

                if (enableBoomboxSpawner && !hasSpawnedBoombox)
                {
                    // Spawn boombox (id 1)
                    GameObject bb = ItemManager.spawnItemID(1, __instance.transform.position + new Vector3(-1, 2, 0));
                    bb.GetComponent<GrabbableObject>().fallTime = 0f;
                    bb.GetComponent<NetworkObject>().Spawn();

                    //AudioClip spawnsound = AudioManager.getAudioClip("whistle.mp3");

                    //AudioSource audioPlayer = AudioManager.createAudioSource(__instance.transform.position, __instance.transform, 30f);

                    //audioPlayer.clip = spawnsound;
                    //audioPlayer.Play();
                    //__instance.StartCoroutine(stopPlayerAfter(audioPlayer, 20));

                    hasSpawnedBoombox = true;
                }
            }
            else
            {
                __instance.EndGame();
            }
            return false;
        }

        private static IEnumerator stopPlayerAfter(AudioSource source, float waitFor)
        {
            yield return new WaitForSeconds(waitFor);
            while (source.volume > 0)
            {
                source.volume -= Time.deltaTime * 0.25f;
            }
            source.Stop();
        }
    }
}
