using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move_scene : MonoBehaviour
{
    public void Play_scene()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
    public void Main_menu_scene()
    {
        SceneManager.LoadScene(0);
    }
}
