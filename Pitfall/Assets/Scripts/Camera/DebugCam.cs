using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DebugCam : MonoBehaviour
{
    public Rigidbody rbP1;
    public Rigidbody rbP2;
    private Vector3 PosCentrale;
    public Vector3 PosCameraGauche;
    public Vector3 PosCameraDroite;
    public CinemachineVirtualCamera vcam;

    private void Start()
    {
        PosCameraGauche = new Vector3(0f, 0f, -45f);
        PosCameraDroite = new Vector3(0f, 0f, 45f);
    }

    private void Update()
    {

        //Debug.Log(SontVisibles());

        if (!(rbP1 == null) && !(rbP2 == null))
        {
            transform.position = (rbP1.position + rbP2.position) / 2;
            PosCentrale = transform.position;
        }

        //ZOOM

        if (SontVisibles() == true)
        {
            vcam.m_Lens.FieldOfView -= 0.05f;

            PosCameraGauche.z += 0.125f;

            PosCameraDroite.z -= 0.125f;

        }

        //DEZOOM

        else if (SontVisibles() == false)
        {
            vcam.m_Lens.FieldOfView += 0.1f;

            PosCameraGauche.z -= 0.25f;

            PosCameraDroite.z += 0.25f;

        }

    }

    private bool SontVisibles()
    {
        float Dist2Joueurs;
        float LargeurCamera;

        Dist2Joueurs = Mathf.Sqrt(Mathf.Pow((rbP2.position.x - rbP1.position.x), 2) + Mathf.Pow((rbP2.position.z - rbP1.position.z), 2));
        LargeurCamera = Mathf.Sqrt(Mathf.Pow((PosCameraDroite.x - PosCameraGauche.x), 2) + Mathf.Pow((PosCameraDroite.z - PosCameraGauche.z), 2));

        Debug.Log("LargeurCamera");
        Debug.Log(LargeurCamera);
        Debug.Log("Dist2Joueurs");
        Debug.Log(Dist2Joueurs);


        if (Dist2Joueurs > LargeurCamera)
        {
            return false;
        }

        return true;

    }
}
