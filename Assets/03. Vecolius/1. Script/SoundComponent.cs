using Redcode.Pools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Veco;

public class SoundComponent : MonoBehaviour, IPlayClipable
{
    [SerializeField] AudioSource audioSource;

    event Action audioPlay;

    public AudioSource AudioSource=>audioSource;
    public Action AudioPlay {
        get => audioPlay;
        set => audioPlay += value;
    }

    void OnEnable()
    {
        if(audioPlay != null) audioPlay();
    }
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void SoundPlay(AudioClip clip, AudioMixer mixer)
    {
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        audioSource.loop = false;
        audioSource.Play();
        Invoke("ReturnPool", clip.length);
    }

    public void ReturnPool()
    {
        ObjectPoolManager.Instance.ReturnPool(gameObject);
    }
}
