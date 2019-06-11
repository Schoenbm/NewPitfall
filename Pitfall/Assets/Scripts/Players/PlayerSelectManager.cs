using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerSelectManager : MonoBehaviour
{
    private PlayerManager aPlayerManager;

    private void Start()
    {
        GameObject vPlayerManagerGameObject = GameObject.Find("PlayerManager");
        aPlayerManager = vPlayerManagerGameObject.GetComponent<PlayerManager>();
    }

    public void caracterSelected(string pString)
    {
        bool vBool = aPlayerManager.setNewPlayer(pString);
        if (!vBool) // Si tout les joueurs ont choisis
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
