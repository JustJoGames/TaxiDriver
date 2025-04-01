using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource radioAudio;
    public Radio_Controller radio;

    private void Awake()
    {

        GameObject[] musicobj = GameObject.FindGameObjectsWithTag("Game_Music");
        if (musicobj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Imelda_Conversation"))
        {
            StartCoroutine(FadeOut(radioAudio, 0.1f));
            Destroy(this.gameObject);
        }
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