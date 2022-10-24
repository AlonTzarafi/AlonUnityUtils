using UnityEngine;
using System.Linq;

namespace AlonUnityUtils
{
    public static class GameObjectUtil
    {
        static public void DestroyChildren(Transform gameObject)
        {
            var allChildren = GetAllChildren(gameObject);
            foreach (var child in allChildren)
            {
                GameObject.Destroy(child);
            }
        }
        
        static public void DestroyChildren(GameObject gameObject)
        {
            DestroyChildren(gameObject.transform);
        }

        static public GameObject[] GetAllChildren(Transform gameObject)
        {
            return gameObject.GetComponentsInChildren<Transform>()
                .Where(t => t != gameObject)
                .Select(t => t.gameObject)
                .ToArray();
        }

        static public GameObject[] GetAllChildren(GameObject gameObject)
        {
            return GetAllChildren(gameObject.transform);
        }
    }
}
