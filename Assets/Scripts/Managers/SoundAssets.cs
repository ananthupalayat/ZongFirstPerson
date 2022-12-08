using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundAssets : MonoBehaviour
{

    public static SoundAssets pInstance;
    public SoundAudioClip[] Sounds;

    private void Awake()
    {
        if (pInstance == null)
        {
            pInstance = this;
        }
        SoundManager.Initialise();
    }


    [Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }

}