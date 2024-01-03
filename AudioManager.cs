using LCSoundTool;
using System.Collections.Generic;
using UnityEngine;

namespace AIO_Memepack
{
    internal class AudioManager
    {
        private static List<AudioClip> loadedClips = new List<AudioClip>();
        public static AudioClip getAudioClip(string filename)
        {
            if (loadedClips.Count == 0 || !isClipLoaded(filename))
            {
                AudioClip clip = SoundTool.GetAudioClip("Dom3005-German_AIO_Memepack", "AIO Memepack", filename);
                clip.name = filename;
                loadedClips.Add(clip);

                return clip;
            }

            return getLoadedClip(filename);
        }

        public static AudioClip getRandomClip(params string[] filenames)
        {
            string filename = filenames[Random.Range(0, filenames.Length)];
            return getAudioClip(filename);
        }

        public static AudioClip[] getAllClips(params string[] filenames)
        {
            AudioClip[] clips = new AudioClip[filenames.Length];
            for(int i = 0; i < filenames.Length; i++)
            {
                clips[i] = getAudioClip(filenames[i]);
            }
            return clips;
        }

        private static AudioClip getLoadedClip(string filename)
        {
            foreach (AudioClip clip in loadedClips)
            {
                if (clip.name == filename) return clip;
            }
            return null;
        }

        private static bool isClipLoaded(string filename)
        {
            foreach(AudioClip clip in loadedClips) 
            { 
                if(clip.name == filename) return true;
            }
            return false;
        }

        public static AudioSource createAudioSource(Vector3 position, Transform parent, float distance)
        {
            AudioSource audioSource = UnityEngine.Object.Instantiate(new GameObject(), position, Quaternion.identity, parent).AddComponent<AudioSource>();

            audioSource.maxDistance = distance;
            audioSource.spatialBlend = 1;
            audioSource.rolloffMode = AudioRolloffMode.Linear;

            return audioSource;
        }
    }

    public class AudioCollection
    {
        public List<AudioClip> clips;
        public string collectionName;

        public AudioCollection(List<AudioClip> _clips, string _name) 
        {
            clips = _clips;
            collectionName = _name;
        }
    }
}
