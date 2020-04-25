using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class ChangeScene : MonoBehaviour
{
    public void ToMainMenu()
    {
        UnitySceneManager.LoadScene("Main Menu");
    }

    public void ToMainGame()
    {
        UnitySceneManager.LoadScene("Main Game");
    }

    public void ToGameOverScene()
    {
        UnitySceneManager.LoadScene("Game Over");
    }
}
