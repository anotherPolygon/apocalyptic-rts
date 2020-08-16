using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayShotSound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip audioClip;



    // Start is called before the first frame update
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void ShootEvent()
    {
        Debug.Log("Played Shot");
        audioSource.PlayOneShot(audioClip);
    }



}
