using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace TenSecondsToDie
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;

        
        [HideInInspector] public Sound mainStart;
        private Sound mainLoop;
        private Sound deathScreen;

        public static Action<string> PlayAudio;

        private void Awake()
        {
            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
            }

            mainStart = Array.Find(sounds, sound => sound.name == "MainStart");
            mainLoop = Array.Find(sounds, sound => sound.name == "MainLoop");
            deathScreen = Array.Find(sounds, sound => sound.name == "DeathScreen");
        }

        public void Play(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.source.Play();
        }
        public void Stop(string name)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.source.Stop();
        }

        // --------- SOUNDTRACK MANAGEMENT -----------//
        private void Start()
        {
            //deathScreen.source.mute = true;
            deathScreen.source.loop = true;
            mainLoop.source.loop = true;

            //.source.Play();
            deathScreen.source.Play();
        }

        private void PlayMainSoundtrack()
        {
            Invoke("PlayMainLoop", mainStart.source.clip.length);
            deathScreen.source.Stop();
            deathScreen.source.mute = true;
            mainStart.source.Play();
            deathScreen.source.Play();
        }


        private void PlayMainLoop()
        {
            mainLoop.source.Play();
        }

        public void SwitchToEndScreenSoundtrack()
        {
            deathScreen.source.mute = false;
            mainStart.source.mute = true;
        }

        private void OnEnable()
        {
            PlayAudio += Play;
            EventManager.OnTwoPlayersConnected += PlayMainSoundtrack;
        }
        private void OnDisable()
        {
            PlayAudio -= Play;
            EventManager.OnTwoPlayersConnected -= PlayMainSoundtrack;
        }
    }
}

