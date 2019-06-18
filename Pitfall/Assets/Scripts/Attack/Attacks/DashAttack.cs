using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttack : Attack
{
    public float DashTime = 3f;
    public float DashPreparingTime = 1f;
    public float DashSpeed = 1.1f;
    public float exponentialFactor;
    public GameObject Player;
    private PlayerMovement aPlayerMovement;
    private float aPlayerSpeed;
    private float CurrentDashTime = 0f;
    private Vector3 DashDirection;
    private Vector3 DashExpulsionDirection;
    private void Start()
    {
        aPlayerMovement = Player.GetComponent<PlayerMovement>();
        aPlayerSpeed = aPlayerMovement.speed;
    }

    /*
     *     Vector3 vInputDashDir = Vector3.zero;
        vInputDashDir.z = pInputDashZ;
        vInputDashDir.x = pInputDashX;
        rb.AddForce(p_dashForce * vInputDashDir * Time.deltaTime, ForceMode.VelocityChange);
        yield return new WaitForSeconds(p_dashTime);
        rb.velocity = Vector3.zero;
     * */

    override 
    public void Touch(GameObject pPlayer)
    {
        PlayerData pPlayerData = pPlayer.GetComponent<PlayerData>();
        if(pPlayerData == null)
        {
            Debug.Log("Player has no PlayerData");
            Debug.Log(pPlayer.tag);
        }
        else
        {
            float ExpulsionCoef = pPlayerData.getExpulsionCoef();
            pPlayerData.takeDamage(Damage);
            DashExpulsionDirection = new Vector3();
            DashExpulsionDirection = pPlayer.transform.position - aPlayerMovement.transform.position;
            DashExpulsionDirection.Normalize();
            pPlayer.GetComponent<PlayerMovement>().ExpulsePlayer(DashExpulsionDirection, ExpulsionCoef * Expulsion);
        }
    }

    override
    public void Execute()
    {
        float yAngle = this.gameObject.transform.eulerAngles.y * Mathf.Deg2Rad;
        DashDirection = new Vector3(Mathf.Sin(yAngle), 0 , Mathf.Cos(yAngle));
        setMoving(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (getMoving())
        {
            aPlayerMovement.speed = 0;
            CurrentDashTime += Time.deltaTime;
            if (CurrentDashTime > DashTime)
            {
                aPlayerMovement.speed = aPlayerSpeed;
                setMoving(false);
                CurrentDashTime = 0f;
                ActivateHitBox(false);
            }
            if (CurrentDashTime > DashPreparingTime)
            {
                ActivateHitBox(true);
                aPlayerMovement.MovePlayer(DashDirection, DashSpeed - Mathf.Exp(CurrentDashTime) * exponentialFactor);
            }
        }
    }
}
