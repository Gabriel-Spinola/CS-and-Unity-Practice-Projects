﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seven.AudioManager
{
    public class SoundLibrary : MonoBehaviour
    {
        public SoundGroup[] soundGroups;

        private Dictionary<string, AudioClip[]> groupDictionary = new Dictionary<string, AudioClip[]>();

        private void Awake()
        {
            foreach (SoundGroup group in soundGroups) {
                groupDictionary.Add(group.groupID, group.group);
            }
        }

        public AudioClip GetClipFromName(string name)
        {
            if (groupDictionary.ContainsKey(name)) {
                AudioClip[] sounds = groupDictionary[name];

                Debug.Log(groupDictionary.Count);
                return sounds[Random.Range(0, sounds.Length)];
            }

            Debug.LogWarning($"Sound: { name } not found!");

            return null;
        }

        [System.Serializable]
        public class SoundGroup
        {
            public string groupID;
            public AudioClip[] group;
        }
    }
}