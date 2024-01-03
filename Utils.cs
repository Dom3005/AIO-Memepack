using BepInEx;
using LCSoundTool;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace AIO_Memepack
{
    internal class Utils
    {
        private static Dictionary<string, AssetBundle> loadedBundles = new Dictionary<string, AssetBundle>();

        public static AssetBundle loadBundle(string bundlename)
        {
            if(loadedBundles.ContainsKey(bundlename)) return loadedBundles[bundlename];

            string path = Path.Combine(Paths.PluginPath, "AIO Memepack", bundlename);
            if (!File.Exists(path))
            {
                MemepackBase.instance.logger.LogError($"{path}");
                path = Path.Combine(Paths.PluginPath, "Dom3005-German_AIO_Memepack", "AIO Memepack", bundlename);
                if (!File.Exists(path))
                {
                    MemepackBase.instance.logger.LogError($"Could not find bundle \"{bundlename}\"");
                    MemepackBase.instance.logger.LogError($"{path}");
                    return null;
                }
            }
            MemepackBase.instance.logger.LogInfo(path);

            AssetBundle bundle = AssetBundle.LoadFromFile(path);
            loadedBundles.Add(bundlename, bundle);
            return bundle;
        }

        public static GameObject loadGameObject(string filename, AssetBundle bundle)
        {
            return bundle.LoadAsset<GameObject>(filename);

        }

        public static Material loadMaterial(string filename, AssetBundle bundle)
        {
            return bundle.LoadAsset<Material>(filename);
        }
        
        public static void addItemToAllLevels(Item item, int rarity)
        {
            SpawnableItemWithRarity spawnable = new SpawnableItemWithRarity();
            spawnable.rarity = rarity;
            spawnable.spawnableItem = item;

            SelectableLevel[] levels = StartOfRound.Instance.levels;

            foreach(SelectableLevel level in levels)
            {
                level.spawnableScrap.Add(spawnable);
            }

            // look at spawn positions (strings)
        }

        public static void logComponents(GameObject go)
        {
            Component[] comps = go.GetComponents<Component>();
            foreach(Component comp in comps)
            {
                MemepackBase.instance.logger.LogInfo(comp.ToString());
            }
        }

        public static void logComponentsRecursively(GameObject go)
        {
            foreach (Transform child in go.transform)
            {
                logComponents(child.gameObject);
            }
        }

        public static void logChildren(GameObject go)
        {
            foreach (Transform child in go.transform)
            {
                MemepackBase.instance.logger.LogInfo(child.ToString());
            }
        }
    }
}
