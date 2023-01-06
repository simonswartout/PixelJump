using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUIManager : MonoBehaviour
{
    SceneController sceneController;
    public void StartGame()
    {
        SceneManager.LoadScene("Level001");
    }
}
