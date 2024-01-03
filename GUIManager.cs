using UnityEngine;
using TMPro;

namespace AIO_Memepack
{
    internal class GUIManager
    {
        public static Canvas myCanvas;

        public static TextMeshPro CreateText(Vector2 position, string text)
        {
            GameObject go = new GameObject();

            if (myCanvas == null) myCanvas = new GameObject("aio_memeCanvas").AddComponent<Canvas>();

            go.transform.SetParent(myCanvas.transform);

            TextMeshPro tmp = go.AddComponent<TextMeshPro>();
            tmp.text = text;
            go.transform.position = position;

            return tmp;
        }
    }
}
