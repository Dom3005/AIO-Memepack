using HarmonyLib;
using TMPro;
using UnityEngine;

namespace AIO_Memepack
{
    [HarmonyPatch(typeof(HUDManager))]
    internal class HudManagerPatch
    {
        private static TextMeshPro shipScrapText;

        [HarmonyPatch("Start")]
        [HarmonyPrefix]
        public static void Start(ref HUDManager __instance)
        {
            //GameObject go = Object.Instantiate(__instance.totalValueText).gameObject;
            //Utils.logComponentsRecursively(__instance.totalValueText.gameObject);
            //go.GetComponent<RectTransform>().position += Vector3.left * 50;
            //shipScrapText = go.GetComponent<TextMeshPro>();
            //shipScrapText.fontSizeMin = 10;
            //shipScrapText.fontSize = 10;
        }
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        public static void Update(ref HUDManager __instance)
        {
            int[] values;
            int sum = calculateShiploot();

            int scanValue = Traverse.Create(__instance).Field("totalScrapScannedDisplayNum").GetValue<int>();
            __instance.totalValueText.text = $"<size=10>Scan: {scanValue}\nIn ship: {sum}</size>";


        }

        private static int calculateShiploot()
        {
            GameObject ship = GameObject.Find("/Environment/HangarShip");
            GrabbableObject[] loot = ship.GetComponentsInChildren<GrabbableObject>();
            int sum = 0;
            foreach(GrabbableObject scrap in loot)
            {
                if (!scrap.itemProperties.isScrap) continue;
                sum += scrap.scrapValue;
            }

            return sum;
        }
    }
}
