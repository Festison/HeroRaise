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
        [SerializeField] AudioClip[] SPXclips;


        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            bgSound = GetComponent<AudioSource>();
            
            //BgSoundPlay(bgClips[0]);
        }


        private void Update()
        {
            //transform.position = Camera.main.transform.position;
        }

        //æ¿ ¿¸»Ø Ω√, πË∞Ê¿Ω πŸ≤ﬁ
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
            IPlayClip playClip = soundObj.GetComponent<IPlayClip>();
            AudioSource audiosource = playClip.AudioSource;
            audiosource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
            soundObj.SetActive(true);
            playClip.SoundPlay(clip);
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
