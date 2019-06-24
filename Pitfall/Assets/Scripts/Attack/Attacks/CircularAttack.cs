using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularAttack : Attack
{
    private GameObject tempPlayerTouched;
    public int DamageReturn;
    public int ExpulsionReturn;
    public float timeReturn;
    private float time;

    private Vector3 ExpulsionDirection;

    public Vector3 getExpulsionDirection()
    {
        return ExpulsionDirection;
    }

    private void Start()
    {
        ExpulsionDirection = new Vector3();
        tempPlayerTouched = this.gameObject;
        this.ActivateHitBox(false);
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
            ExpulsionDirection = pPlayer.transform.position - this.transform.position;
            ExpulsionDirection.y = 0;
            ExpulsionDirection = ExpulsionDirection.normalized;

            float ExpulsionCoef = pPlayerData.getExpulsionCoef();

            if (this.tempPlayerTouched == pPlayer)
            {
                pPlayerData.takeDamage(DamageReturn);
                pPlayer.GetComponent<PlayerMovement>().ExpulsePlayer(ExpulsionDirection, ExpulsionCoef * ExpulsionReturn);
            }
            else
            {
                pPlayerData.takeDamage(Damage);
                pPlayer.GetComponent<PlayerMovement>().ExpulsePlayer(ExpulsionDirection, ExpulsionCoef * Expulsion);
                tempPlayerTouched = pPlayer;
            }
        }
    }

    override
    public void Execute()
    {
        time = 0;
        playLaunchSe();
        this.PlayerAnimator.SetBool("Attacking", true);
        this.ActivateHitBox(true);
        setMoving(true);
    }

    private void Update()
    {
        if (this.GetActivateHitBox() && !this.PlayerAnimator.GetBool("Attacking"))
        {
            this.tempPlayerTouched = this.gameObject;
            this.ActivateHitBox(false);
        }
        
        if (getMoving())
        {
            time += Time.deltaTime;

            if (time >= timeReturn)
            {
                if (this.tempPlayerTouched != this.gameObject)
                {
                    this.getLaunchSe().pitch = 1.2f;
                    this.playLaunchSe();
                }
                this.getLaunchSe().pitch = 0.8f;
                this.playLaunchSe();
                this.getLaunchSe().pitch = 1f;


                time = 0;
                this.setMoving(false);
            }
        }
    }
}
