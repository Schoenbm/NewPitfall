using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject[] aRespawnPoints;
    private PlayerManager aPlayerManager;

    // Start is called before the first frame update
    public void setRespawnPoints()
    {
        GameObject vPlayerManagerGameObject = GameObject.Find("PlayerManager");
        aPlayerManager = vPlayerManagerGameObject.GetComponent<PlayerManager>();

        aPlayerManager.setRespawnPoints(aRespawnPoints);
    }

}
