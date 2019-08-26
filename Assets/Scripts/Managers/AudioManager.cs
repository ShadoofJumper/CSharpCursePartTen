using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    public float soundVolume
    {
        get {return AudioListener.volume; }
        set { AudioListener.volume = value;}
    }

    public bool soundMute
    {
        get { return AudioListener.pause; }
        set { AudioListener.pause = value; }
    }

    private NetworkService _networ;

    // here code for controll volume

    public void Startup(NetworkService service)
    {
        Debug.Log("Audio manager startup...");

        _networ = service;

        // here inizialize sound source
        soundVolume = 1f;

        status = ManagerStatus.Started;
    }



}
