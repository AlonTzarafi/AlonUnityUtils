using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

namespace AlonUnityUtils
{
    public class LocalizedGameObject : MonoBehaviour
    {
        [SerializeField] public GameObject hebrew;
        [SerializeField] public GameObject english;
        [SerializeField] public GameObject arabic;
        [SerializeField] public GameObject russian;
        
        [HideInInspector] public GameObject obj;

        private List<GameObject> allLanguages;

        static public LocalizedGameObject FindAndInitLocalized(string name)
        {
            var found = GameObject.FindObjectsOfType<LocalizedGameObject>()
                .FirstOrDefault(obj => obj.gameObject.name == name);
            found.Init();
            return found;
        }

        public void Init()
        {
            allLanguages = new List<GameObject>();

            if (hebrew) { allLanguages.Add(hebrew); }
            if (english) { allLanguages.Add(english); }
            if (arabic) { allLanguages.Add(arabic); }
            if (russian) { allLanguages.Add(russian); }

            foreach (var lang in allLanguages) {
                lang.SetActive(false);
            }
        }

        public void SetLanguage(GameLanguage language)
        {
            switch (language) {
                case GameLanguage.Hebrew:
                    obj = hebrew;
                    break;
                case GameLanguage.English:
                    obj = english;
                    break;
                case GameLanguage.Arabic:
                    obj = arabic;
                    break;
                case GameLanguage.Russian:
                    obj = russian;
                    break;
            }
        }

        public void SetShown(bool value)
        {
            foreach (var lang in allLanguages) {
                if (lang != obj) {
                    lang.SetActive(false);
                } else {
                    lang.SetActive(value);
                }
            }
        }

        public Button Button()
        {
            return obj.GetComponentInChildren<Button>();
        }
    }
}
