using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    private bool Recast;
    public int Damage;
    public float Cd;
    public float Expulsion;
    private bool IsMoving = false;
    private bool Canalisation = false;

    public AttackHitBox HitBox;

    public bool getCanalisation()
    {
        return Canalisation;
    }

    public void setCanalisation(bool pBool)
    {
        Canalisation = pBool;
    }

    public void ActivateHitBox(bool pBool)
    {
        this.HitBox.Activate(pBool);
    }

    public abstract void Touch(GameObject pPlayer);

    public abstract void Execute();

    public bool getMoving()
    {
        return IsMoving;
    }

    public void setMoving(bool pBool)
    {
        IsMoving = pBool;
    }
}
