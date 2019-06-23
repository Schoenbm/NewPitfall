using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private bool RotationOn = true;

    public bool getRotationOn()
    {
        return RotationOn;
    }

    public void setRotationOn(bool pBool)
    {
        RotationOn = pBool;
    }
}
