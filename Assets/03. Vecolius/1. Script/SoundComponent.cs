using Redcode.Pools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Veco
{
    public class SoundComponent : MonoBehaviour, IPlayClipable, IAddressable
    {
        [SerializeField] AudioSource audioSource;
        AsyncOperationHandle handle;

        event Action audioPlay;

        public AudioSource AudioSource => audioSource;
        public Action AudioPlay
        {
            get => audioPlay;
            set
            {
                audioPlay += value;
            }
        }

        void OnEnable()
        {
            if (audioPlay != null) audioPlay();
        }
        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        public void SoundPlay(string clipAddress, AudioMixer mixer)
        {
            LoadAsset(clipAddress);
            audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
            audioSource.loop = false;
            //Invoke("ReturnPool", clip.length);
        }

        public void ReturnPool()
        {
            UnloadAsset();
            ObjectPoolManager.Instance.ReturnPool(gameObject);
        }

        public void LoadAsset(string namePath)
        {
            Addressables.LoadAssetAsync<AudioClip>(namePath).Completed += (AsyncOperationHandle<AudioClip> clip) =>
            {
                handle = clip;
                audioSource.clip = clip.Result;
                audioSource.Play();
                Invoke("ReturnPool", clip.Result.length);
            };
        }

        public void UnloadAsset()
        {
            audioSource.clip = null;
            Addressables.Release(handle);
        }
    }
}
