using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    public Attack OriginAttack;
    private bool Activated;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && Activated)
        {
            Debug.Log("Touch Player");
            OriginAttack.Touch(other.gameObject);
            OriginAttack.playTouchingSe();
        }
    }

    public void Activate(bool pActivate)
    {
        Activated = pActivate;
    }

    public bool getActivate()
    {
        return Activated;
    }

    public void SetAttack(Attack pOriginAttack)
    {
        OriginAttack = pOriginAttack;
    }
}
