using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    public Animator PlayerAnimator;
    private bool CanRecast;
    private bool Recast;
    public int Damage;
    public float Cd;
    public float Expulsion;
    private bool IsMoving = false;
    private bool Canalisation = false;

    public AudioSource LaunchingSe;
    public AudioSource TouchingSe;
    public AttackHitBox HitBox;

    public void touchWall(GameObject pWall)
    {
        pWall.GetComponent<WallFall>().takeDamage(Damage);
        Debug.Log("take damage" + pWall.GetComponent<WallFall>().health);
    }

    public void playLaunchSe()
    {
        LaunchingSe.Play();
    }

    public void playTouchingSe()
    {
        TouchingSe.Play();
    }

    public AudioSource getTouchingSe()
    {
        return TouchingSe;
    }

    public AudioSource getLaunchSe()
    {
        return LaunchingSe;
    }

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

    public bool GetActivateHitBox()
    {
        return this.HitBox.getActivate();
    }

    public abstract void Touch(GameObject pPlayer);

    public abstract void Execute();

    public void ExecuteTimer() { }

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
