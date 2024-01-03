using HarmonyLib;
using UnityEngine;

namespace AIO_Memepack
{
    [HarmonyPatch(typeof(RoundManager))]
    internal class RoundManagerPatch
    {
        //[HarmonyPatch(nameof(RoundManager.SpawnScrapInLevel))]
        //[HarmonyPrefix]
        //private static void RegisterScrap()
        //{
        //    // Jaegermeister, based on soda (id 50)
        //    Item jaeger = ItemManager.getItem(50);
        //    jaeger.itemName = "Jägermeister";
        //    jaeger.creditsWorth = 56;
        //    jaeger.minValue = 100;
        //    jaeger.maxValue = 100;
        //    jaeger.grabSFX = AudioManager.getAudioClip("jaegermeister.mp3");
        //    jaeger.itemId = StartOfRound.Instance.allItemsList.itemsList.Count;

        //    GameObject jaegerGO = Utils.loadGameObject("Jaeger.prefab", Utils.loadBundle("jaegermeister"));

        //    jaeger.spawnPrefab.GetComponent<MeshFilter>().mesh = jaegerGO.GetComponentInChildren<MeshFilter>().mesh;
        //    jaeger.spawnPrefab.GetComponent<MeshRenderer>().materials = jaegerGO.GetComponentInChildren<MeshRenderer>().materials;
        //    jaeger.spawnPrefab.GetComponent<GrabbableObject>().itemProperties = jaeger;

        //    ScanNodeProperties jaegerScan = jaeger.spawnPrefab.transform.GetChild(0).GetComponent<ScanNodeProperties>();
        //    jaegerScan.headerText = jaeger.itemName;

        //    ItemManager.getItem(20).grabSFX = AudioManager.getAudioClip("michi.mp3");


        //    StartOfRound.Instance.allItemsList.itemsList.Add(jaeger);
        //    Utils.addItemToAllLevels(jaeger, 100);
        //}
        
    }
}
