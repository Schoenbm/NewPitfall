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

        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, Mathf.Atan2(vLookTarget.x, vLookTarget.z) * Mathf.Rad2Deg, this.transform.eulerAngles.z);
        //Quaternion targetRotation = Quaternion.LookRotation(vLookTarget - this.transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    }
}
