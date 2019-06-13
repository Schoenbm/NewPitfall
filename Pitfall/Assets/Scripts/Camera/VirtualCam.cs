using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCam : MonoBehaviour
{
    public CinemachineVirtualCamera aVCam;

    private Transform[] aTransform;
    private int aNumberPlayers;
    private Vector3 PosCameraGauche;
    private Vector3 PosCameraDroite;
    private float aMaxDistance = 0;
    private Vector3 aCenter = Vector3.zero;

    private void Update()
    {
        aMaxDistance = 0;
        float vDistanceFromCenter;
        Vector3 vPos = Vector3.zero;
        for(int k = 0; k < aNumberPlayers; k++)
        {
            if(aTransform[k] != null)
            {
                vPos += aTransform[k].position;
                vDistanceFromCenter = (aTransform[k].position - aCenter).magnitude;
                if (vDistanceFromCenter > aMaxDistance)
                {
                    aMaxDistance = vDistanceFromCenter;
                }
            }
        }
        aCenter = vPos / aNumberPlayers;
        this.transform.position = aCenter;

        if (AllPlayerClose())
        {
            aVCam.m_Lens.FieldOfView -= HowFastYouZoom();
        }

        if (!AllPlayersVisible())
        {
            aVCam.m_Lens.FieldOfView += HowFastYouDezoom();
        }
    }

    bool AllPlayersVisible()
    {
        return (aMaxDistance < aVCam.m_Lens.FieldOfView);
    }

    float HowFastYouDezoom()
    {
        return (Mathf.Exp(aMaxDistance / aVCam.m_Lens.FieldOfView) / 16);
    }

    float HowFastYouZoom()
    {
        return (Mathf.Log(aVCam.m_Lens.FieldOfView / aMaxDistance) / 16);
    }

    bool AllPlayerClose()
    {
        if (aVCam.m_Lens.FieldOfView < 16)
        {
            return false;
        }
        return (aMaxDistance * 2 < aVCam.m_Lens.FieldOfView);
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
