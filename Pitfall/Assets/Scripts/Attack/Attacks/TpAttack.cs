using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpAttack : Attack
{
    private GameObject playerAttacking;
    public int Distance;
    public float timeBeforeTp;
    private float time;

    private float HaloMaxScale;
    private float HaloTime;

    private Vector3 ExpulsionDirection;
    private Vector3 TpDirection;

    private bool IsMoving;
    private bool DidTp = false;

    public AttackHitBox Halo;
    // Start is called before the first frame update
    void Start()
    {
        TpDirection = new Vector3();
        playerAttacking = this.transform.parent.transform.parent.gameObject;
    }

    override
    public void Touch(GameObject pPlayer)
    {
        PlayerData pPlayerData = pPlayer.GetComponent<PlayerData>();
        if (pPlayerData == null)
        {
            Debug.Log("Player has no PlayerData");
            Debug.Log(pPlayer.tag);
        }
        else if (playerAttacking != pPlayer)
        {
            float ExpulsionCoef = pPlayerData.getExpulsionCoef();
            pPlayerData.takeDamage(Damage);
            ExpulsionDirection = pPlayer.transform.position - playerAttacking.transform.position;
            ExpulsionDirection = ExpulsionDirection.normalized;
            pPlayer.GetComponent<PlayerMovement>().ExpulsePlayer(ExpulsionDirection, ExpulsionCoef * Expulsion);
        }
    }

    public void Tp()
    {
        DidTp = true;
        HitBox = Instantiate(Halo, this.transform.position, this.transform.rotation);
        float yAngle = this.gameObject.transform.eulerAngles.y * Mathf.Deg2Rad;
        TpDirection = new Vector3(Mathf.Sin(yAngle), 0, Mathf.Cos(yAngle));
        playerAttacking.GetComponent<PlayerMovement>().TpPlayer(TpDirection, Distance);
    }

    override
    public void Execute()
    {
        IsMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMoving)
        {
            if(time> timeBeforeTp + HaloTime)
            {
                time = 0;
                IsMoving = false;
                DidTp = false;
                Destroy(HitBox);
            }
            else if (DidTp)
            {
                Halo.transform.localScale = new Vector3(vFactor + 1, 1, vFactor + 1);
            }
            else if(time > timeBeforeTp)
            {
                Tp();
            }
            time += Time.deltaTime;
        }
    }
}
