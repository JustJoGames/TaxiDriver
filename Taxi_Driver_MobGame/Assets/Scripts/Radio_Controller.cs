using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radio_Controller : MonoBehaviour
{
    [Header("List of Tracks")]
    [SerializeField] private Track[] audioTracks;
    private int trackIndex;

    private AudioSource radioAudio;
    public AudioSource honk;

    public void Awake()
    {
        radioAudio = GetComponent<AudioSource>();
        trackIndex = 0;
        radioAudio.clip = audioTracks[trackIndex].trackAudioClip;

    }

    public void SkipForwardButton()
    {
        if (trackIndex < audioTracks.Length - 1)
        {
            trackIndex++;
            StartCoroutine(FadeOut(radioAudio, 0.1f));
            
        }
        else
        {
            trackIndex = 0;
        }

    }

    public void SkipBackwardsButton()
    {
        if (trackIndex >= 1)
        {
            trackIndex--;
            StartCoroutine(FadeOut(radioAudio, 0.1f));
            
        }

    }

    void UpdateTrack(int index)
    {
        radioAudio.clip = audioTracks[index].trackAudioClip;

        PlayAudio();
    }

    public void PlayAudio()
    {
        radioAudio.Play();
        StartCoroutine(FadeIn(radioAudio, 0.1f));
    }

    public void StopAudio()
    {
        radioAudio.Stop();

    }

    public void Honk()
    {
        honk.Play();
    }

    public IEnumerator FadeOut(AudioSource audioSource, float fadetime)
    {
        float startvolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startvolume * Time.deltaTime / fadetime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startvolume;
        UpdateTrack(trackIndex); 
    }

    public IEnumerator FadeIn(AudioSource audioSource, float fadetime)
    {
        float startvolume = audioSource.volume;

        
        audioSource.Play();

        while (audioSource.volume > startvolume)
        {
            audioSource.volume += startvolume * Time.deltaTime / fadetime;
            yield return null;
        }

        audioSource.volume = startvolume;
    }
}
