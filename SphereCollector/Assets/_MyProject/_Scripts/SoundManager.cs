using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioClip[] sounds;
    [SerializeField]
    AudioSource controlSounds;
    private void Awake() 
    {
        controlSounds = GetComponent<AudioSource>();
    }

    public void SelectSound(int indice, float volumen)
    {
        controlSounds.PlayOneShot(sounds[indice],volumen);
    }
}
