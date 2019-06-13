using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerManager aPlayerManager;
    void Start()
    {
        GameObject vPlayerManagerGameObject = GameObject.Find("PlayerManager");
        aPlayerManager = vPlayerManagerGameObject.GetComponent<PlayerManager>();
        aPlayerManager.setPlayerManager(2);
        aPlayerManager.setNewPlayer("Mage");
        aPlayerManager.setNewPlayer("Knight");
        aPlayerManager.setVirtualCam();
    }

    private void Update()
    {
        aPlayerManager.setPlayerManager(2);
        aPlayerManager.setNewPlayer("Mage");
        aPlayerManager.setNewPlayer("Knight");
        aPlayerManager.setVirtualCam();
    }
}
