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
    }
}
