using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    private bool CanRecast;
    private bool Recast;
    public int Damage;
    public float Cd;
    public float Expulsion;
    private bool IsMoving;
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
    
    public void ExecuteTimer(float pTimer) { }

    public bool getMoving()
    {
        return IsMoving;
    }

    public void setMoving(bool pBool)
    {
        IsMoving = pBool;
    }

    public bool getCanRecast() { return CanRecast; }

    public bool setCanRecast(bool pCanRecast) { return CanRecast = pCanRecast; }

    public bool getRecast() { return Recast; }

    public bool setRecast(bool pRecast) { return Recast = pRecast; }
}
