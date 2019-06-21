using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public Attack[] Attacks;
    public bool IsController;
    private float[] AttacksCds;
    private float[] CurrentAttacksCds;
    private float[] AttacksPressureTime;
    private string[] ButtonsName;

    // Start is called before the first frame update
    void Start()
    {
        int i = Attacks.Length;
        AttacksCds = new float[i];
        CurrentAttacksCds = new float[i];
        ButtonsName = new string[i];

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

        if (IsController)
        {
            Debug.Log("ControllerSet");
            ButtonsName[0] = "ControllerAttack";
            ButtonsName[1] = "ControllerCapacity1";
            ButtonsName[2] = "ControllerCapacity2";
        }
        else
        {
            ButtonsName[0] = "Attack";
            ButtonsName[1] = "Capacity1";
            ButtonsName[2] = "Capacity2";
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

        for (int k = 0; k < 3; k++)
        {
            if (this.GetButtonDown(ButtonsName[k]) && CurrentAttacksCds[k] <= 0)
            {
                Attacks[k].Execute();
                CurrentAttacksCds[k] = AttacksCds[k];
            }
        }
    }

    bool GetButtonDown(string pButtonName)
    {
        if (pButtonName == "ControllerAttack" || pButtonName == "ControllerCapacity1")
        {
            return (Input.GetAxis(pButtonName) > 0);
        }
        else
        {
            return Input.GetButtonDown(pButtonName);
        }
    }
}
