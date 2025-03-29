using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
   public void Quit()
    {
        Application.Quit();
        Debug.Log("Game_Closed");
    }

    public void Game()
    {
        SceneManager.LoadScene("Opening_Cutscene");
    }
}
