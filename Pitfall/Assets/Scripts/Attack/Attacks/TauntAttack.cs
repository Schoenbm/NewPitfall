using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauntAttack : Attack
{
    private GameObject playerTaunting;
    private PlayerData playerData;
    private PlayerMovement playerMovement;
    private int currentHealth;

    private bool DidTaunt;
    private float playerOriginalWeight;
    private float playerOriginalSpeed;
    private float time;
    public float buffTime;
    public float speedBonus;
    public float weightBonus;

    override
    public void Touch(GameObject pPlayer)
    {
        Debug.Log("????");
    }


    override
    public void Execute()
    {
        this.currentHealth = this.playerData.getHealth();
        time = 0;
        DidTaunt = true;
        playLaunchSe();
        playerData.Weight = playerData.Weight * 2;
        Debug.Log("playerDataWeight : " + playerData.Weight);
        playerMovement.speed = 0;
        setMoving(true);
        this.PlayerAnimator.SetBool("Taunt", true);
    }

    private void Start()
    {
        DidTaunt = false;
        playerTaunting = this.transform.parent.transform.parent.gameObject;
        playerData = playerTaunting.GetComponent<PlayerData>();
        playerOriginalWeight = playerData.Weight;
        playerMovement = playerTaunting.GetComponent<PlayerMovement>();
        playerOriginalSpeed = playerMovement.speed;
    }

    private void Update()
    {
        if (this.getMoving())
        {
            time += Time.deltaTime;
            if (this.time >= this.buffTime)
            {
                this.setMoving(false);
                this.playerMovement.speed = playerOriginalSpeed;
                this.playerData.Weight = playerOriginalWeight;
            }
        }

        if (this.PlayerAnimator.GetBool("Taunt"))
        {
            if (currentHealth != playerData.getHealth())
            {
                this.playTouchingSe();
                this.setMoving(true);
                this.PlayerAnimator.SetBool("Taunt", false);
                this.playerData.healDamage(currentHealth - playerData.getHealth());
                DidTaunt = false;
                this.playerMovement.speed = this.playerOriginalSpeed + this.speedBonus;
                this.playerData.Weight =this.playerOriginalWeight + this.weightBonus;
            }

        }

        if (DidTaunt && !this.PlayerAnimator.GetBool("Taunt"))
        {
            DidTaunt = false;
            this.playerMovement.speed = playerOriginalSpeed;
            this.playerData.Weight = playerOriginalWeight;
        }
    }
}
