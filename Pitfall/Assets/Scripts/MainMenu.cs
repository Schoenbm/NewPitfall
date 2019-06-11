using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private PlayerManager aPlayerManager;

    private void Start()
    {
        GameObject vPlayerManagerGameObject = GameObject.Find("PlayerManager");
        aPlayerManager = vPlayerManagerGameObject.GetComponent<PlayerManager>();
    }

    public void PlayGame(int pNumberPlayer)
    {
        aPlayerManager.setPlayerManager(pNumberPlayer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("ShouldQUit");
        Application.Quit();
    }
}
