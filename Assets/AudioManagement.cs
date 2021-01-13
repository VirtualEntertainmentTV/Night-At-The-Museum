using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManagement : MonoBehaviour
{
    public static AudioManagement instance;

    [SerializeField]
    private AudioSource sceneChange, errorSound;

    void Awake()
    {
        instance = this;
    }

    public void PlaySound(SoundEffects type = SoundEffects.Scene)
    {
        switch (type)
        {
            case SoundEffects.Scene:
                sceneChange.Play();
                break;
        }
    }
}

public enum SoundEffects
{
    Scene, PositiveTone, NegativeTone
}



