using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;
    public enum Sound
    {
        PickUpObjectSound,
        DropSound,
        ParticleSound,
        ResetSound,

    }

    private static Dictionary<Sound, float> soundTimerDictionary;

    public static void Initialise()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.PickUpObjectSound] = 0;
    }


    public static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;
            //case Sound.PickUpObjectSound:
            //    if (soundTimerDictionary.ContainsKey(sound))
            //    {
            //        float lastTimePlayed = soundTimerDictionary[sound];
            //        float intervelMax = 1;
            //        if (intervelMax + lastTimePlayed < Time.time)
            //        {
            //            soundTimerDictionary[sound] = Time.time;
            //            return true;
            //        }
            //        else
            //        {
            //            return false;
            //        }
            //    }
            //    else
            //    {
            //        return true;
            //    }

        }
    }

    public static void StopPlayingSound(Sound sound)
    {
        if (oneShotAudioSource.isPlaying)
        {
            Debug.LogError("Called");
            oneShotAudioSource.Stop();
        }
    }

    public static void PlaySound(Sound sound)
    {
        if (oneShotGameObject == null)
        {
            oneShotGameObject = new GameObject("One Shot Sound");
            oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
        }
        oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
        
    }



    public static AudioClip GetAudioClip(Sound sound)
    {
        foreach (var clip in SoundAssets.pInstance.Sounds)
        {
            if (clip.sound == sound)
            {
                return clip.audioClip;
            }
        }
        return null;
    }

}
