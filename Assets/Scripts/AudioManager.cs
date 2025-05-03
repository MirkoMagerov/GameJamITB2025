using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;

    public void PlayRandomPitch()
    {
        float randomPitch = Random.Range(0.95f, 1.05f);
        audioSource.pitch = randomPitch;
        audioSource.Play();
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

}
