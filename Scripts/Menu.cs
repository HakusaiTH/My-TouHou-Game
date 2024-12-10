using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public void startgame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void exitgame()
    {
        Application.Quit();
    }
}
