using UnityEngine;
using System.Linq;
using System.Collections.Generic;

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

        static public List<GameObject> GetChildrenList(GameObject obj)
        {
            var a = new List<GameObject>();
            for (int i = 0; i < obj.transform.childCount; i++) {
                a.Add(obj.transform.GetChild(i).gameObject);
            }
            return a;
        }

        static public GameObject[] GetChildrenArray(GameObject obj)
        {
            var a = new GameObject[obj.transform.childCount];
            for (int i = 0; i < obj.transform.childCount; i++) {
                a[i] = obj.transform.GetChild(i).gameObject;
            }
            return a;
        }

        static public int GetChildIndex(GameObject gameObject)
        {
            return GetChildIndex(gameObject.transform);
        }
        
        static public int GetChildIndex(Transform transform)
        {
            var parent = transform.parent;
            for (int i = 0; i < parent.childCount; i++) {
                if (parent.GetChild(i) == transform) {
                    return i;
                }
            }
            Debug.Log($"CHILD NOT FOUND: {transform.name} in {parent.name}");
            return -1;
        }
    }
}
