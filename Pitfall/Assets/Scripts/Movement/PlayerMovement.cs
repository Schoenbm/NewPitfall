
using UnityEngine;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{

    public Rigidbody rb;
    public Rotation playerRotation;
    public AttackManager playerAttackManager;
    public float speed;
    public bool aMouse;

    public bool IsWalking = false;
    private float timeExpulsed;
    Animator playerAnimator;

    //<<<<<<< Updated upstream
    //=======
    public void Start()
    {
        playerAnimator = GetComponent<Animator>();
        timeExpulsed = 0f;
    }
    public void MovePlayer(Vector3 pDirection, float pSpeed)
    {
        rb.MovePosition(pDirection * pSpeed + rb.position);
    }

    public void TpPlayer(Vector3 pDirection, float pDistance)
    {
        this.transform.position += pDirection * pDistance;
    }

    public void ExpulsePlayer(Vector3 pDirection, float pForce)
    {
        pForce = pForce / 10;
        Debug.Log("Force is" + pForce);
        rb.AddForce(pDirection * pForce, ForceMode.VelocityChange);

        float angle = Mathf.Atan2(pDirection.x, pDirection.z) * Mathf.Rad2Deg;

        if (pForce > 150)
        {
            timeExpulsed = 1.5f;
            this.playerRotation.setRotationOn(false);
            this.transform.eulerAngles = new Vector3(0, angle - 90, 0);
            this.playerAttackManager.setCanAttack(false);
            this.playerAnimator.SetBool("Expulsed", true);
        }
        else if (pForce > 100)
        {
            this.playerRotation.setRotationOn(false);
            this.transform.eulerAngles = new Vector3(0, angle - 90, 0);
            this.playerAttackManager.setCanAttack(false);
            timeExpulsed = 1.3f;
            this.playerAnimator.SetBool("Expulsed", true);
        }
        else if (pForce > 50f)
        {
            this.playerRotation.setRotationOn(false);
            this.transform.eulerAngles = new Vector3(0, angle - 90, 0);
            this.playerAttackManager.setCanAttack(false);
            timeExpulsed = 0.8f;
            this.playerAnimator.SetBool("Expulsed", true);
        }
        else if (pForce > 25f)
        {
            this.playerRotation.setRotationOn(false);
            this.transform.eulerAngles = new Vector3(0, angle - 90, 0);
            this.playerAttackManager.setCanAttack(false);
            timeExpulsed = 0.5f;
            this.playerAnimator.SetBool("Expulsed", true);
        }
    }

    void Update()
    {
        //----------------clavier--------------------------------------------------

        if (aMouse)
        {
            Vector3 vInputDirection = Vector3.zero;
            vInputDirection.x = Input.GetAxis("Horizontal");
            vInputDirection.z = Input.GetAxis("Vertical");

            //<<<<<<< Updated upstream
            //=======
            bool hasHorizontalInput = !Mathf.Approximately(vInputDirection.x, 0f);
            bool hasVerticalInput = !Mathf.Approximately(vInputDirection.z, 0f);
            IsWalking = hasHorizontalInput || hasVerticalInput;
            playerAnimator.SetBool("Run", IsWalking);

            //>>>>>>> Stashed changes
            if (vInputDirection.magnitude > 1)
            {
                vInputDirection.Normalize();
            }
            rb.MovePosition(rb.position + vInputDirection * speed);
        }
        // ------------------Manette-----------------------------------------------
        //Mouvement de base
        if (!aMouse)
        {
            Vector3 vInputDirection = Vector3.zero;
            vInputDirection.x = Input.GetAxis("ControllerHorizontal") * speed;
            vInputDirection.z = Input.GetAxis("ControllerVertical") * speed;

            //<<<<<<< Updated upstream
            //=======
            bool hasHorizontalInput = !Mathf.Approximately(vInputDirection.x, 0f);
            bool hasVerticalInput = !Mathf.Approximately(vInputDirection.z, 0f);
            IsWalking = hasHorizontalInput || hasVerticalInput;
            playerAnimator.SetBool("Run", IsWalking);

            //>>>>>>> Stashed changes
            rb.MovePosition(rb.position + vInputDirection);
        }

        if (this.timeExpulsed > 0)
        {
            this.timeExpulsed -= Time.deltaTime;

            if (this.timeExpulsed < 0)
            {
                Debug.Log(" on enleve tout");
                this.playerAttackManager.setCanAttack(true);
                this.playerRotation.setRotationOn(true);
                this.playerAnimator.SetBool("Expulsed", false);
            }
        }
    }

}
