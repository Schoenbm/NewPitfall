
using UnityEngine;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{

    public Rigidbody rb;
    public float speed;
    public bool aMouse;
    public bool IsWalking = false;
    Animator aAnimator;

    private bool aTouchGroundPlayer1 = true;
    private bool aTouchGroundPlayer2 = true;


    public void Start()
    {
        aAnimator = GetComponent<Animator>();
    }

    public void MovePlayer(Vector3 pDirection, float pSpeed)
    {
        rb.MovePosition(pDirection * pSpeed + rb.position);
        Debug.Log(IsWalking);
    }

    public void ExpulsePlayer(Vector3 pDirection, float pForce)
    {
        pForce = pForce / 10;
        Debug.Log("Force is" + pForce);
        rb.AddForce( pDirection* pForce, ForceMode.VelocityChange);
    }

    void Update()
    {
//-----------------Joueur 1---------------------------------------------------

        if (aMouse)
        {
            Vector3 vInputDirection = Vector3.zero;
            vInputDirection.x = Input.GetAxis("Horizontal");
            vInputDirection.z = Input.GetAxis("Vertical");

            bool hasHorizontalInput = !Mathf.Approximately(vInputDirection.x, 0f);
            bool hasVerticalInput = !Mathf.Approximately(vInputDirection.z, 0f);
            IsWalking = hasHorizontalInput || hasVerticalInput;
            aAnimator.SetBool("IsWalking", IsWalking);

            if (vInputDirection.magnitude > 1)
            {
                vInputDirection.Normalize();
            }
            rb.MovePosition(rb.position + vInputDirection * speed);

        }


// ------------------- Joueur 2 -----------------------------------------------
        //Mouvement de base
        if (!aMouse)
        {
            Vector3 vInputDirection = Vector3.zero;
            vInputDirection.x = Input.GetAxis("ControllerHorizontal") * speed;
            vInputDirection.z = Input.GetAxis("ControllerVertical") * speed;

            bool hasHorizontalInput = !Mathf.Approximately(vInputDirection.x, 0f);
            bool hasVerticalInput = !Mathf.Approximately(vInputDirection.z, 0f);
            IsWalking = hasHorizontalInput || hasVerticalInput;
            aAnimator.SetBool("IsWalking", IsWalking);

            rb.MovePosition(rb.position + vInputDirection);
        }
    }

}
