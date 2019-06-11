using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCam : MonoBehaviour
{
    private Transform[] aTransform;
    private int aNumberPlayers;

    private int aMaxDistance;

    private void Update()
    {
        Vector3 vPos = Vector3.zero;
        for(int k = 0; k < aNumberPlayers; k++)
        {
            if(aTransform[k] != null)
            {
                vPos += aTransform[k].position;
            }
        }
        this.transform.position = vPos / aNumberPlayers;
    }

    public void setPlayer(Transform pTransform, int pPlayerNumber)
    {
        aTransform[pPlayerNumber] = pTransform;
    }


    public void setNumberPlayers(int pNumber)
    {
        aNumberPlayers = pNumber;
        aTransform = new Transform[aNumberPlayers];
    }
}
