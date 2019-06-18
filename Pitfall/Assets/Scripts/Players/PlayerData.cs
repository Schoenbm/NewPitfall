using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float Weight;
    private int Health = 0;

    public void takeDamage(int Damage)
    {
        Health += Damage;
    }

    public void healDamage(int Healing)
    {
        Health += Healing;
        if (Health < 0)
        {
            Health = 0;
        }
    }

    public int getHealth()
    {
        return Health;
    }

    public float getExpulsionCoef()
    {
        return  2 + (Health / Weight);
    }
}
