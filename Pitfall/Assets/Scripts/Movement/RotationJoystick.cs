using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationJoystick : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vLookTarget = Vector3.zero;

        vLookTarget.x = Input.GetAxis("ControllerHorizontalRight");
        vLookTarget.z = Input.GetAxis("ControllerVerticalRight");

        float angle = Mathf.Atan2(vLookTarget.x, vLookTarget.z) * Mathf.Rad2Deg;
        this.transform.eulerAngles = new Vector3(0, angle, 0);
        //Debug.Log(angle);
    }
}
