using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Veco
{
    public class SoundManager : SingleTon<SoundManager>
    {
        public AudioMixer mixer;
        public AudioSource bgSound;
        [SerializeField] AudioClip[] bgClips;
        public AudioClip[] SPXclips;

        private void Start()
        {
            bgSound = GetComponent<AudioSource>();
            BgSoundPlay(bgClips[0]);
        }
        private void Update()
        {
            transform.position = Camera.main.transform.position;
        }

        public void SFXPlay(string sfxName, AudioClip clip, Transform audioPos)     //SFX Play
        {
            GameObject go = new GameObject(sfxName + "Sound");
            go.transform.position = audioPos.position;
            AudioSource audiosource = go.AddComponent<AudioSource>();
            audiosource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
            audiosource.clip = clip;
            audiosource.Play();
            Destroy(go, clip.length);
        }

        public void BgSoundPlay(AudioClip clip)     //BgSound Play
        {
            bgSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BGSound")[0];
            bgSound.clip = clip;
            bgSound.loop = true;
            bgSound.volume = 0.1f;
            bgSound.Play();
        }

        public void BgSoundVolume(float value)          //BGsound Volume Setting
        {
            mixer.SetFloat("BGSoundVolume", Mathf.Log10(value) * 20);
        }

        public void SFXVolume(float value)                 //SFX Volume Setting
        {
            mixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
        }
    }
}
