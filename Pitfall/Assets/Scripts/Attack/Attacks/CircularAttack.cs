using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularAttack : Attack
{
    public float Radius;
    public GameObject Hand;
    private Vector3 originalHandPos;
    private Vector3 originalHandRot;

    public float TotalTime;
    private float time;
    private float cosTime;
    private float sinTime;
    private float tanTime;
    private Vector3 newPosition;
    private Vector3 newRotation;
    private Vector3 ExpulsionDirection;

    public Vector3 getExpulsionDirection()
    {
        return ExpulsionDirection;
    }

    private void Start()
    {
        this.setCanalisation(true);
        this.setCanRecast(true);
        newRotation = new Vector3(90, 0, 0);
        newPosition = new Vector3(-Radius, 0, 0);
        ExpulsionDirection = new Vector3();
    }

    override
    public void Touch(GameObject pPlayer)
    {
        PlayerData pPlayerData = pPlayer.GetComponent<PlayerData>();
        if (pPlayerData == null)
        {
            Debug.Log("Player has no PlayerData");
            Debug.Log(pPlayer.tag);
        }
        else
        {
            float ExpulsionCoef = pPlayerData.getExpulsionCoef();
            pPlayerData.takeDamage(Damage);
            ExpulsionDirection = Hand.transform.position - this.transform.position;
            ExpulsionDirection.y = 0;
            ExpulsionDirection = ExpulsionDirection.normalized;
            pPlayer.GetComponent<PlayerMovement>().ExpulsePlayer(ExpulsionDirection, ExpulsionCoef * Expulsion);
        }
    }

    override
    public void Execute()
    {
        playLaunchSe();
        originalHandPos = Hand.transform.localPosition;
        originalHandRot = Hand.transform.localEulerAngles;
        time = 0;
        newPosition = new Vector3(-1,0,0);
        setMoving(true);
    }

    private void Update()
    {
        if (getMoving())
        {
            ActivateHitBox(true);
            cosTime = Mathf.Cos((Mathf.PI * time) / TotalTime);
            sinTime = Mathf.Sin((Mathf.PI * time) / TotalTime);
            newPosition.x = sinTime * Radius;
            newPosition.z = cosTime * Radius;
            newRotation.z = Mathf.Rad2Deg * Mathf.Atan2(cosTime , sinTime) - 90;
            Hand.transform.localPosition = newPosition;
            Hand.transform.localEulerAngles = newRotation;
            time += Time.deltaTime;

            if (getRecast() && sinTime < 0){
                Debug.Log("Recast");
                setRecast(false);
            }
            else if ( sinTime < 0)
            {
                Hand.transform.localPosition = originalHandPos;
                Hand.transform.localEulerAngles = originalHandRot;
                setMoving(false);
                ActivateHitBox(false);
            }

        }
    }
}
