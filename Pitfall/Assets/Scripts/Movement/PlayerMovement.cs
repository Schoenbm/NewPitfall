
using UnityEngine;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{

    public Rigidbody rb;
    public float speed;
    public float p_dashTime;
    public int p_dashForce;
    public bool aMouse;

    private bool aTouchGroundPlayer1 = true;
    private bool aTouchGroundPlayer2 = true;




    void Update()
    {
//-----------------Joueur 1---------------------------------------------------

        if (aMouse)
        {
            Vector3 vInputDirection = Vector3.zero;
            vInputDirection.x = Input.GetAxis("Horizontal");
            vInputDirection.z = Input.GetAxis("Vertical");

            if (vInputDirection.magnitude > 1)
            {
                vInputDirection.Normalize();
            }
            rb.MovePosition(rb.position + vInputDirection * speed);

            //Dash Attack

            if (Input.GetButton("Attack"))
            {
                StartCoroutine(Dash(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")));
            }

        }


// ------------------- Joueur 2 -----------------------------------------------
        //Mouvement de base
        if (!aMouse)
        {
            Vector3 vInputDirection = Vector3.zero;
            vInputDirection.x = Input.GetAxis("ControllerHorizontal") * speed;
            vInputDirection.z = Input.GetAxis("ControllerVertical") * speed;
            rb.MovePosition(rb.position + vInputDirection);

            //Dash Attack

            if (Input.GetButton("ControllerAttack"))
            {
                StartCoroutine(Dash( Input.GetAxis("ControllerVertical"), Input.GetAxis("ControllerHorizontal")));
            }
        }
    }

    /*
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Entered");
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }


    void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exited");
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    */


    IEnumerator Dash(float pInputDashZ, float pInputDashX)
    {
        Vector3 vInputDashDir = Vector3.zero;
        vInputDashDir.z = pInputDashZ;
        vInputDashDir.x = pInputDashX;
        rb.AddForce(p_dashForce * vInputDashDir * Time.deltaTime, ForceMode.VelocityChange);
        yield return new WaitForSeconds(p_dashTime);
        rb.velocity = Vector3.zero;
    }

}
