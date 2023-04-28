using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    public void playClip(string clip)
    {
        Debug.Log("Sound Effects/" + clip);
        audioPlayer.clip = Resources.Load<AudioClip>("Sound Effects/" + clip);
        audioPlayer.Play();
    }
}



