using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallAttack : Attack
{
    private GameObject playerAttacking;
    public GameObject fireballPrefab;
    public GameObject Hand;

    public int damageCoef;
    public float expulsionCoef;
    public float speed;
    public float TotalTime;
    private float time;
    private GameObject fireball;
    private Rigidbody fireballRb;
    private int stacks =0;
    private Vector3 ExpulsionDirection;
    private Vector3 FireballDirection;
    private AttackHitBox FireballHitBox;

    private float tempExpulsion;
    private int tempDamage;
    public void AddStack()
    {
        if (stacks == 3)
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
        else if (playerAttacking != pPlayer)
        {
            time = TotalTime;
            float ExpulsionCoef = pPlayerData.getExpulsionCoef();
            pPlayerData.takeDamage(tempDamage);
            ExpulsionDirection = pPlayer.transform.position - fireball.transform.position;
            ExpulsionDirection = ExpulsionDirection.normalized;

            pPlayer.GetComponent<PlayerMovement>().ExpulsePlayer(ExpulsionDirection, ExpulsionCoef * tempExpulsion);
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
        tempDamage = Damage + damageCoef * stacks;
        tempExpulsion = Expulsion + expulsionCoef * stacks;
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
