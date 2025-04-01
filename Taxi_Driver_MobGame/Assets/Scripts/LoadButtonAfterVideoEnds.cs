using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LoadButtonAfterVideoEnds : MonoBehaviour
{
    public VideoPlayer VideoPlayer; // Drag & Drop the GameObject holding the VideoPlayer component
    public GameObject returnbtn;

    void Start()
    {
        VideoPlayer.loopPointReached += LoadScene;
    }

    void LoadScene(VideoPlayer vp)
    {
        returnbtn.SetActive(true);
    }

    public void Return()
    {
        SceneManager.LoadScene("Title_Screen");
    }
}