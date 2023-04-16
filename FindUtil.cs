using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace AlonUnityUtils
{
    public static class FindUtil
    {
        static public T In<T>(GameObject gameObject, string str, bool optional = false) where T : Component
        {
            return ChildComponentByName<T>(gameObject, str, optional);
        }

        static public Transform ChildTransformByName(GameObject galleryScreen, string v)
        {
            return ChildComponentByName<Transform>(galleryScreen, v);
        }

        static public GameObject ChildByName(GameObject galleryScreen, string v)
        {
            return ChildComponentByName<Transform>(galleryScreen, v).gameObject;
        }
        
        static public GameObject MaybeChildByName(GameObject galleryScreen, string v)
        {
            return ChildComponentByName<Transform>(galleryScreen, v, true)?.gameObject;
        }

        static public T ChildComponentByName<T>(GameObject gameObject, string str, bool optional = false) where T : Component
        {
            var allComponents = gameObject.GetComponentsInChildren<T>(true);
            
            if (!optional) { 
                return allComponents.First(comp => comp.name == str);
            } else {
                return allComponents.FirstOrDefault(comp => comp.name == str);
            }
        }

        static public T ComponentByName<T>(string str) where T : Component
        {
            return GameObject.FindObjectsOfType<T>(true)
                .First(go => go.name == str);
        }

        static public List<GameObject> GameObjectsByName(string str)
        {
            // Debug.Log($"Finding GO with name: {str}");
            return GameObject.FindObjectsOfType<GameObject>()
                .Where(go => go.name == str)
                .ToList();
        }
        static public GameObject GameObjectByName(string str, bool optional = false)
        {
            // Debug.Log($"Finding GO with name: {str}");
            if (!optional) {
                return GameObject.FindObjectsOfType<GameObject>()
                    .First(go => go.name == str);
            } else {
                return GameObject.FindObjectsOfType<GameObject>()
                    .FirstOrDefault(go => go.name == str);
            }
        }
        static public Transform TransformByName(string str)
        {
            // Debug.Log($"Finding GO with name: {str}");
            return GameObject.FindObjectsOfType<Transform>()
                .First(go => go.name == str);
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
                    field.SetValue(obj, GameObjectByName(field.Name));
                } else if (type == typeof(Transform)) {
                    field.SetValue(obj, TransformByName(field.Name));
                } else {
                    Debug.LogError($"Error: The object {obj} has a SceneDependency with an unsupported type: {type}. Currently only GameObject and Transform are supported.");
                }
            }
        }
    }
}
