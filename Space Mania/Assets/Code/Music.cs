using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music Main;

    private AudioSource audioSource = null;
    
    void Start()
    {
        if (Main == null)
                Main = this;
        else
            Debug.LogWarning("Music singleton is already assigned.");

        audioSource = GetComponent<AudioSource>();
    }

    public static void StopMusic()
    {
        Main.audioSource.Stop();
    }
}