using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallAttack : Attack
{
    public GameObject fireballPrefab;
    public float speed;
    public float TotalTime;
    private float time;
    private GameObject fireball;
    private Rigidbody fireballRb;
    public int stacks =0;
    private Vector3 ExpulsionDirection;
    private Vector3 FireballDirection;
    private AttackHitBox FireballHitBox;

    private void AddStack()
    {
        if (stacks == 2)
        {
            return;
        }
        stacks++;        
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
        else
        {
            time = TotalTime;
            float ExpulsionCoef = pPlayerData.getExpulsionCoef();
            pPlayerData.takeDamage(Damage + 5 * stacks);
            ExpulsionDirection = fireball.transform.position - this.transform.position;
            ExpulsionDirection = ExpulsionDirection.normalized;
            pPlayer.GetComponent<PlayerMovement>().ExpulsePlayer(ExpulsionDirection, ExpulsionCoef * (Expulsion + 10 * stacks));
        }
    }

    override
    public void Execute()
    {
        time = 0;
        fireball = Instantiate(fireballPrefab, this.transform.position, this.transform.rotation);

        Debug.Log("Instantiate" + fireball.transform.position);
        FireballHitBox = fireball.GetComponent<AttackHitBox>();
        fireballRb = fireball.GetComponent<Rigidbody>();
        FireballDirection = this.transform.forward;
        fireballRb.velocity = FireballDirection * speed;
        FireballHitBox.Activate(true);
        FireballHitBox.SetAttack(this);
        Vector3 vFireballScale = new Vector3(1 + 0.2f * stacks, 1 + 0.2f * stacks, 1 + 0.2f * stacks);
        fireball.transform.localScale = vFireballScale;

        setMoving(true);   
    }

    // Update is called once per frame
    void Update()
    {
        if (getMoving())
        {
            time += Time.deltaTime;
            if (time >= TotalTime)
            {
                Destroy(fireball);
                setMoving(false);
            }
        }
    }
}
