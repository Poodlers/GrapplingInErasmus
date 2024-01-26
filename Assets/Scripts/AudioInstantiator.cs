using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInstantiator : MonoBehaviour
{
    public AudioClip audioClip;
    // Start is called before the first frame update
    void Awake()
    {
        //find any audio source in the scene
        AudioSource audioSource = FindObjectOfType<AudioSource>();
        //if there is no audio source
        if (audioSource == null)
        {
            //create a new audio source
            AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
            //copy the properties of the audio source
            newAudioSource.clip = audioClip;
            newAudioSource.loop = true;
            newAudioSource.Play();

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
