﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioManager))]

public class Managers : MonoBehaviour
{
    public static AudioManager Audio { get; private set; }


    private List<IGameManager> _startSequence;

    void Awake()
    {
        Audio = GetComponent<AudioManager>();

        _startSequence = new List<IGameManager>();
        _startSequence.Add(Audio);
        StartCoroutine(StartupManagers());

    }

    private IEnumerator StartupManagers()
    {
        NetworkService network = new NetworkService();

        foreach(IGameManager manager in _startSequence)
        {
            manager.Startup(network);
        }
        yield return null;

        int numModels = _startSequence.Count;
        int numReady = 0;

        while (numReady < numModels)
        {
            int lastReady = numReady;
            numReady = 0;

            foreach(IGameManager manager in _startSequence)
            {
                if (manager.status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }

            if (numReady > lastReady)
                Debug.Log("Progress: "+ numReady + "/" + numModels);

            yield return null;
        }
        Debug.Log("All manager started");
    }
}
