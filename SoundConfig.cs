using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AlonUnityUtils
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SoundConfig", order = 1)]
    public class SoundConfig : ScriptableObject
    {
        [SerializeField]
        public List<SoundNameAndClips> sounds;

        public AudioClip GetClip(string soundName)
        {
            foreach (var sound in sounds)
            {
                if (sound.name == soundName) {
                    return sound.GetClip();
                }
            }
            Debug.LogError($"Sound does not exist in the config: \"{soundName}\"", this);
            return null;
        }
    }

    [Serializable]
    public struct SoundNameAndClips
    {
        public string name;
        public AudioClip[] clips;

        public AudioClip GetClip()
        {
            if (clips != null && clips.Any()) {
                return RandomUtil.Element(clips);
            }
            return null;
        }
    }
}
