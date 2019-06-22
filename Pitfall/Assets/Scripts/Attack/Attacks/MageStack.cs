using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageStack : CircularAttack
{
    public FireBallAttack FireBallAttackOfPlayer;
    private Vector3 StackedExpulsionDirection;

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
            this.FireBallAttackOfPlayer.AddStack();
            float ExpulsionCoef = pPlayerData.getExpulsionCoef();
            pPlayerData.takeDamage(Damage);
            StackedExpulsionDirection = Hand.transform.position - this.transform.position;
            StackedExpulsionDirection = StackedExpulsionDirection / Radius;
            pPlayer.GetComponent<PlayerMovement>().ExpulsePlayer(StackedExpulsionDirection, ExpulsionCoef * Expulsion);
        }
    }

}
