using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallAttack : Attack
{
    private GameObject playerAttacking;
    public GameObject fireballPrefab;
    public GameObject Hand;
    public float speed;
    public float TotalTime;
    private float time;
    private GameObject fireball;
    private Rigidbody fireballRb;
    private int stacks =0;
    private Vector3 ExpulsionDirection;
    private Vector3 FireballDirection;
    private AttackHitBox FireballHitBox;

    public void AddStack()
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
        else if(playerAttacking != pPlayer)
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
        fireball = Instantiate(fireballPrefab, Hand.transform.position, Hand.transform.rotation);

        FireballHitBox = fireball.GetComponent<AttackHitBox>();
        fireballRb = fireball.GetComponent<Rigidbody>();
        FireballDirection.x = Mathf.Cos(Hand.transform.eulerAngles.y * Mathf.Deg2Rad);
        FireballDirection.z = -Mathf.Sin(Hand.transform.eulerAngles.y * Mathf.Deg2Rad);
        fireballRb.velocity = FireballDirection * (speed - 5 * stacks);
        FireballHitBox.Activate(true);
        FireballHitBox.SetAttack(this);
        Vector3 vFireballScale = new Vector3(1 + 0.5f * stacks, 1 + 0.5f * stacks, 1 + 0.5f * stacks);
        fireball.transform.localScale = vFireballScale;

        setMoving(true);
        Debug.Log("stack = " + stacks);
        this.stacks = 0;
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

    private void Start()
    {
        playerAttacking = this.transform.parent.transform.parent.gameObject;
    }
}
