using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScript : MonoBehaviour
{
    private Vector3 aInitPosition;
    private Quaternion aInitRotation;

    private void Start()
    {
        aInitPosition = this.transform.position;
        aInitRotation = this.transform.rotation;
    }

    private void Reset()
    {
        this.transform.SetPositionAndRotation(aInitPosition, aInitRotation);
    }
}
