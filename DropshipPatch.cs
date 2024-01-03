using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace AIO_Memepack
{
    [HarmonyPatch(typeof(ItemDropship))]
    internal class DropshipPatch
    {
        private static int firstWaitTime;
        private static int normalWaitTime;

        public static Terminal terminal;
        private static StartOfRound playerManager;
        private static List<int> itemDelivery;

        [HarmonyPatch("Start")]
        [HarmonyPostfix]
        private static void Start(ref ItemDropship __instance)
        {
            firstWaitTime = ConfigManager._dropshipFirstWait.Value;
            normalWaitTime = ConfigManager._dropshipNormalWait.Value;

            string[] possibleClips = { "wuhlmaus-apored.mp3", "chinese-rap-song.mp3" };
            string clipName = possibleClips[Random.Range(0, possibleClips.Length)];
            __instance.transform.Find("Music").GetComponent<AudioSource>().clip = AudioManager.getAudioClip(clipName);
        }

        [HarmonyPatch("Update")]
        [HarmonyPrefix]
        private static bool Update(ref ItemDropship __instance)
        {
            terminal = Traverse.Create(__instance).Field("terminalScript").GetValue() as Terminal;
            playerManager = Traverse.Create(__instance).Field("playersManager").GetValue() as StartOfRound;
            itemDelivery = Traverse.Create(__instance).Field("itemsToDeliver").GetValue() as List<int>;

            if (!__instance.IsServer) return false;
            if (!__instance.deliveringOrder)
            {
                if (terminal.orderedItemsFromTerminal.Count > 0)
                {
                    if (playerManager.shipHasLanded)
                    {
                        __instance.shipTimer += Time.deltaTime;
                    }
                    if (__instance.playersFirstOrder)
                    {
                        __instance.playersFirstOrder = false;
                        __instance.shipTimer = firstWaitTime;
                    }
                    if (__instance.shipTimer > normalWaitTime)
                    {
                        LandShipOnServer(ref __instance);
                    }
                }
                return false; // Dont run base method
            }
            return true; // Run base method
        }

        [HarmonyPatch("LandShipOnServer")]
        [HarmonyPrefix]
        private static bool LandShipOnServer(ref ItemDropship __instance)
        {
            __instance.shipTimer = 0f;
            itemDelivery.Clear();
            itemDelivery.AddRange(terminal.orderedItemsFromTerminal);
            terminal.orderedItemsFromTerminal.Clear();
            __instance.playersFirstOrder = false;
            __instance.deliveringOrder = true;
            __instance.LandShipClientRpc();

            return false;
        }
    }
}
