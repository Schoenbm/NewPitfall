using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int aPlayerNumber;
    private PlayerManager aPlayerManager;
    private Vector3 aInitPosition;
    private Quaternion aInitRotation;

    private void Start()
    {
        aInitPosition = this.transform.position;
        aInitRotation = this.transform.rotation;
    }

    public void Reset()
    {
        this.transform.SetPositionAndRotation(aInitPosition, aInitRotation);
    }

    void Update()
    {
        if(gameObject.transform.position.y <= -6)
        {
            aPlayerManager.playerDied(aPlayerNumber);
        }
    }

    public void setPlayer(int pNumber, PlayerManager pPlayerManager)
    {
        aPlayerNumber = pNumber;
        aPlayerManager = pPlayerManager;
    }
}
