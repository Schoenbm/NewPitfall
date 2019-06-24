using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationJoystick : Rotation
{
    public float speed;
    private string HoriAxis = "ControllerHorizontalRight";
    private string VertiAxis = "ControllerVerticalRight";
    private bool isSetUp = false;

    public void setJoystickNumber(int pNumber)
    {
        HoriAxis = HoriAxis + pNumber;
        Debug.Log(HoriAxis);
        VertiAxis = VertiAxis + pNumber;
        Debug.Log(VertiAxis);
        isSetUp = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!getRotationOn() || ! isSetUp)
        {
            return;
        }

        Vector3 vLookTarget = Vector3.zero;


        vLookTarget.x = Input.GetAxis(HoriAxis);
        vLookTarget.z = Input.GetAxis(VertiAxis);

        if (vLookTarget.x != 0 && vLookTarget.z != 0)
        {
            float angle = Mathf.Atan2(vLookTarget.z, vLookTarget.x) * Mathf.Rad2Deg;
           this.transform.eulerAngles = new Vector3(0, angle + 90, 0);
        }

    }
}
