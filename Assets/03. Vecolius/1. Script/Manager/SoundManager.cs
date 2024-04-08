using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Veco
{
    public class SoundManager : SingleTon<SoundManager>
    {
        public AudioMixer mixer;
        public GameObject soundObj;
        [SerializeField] AudioSource bgSound;
        [SerializeField] AudioClip[] bgClips;
        [SerializeField] public AudioClip[] sfxClips;


        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            bgSound = GetComponent<AudioSource>();
            
            BgSoundPlay(bgClips[0]);
        }


        private void Update()
        {
            //transform.position = Camera.main.transform.position;

        }

        //씬 전환 시, 배경음 바꿈
        void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
        {
            if(scene.name == "LOBBY") 
            {
                
            }
            else if(scene.name == "InGAME")
            {

            }
        }

        public void SFXPlay(AudioClip clip, Transform audioPos = null)     //SFX Play
        {
            GameObject soundObj = ObjectPoolManager.Instance.PopObj(this.soundObj, transform.position, transform.rotation);
            soundObj.SetActive(true);
            if (soundObj.TryGetComponent(out IPlayClipable play))
                play.SoundPlay(clip, mixer);
            else
                Debug.Log("Interface 참조 못함");
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
