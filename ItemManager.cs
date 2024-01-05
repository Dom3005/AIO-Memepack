using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace AIO_Memepack
{
    internal class ItemManager
    {
        public static List<Item> allItems = StartOfRound.Instance.allItemsList.itemsList;

        public static Item getItemNew(int id)
        {
            return UnityEngine.Object.Instantiate(allItems[id]);
        }

        public static Item getItem(int id)
        {
            return allItems[id];
        }

        public static GameObject spawnItemID(int id, Vector3 pos)
        {
            Item item = allItems[id];

            GameObject obj = UnityEngine.Object.Instantiate(item.spawnPrefab, pos, Quaternion.identity, StartOfRound.Instance.propsContainer);
            obj.GetComponent<GrabbableObject>().fallTime = 0f;
            obj.GetComponent<NetworkObject>().Spawn();

            return obj;
        }

        public static GameObject spawnItem(Item item, Vector3 pos)
        {
            GameObject obj = UnityEngine.Object.Instantiate(item.spawnPrefab, pos, Quaternion.identity, StartOfRound.Instance.propsContainer);
            obj.GetComponent<GrabbableObject>().itemProperties = item;
            obj.GetComponent<NetworkObject>().Spawn();
            return obj;
        }

        public static void RegisterScrap()
        {
            // Jaegermeister, based on soda (id 50)
            Item jaeger = GenerateItemStats(
                baseItem: getItemNew(50),
                itemName: "Jägermeister",
                worth: 56,
                min: 100,
                max: 100,
                grabFX: "jaegermeister.mp3"
            );

            SetItemModel(
                item: jaeger,
                prefabName: "Jaeger.prefab",
                bundleName: "jaegermeister"
            );

            AddItemToSpawnpool(jaeger, 75);

            // Chicken nuggets, based off coffee mug (Id 41)

            Item nuggets = GenerateItemStats(
                baseItem: getItemNew(41),
                itemName: "Chicken Nuggets",
                worth: 50,
                min: 50,
                max: 100,
                grabFX: "chicken-nuggets.mp3"
            );
            nuggets.spawnPrefab.GetComponent<BoxCollider>().size *= 2;
            SetItemModel(
                item: nuggets,
                prefabName: "ChickenNuggets.prefab"
            );
            AddItemToSpawnpool(nuggets, 75);

            LogItemIds();
        }

        public static void PatchItems()
        {
            // Boombox
            getItem(1).toolTips = new string[] { "Toggle on/off: [LMB]", "Next song: [Q]" };
            getItem(1).canBeGrabbedBeforeGameStart = true;

            // Shovel
            getItem(10).spawnPrefab.GetComponent<Shovel>().shovelHitForce *= ConfigManager._shovelStrength.Value;

            // Bier
            getItem(20).grabSFX = AudioManager.getAudioClip("michi.mp3");

            // Clown horn
            getItem(25).spawnPrefab.GetComponent<NoisemakerProp>().noiseSFX = new AudioClip[] { AudioManager.getAudioClip("goofy-ahh.mp3") };
            getItem(25).spawnPrefab.GetComponent<NoisemakerProp>().noiseSFXFar = new AudioClip[] { AudioManager.getAudioClip("goofy-ahh.mp3") };

            // Pickle jar
            getItem(44).grabSFX = AudioManager.getAudioClip("pickle-rick.mp3");
        }

        private static Item GenerateItemStats(Item baseItem, string itemName, int worth, int min, int max, string grabFX)
        {
            baseItem.itemName = itemName;
            baseItem.creditsWorth = worth;
            baseItem.minValue = min;
            baseItem.maxValue = max;
            baseItem.grabSFX = AudioManager.getAudioClip(grabFX);
            baseItem.itemId = StartOfRound.Instance.allItemsList.itemsList.Count;

            return baseItem;
        }

        private static void SetItemModel(Item item, string prefabName, string bundleName = "items")
        {
            GameObject prefab = Utils.loadGameObject(prefabName, Utils.loadBundle(bundleName));

            item.spawnPrefab.name = item.itemName;
            item.spawnPrefab.GetComponent<MeshFilter>().mesh = prefab.GetComponentInChildren<MeshFilter>().mesh;
            item.spawnPrefab.GetComponent<MeshRenderer>().materials = prefab.GetComponentInChildren<MeshRenderer>().materials;
            item.spawnPrefab.GetComponent<GrabbableObject>().itemProperties = item;

            ScanNodeProperties itemScan = item.spawnPrefab.transform.GetChild(0).GetComponent<ScanNodeProperties>();
            itemScan.headerText = item.itemName;
        }

        private static void AddItemToSpawnpool(Item item, int rarity)
        {
            StartOfRound.Instance.allItemsList.itemsList.Add(item);
            Utils.addItemToAllLevels(item, rarity);
        }

        public static void LogItemIds()
        {
            for(int i = 0; i < allItems.Count; i++)
            {
                MemepackBase.logger.LogInfo($"{i} - {allItems[i]}");
            }
        }
    }
}
