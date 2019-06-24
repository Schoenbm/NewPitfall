using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int aPlayerNumber;
    private PlayerManager aPlayerManager;
    private Vector3 aInitPosition;
    private Quaternion aInitRotation;

    private int aPercentageDamage;

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
        // si c'est 0 c'est clavier, sinon c'est manette

        aPlayerNumber = pNumber;
        aPlayerManager = pPlayerManager;
        this.gameObject.GetComponentInChildren < AttackManager>().setControl(pNumber);
        this.gameObject.GetComponent<PlayerMovement>().setPLayerNumber(pNumber);

        if (pNumber == 0)
        {
            Destroy(this.gameObject.GetComponent<RotationJoystick>());
        }
        else
        {
            Destroy(this.gameObject.GetComponent<RotationCursor>());
            this.gameObject.GetComponent<RotationJoystick>().setJoystickNumber(pNumber);
        }
        Debug.Log("pNumber : " + pNumber);
    }
}
