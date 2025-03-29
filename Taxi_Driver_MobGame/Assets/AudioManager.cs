using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource radioAudio;

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
            Destroy(this.gameObject);
            StartCoroutine(FadeOut(radioAudio, 0.1f));
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
}