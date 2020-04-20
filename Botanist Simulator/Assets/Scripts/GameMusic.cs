using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    public AudioClip[] tracks;
    public int index = 0;
    public new AudioSource audio;
    void Start()
    {
        GetComponent<AudioSource>().loop = false;
        StartCoroutine(PlaySound());
    }

    IEnumerator PlaySound()
    {
        audio.clip = tracks[index];
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);

        if (index < tracks.Length - 1)
            index++;
        else
            index = 0;
        StartCoroutine(PlaySound());
    }
}
