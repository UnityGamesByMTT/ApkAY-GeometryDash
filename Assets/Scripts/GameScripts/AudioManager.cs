using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] Clips;
    [SerializeField] private AudioClip[] Noob;

    private AudioClip Clip;



    internal void PlaySound(string SoundName)
    {
        switch (SoundName)
        {
            case "Button":
                Clip = Clips[0];
                break;

            case "GameOver":
                Clip = Clips[1];
                break;

            case "LevelComplete":
                Clip = Clips[2];
                break;
            case "Noob":
                int x = Random.Range(0, Noob.Length - 1);
                Clip = Noob[x];
                break;
        }
        audioSource.PlayOneShot(Clip);
    }

    // comit

}
