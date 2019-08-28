using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPopup : MonoBehaviour
{

    [SerializeField] private AudioClip sound;

    public void OnMusicToggle()
    {
        Managers.Audio.musicMute = !Managers.Audio.musicMute;
        Managers.Audio.PlaySound(sound);
    }

    public void OnMusicValue(float volume)
    {
        Managers.Audio.musicVolume = volume;
    }


    public void OnSoundToggle()
    {
        Debug.Log("OnSoundToggle");
        Managers.Audio.soundMute = !Managers.Audio.soundMute;
        Managers.Audio.PlaySound(sound);
    }

    public void OnSoundValue(float volume)
    {
        Managers.Audio.soundVolume = volume;
    }

    public void OnPlayMusic(int selector)
    {
        switch (selector)
        {
            case 1:
                Managers.Audio.PlayLevelMusic();
                break;
            case 2:
                Managers.Audio.PlayIntroMusic();
                break;
            default:
                Managers.Audio.StopMusic();
                break;
        }
    }
}
