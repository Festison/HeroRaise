using Redcode.Pools;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Veco;

public class SoundComponent : MonoBehaviour, IPlayClip
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
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void SoundPlay(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
        Invoke("ReturnPool", clip.length);
    }

    public void ReturnPool()
    {
        ObjectPoolManager.Instance.ReturnPool(gameObject);
    }
}
