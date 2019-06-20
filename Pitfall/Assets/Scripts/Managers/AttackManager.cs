using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public Attack[] Attacks;

    private float[] AttacksCds;
    private float[] CurrentAttacksCds;
    private float[] AttacksPressureTime;

    // Start is called before the first frame update
    void Start()
    {
        int i = Attacks.Length;
        AttacksCds = new float[i];
        CurrentAttacksCds = new float[i];

        if (i == 3)
        {
            for(int k = 0; k < 3; k++)
            {
                AttacksCds[k] = Attacks[k].Cd;
                CurrentAttacksCds[k] = 1;
            }
        }
        else
        {
            Debug.Log("error, not enough attacks");
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int k = 0; k < 3; k++)
        {
            if (CurrentAttacksCds[k] > 0)
            {
                CurrentAttacksCds[k] -= Time.deltaTime;
            }
        }

        if (Input.GetButtonDown("Attack") && CurrentAttacksCds[0] <= 0)
        {
            Attacks[0].Execute();
            CurrentAttacksCds[0] = AttacksCds[0];
        }

        if (Input.GetButtonDown("Capacity1") && CurrentAttacksCds[1] <= 0)
        {
            Attacks[1].Execute();
            CurrentAttacksCds[1] = AttacksCds[1];
        }

        if (Input.GetButtonDown("Capacity2") && CurrentAttacksCds[2] <= 0)
        {
            Attacks[2].Execute();
            CurrentAttacksCds[2] = AttacksCds[2];
        }
    }
}
