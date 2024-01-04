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
        private static void RegisterScrap()
        {
            // Jaegermeister, based on soda (id 50)
            Item jaeger = ItemManager.getItemNew(50);
            jaeger.itemName = "Jägermeister";
            jaeger.creditsWorth = 56;
            jaeger.minValue = 100;
            jaeger.maxValue = 100;
            jaeger.grabSFX = AudioManager.getAudioClip("jaegermeister.mp3");
            jaeger.itemId = StartOfRound.Instance.allItemsList.itemsList.Count;

            GameObject jaegerGO = Utils.loadGameObject("Jaeger.prefab", Utils.loadBundle("jaegermeister"));

            jaeger.spawnPrefab.GetComponent<MeshFilter>().mesh = jaegerGO.GetComponentInChildren<MeshFilter>().mesh;
            jaeger.spawnPrefab.GetComponent<MeshRenderer>().materials = jaegerGO.GetComponentInChildren<MeshRenderer>().materials;
            jaeger.spawnPrefab.GetComponent<GrabbableObject>().itemProperties = jaeger;

            ScanNodeProperties jaegerScan = jaeger.spawnPrefab.transform.GetChild(0).GetComponent<ScanNodeProperties>();
            jaegerScan.headerText = jaeger.itemName;

            // Boombox
            ItemManager.getItem(1).toolTips = new string[]{ "Toggle on/off: [LMB]", "Next song: [Q]" };
            ItemManager.getItem(1).canBeGrabbedBeforeGameStart = true;

            // Shovel
            ItemManager.getItem(10).spawnPrefab.GetComponent<Shovel>().shovelHitForce *= ConfigManager._shovelStrength.Value; 

            // Bier
            ItemManager.getItem(20).grabSFX = AudioManager.getAudioClip("michi.mp3");

            // Clown horn
            ItemManager.getItem(25).spawnPrefab.GetComponent<NoisemakerProp>().noiseSFX = new AudioClip[]{ AudioManager.getAudioClip("goofy-ahh.mp3") };
            ItemManager.getItem(25).spawnPrefab.GetComponent<NoisemakerProp>().noiseSFXFar = new AudioClip[]{ AudioManager.getAudioClip("goofy-ahh.mp3") };

            // Pickle jar
            ItemManager.getItem(44).grabSFX = AudioManager.getAudioClip("pickle-rick.mp3");



            StartOfRound.Instance.allItemsList.itemsList.Add(jaeger);
            Utils.addItemToAllLevels(jaeger, 100);
        }
    }
}
