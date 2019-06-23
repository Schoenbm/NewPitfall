using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageStack : Attack
{
    public FireBallAttack FireBall;
    private Vector3 StackedExpulsionDirection;

    override
    public void Touch(GameObject pPlayer)
    {
        Debug.Log("Attack touch");
        PlayerData pPlayerData = pPlayer.GetComponent<PlayerData>();
        if (pPlayerData == null)
        {
            Debug.Log("Player has no PlayerData");
            Debug.Log(pPlayer.tag);
        }
        else
        {
            this.FireBall.AddStack();
            float ExpulsionCoef = pPlayerData.getExpulsionCoef();
            pPlayerData.takeDamage(Damage);
            StackedExpulsionDirection = pPlayer.transform.position - this.transform.position;
            StackedExpulsionDirection.y = 0;
            StackedExpulsionDirection = StackedExpulsionDirection.normalized;
            pPlayer.GetComponent<PlayerMovement>().ExpulsePlayer(StackedExpulsionDirection, ExpulsionCoef * Expulsion);
        }
    }

    override
    public void Execute()
    {
        playLaunchSe();
        this.PlayerAnimator.SetBool("Attacking", true);
        ActivateHitBox(true);
    }

    private void Update()
    {
        if (this.GetActivateHitBox() && !this.PlayerAnimator.GetBool("Attacking"))
        {
            this.ActivateHitBox(false);
        }
    }
}
