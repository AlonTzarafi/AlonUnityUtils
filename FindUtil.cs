using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace AlonUnityUtils
{
    public static class FindUtil
    {
        static public T FindComponentMatchString<T>(GameObject gameObject, string str) where T : Component
        {
            return gameObject
                .GetComponentsInChildren<T>(true)
                .FirstOrDefault(comp => comp.name == str);
        }

        static public T FindComponentMatchStringGlobal<T>(string str) where T : Component
        {
            return GameObject.FindObjectsOfType<T>(true)
                .First(go => go.name == str);
        }

        static public List<GameObject> FindGameObjectsMatchName(string str)
        {
            Debug.Log($"Finding GO with name: {str}");
            return GameObject.FindObjectsOfType<GameObject>()
                .Where(go => go.name == str)
                .ToList();
        }
        static public GameObject FindGameObjectMatchName(string str)
        {
            Debug.Log($"Finding GO with name: {str}");
            return GameObject.FindObjectsOfType<GameObject>()
                .First(go => go.name == str);
        }
        static public Transform FindTransformMatchName(string str)
        {
            Debug.Log($"Finding GO with name: {str}");
            return GameObject.FindObjectsOfType<Transform>()
                .First(go => go.name == str);
        }

        static public List<GameObject> GetChildren(GameObject obj)
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
            Debug.Log("CHILD NOT FOUND!!!", transform);
            Debug.Log("IN PARENT!!!", parent);
            Debug.LogError("ITS PROBLEM!!!!!");
            return -1;
        }

        static public void ResolveSceneDependencies(object obj)
        {
            var fieldWithAttributes = obj.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(field => field.GetCustomAttributes(typeof(SceneDependencyAttribute), true).Length > 0)
                .ToList();

            foreach (var field in fieldWithAttributes) {
                var type = field.FieldType;
                if (type == typeof(GameObject)) {
                    field.SetValue(obj, FindGameObjectMatchName(field.Name));
                } else if (type == typeof(Transform)) {
                    field.SetValue(obj, FindTransformMatchName(field.Name));
                } else {
                    Debug.LogError($"Error: TRhe object {obj} has a SceneDependency with an unsupported type: {type}. Currently only GameObject and Transform are supported.");
                }
            }
        }
    }
}
