using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallAttack : Attack
{
    private GameObject playerAttacking;
    public GameObject fireballPrefab;
    public GameObject Staff;

    public float animationTime;
    private bool DuringAnimation;

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

    private AudioSource touchSe;
    private float Pitch;
    private float Volume;

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
            //ExpulsionDirection = pPlayer.transform.position - fireball.transform.position;
            //ExpulsionDirection.y = 0;
            //ExpulsionDirection = ExpulsionDirection.normalized;

            pPlayer.GetComponent<PlayerMovement>().ExpulsePlayer(FireballDirection, ExpulsionCoef * tempExpulsion);
        }
    }
    override
    public void Execute()
    {
        if (fireball != null)
        {
            Destroy(fireball);
            setMoving(false);
        }
        this.PlayerAnimator.SetBool("Fireball", true);
        time = 0;
        DuringAnimation = true;
    }

    public void popFireball()
    {
        this.PlayerAnimator.SetBool("Fireball", true);
        time = 0;
        fireball = Instantiate(fireballPrefab, Staff.transform.position, Staff.transform.localRotation);

        FireballHitBox = fireball.GetComponent<AttackHitBox>();
        fireballRb = fireball.GetComponent<Rigidbody>();
        FireballDirection.x = Mathf.Sin(playerAttacking.transform.eulerAngles.y * Mathf.Deg2Rad);
        FireballDirection.z = Mathf.Cos(playerAttacking.transform.eulerAngles.y * Mathf.Deg2Rad);
        fireballRb.velocity = FireballDirection * (speed - 5 * stacks);
        FireballHitBox.Activate(true);
        FireballHitBox.SetAttack(this);
        Vector3 vFireballScale = new Vector3(1 + 0.5f * stacks, 1 + 0.5f * stacks, 1 + 0.5f * stacks);
        fireball.transform.localScale = vFireballScale;

        setMoving(true);
        Debug.Log("stack = " + stacks);
        tempDamage = Damage + damageCoef * stacks;
        tempExpulsion = Expulsion + expulsionCoef * stacks;

        Pitch = 1.6f - 0.2f * stacks;
        Volume = 0.55f + 0.15f * stacks;
        this.touchSe = this.getTouchingSe();
        touchSe.pitch = Pitch;
        touchSe.volume = Volume;

        this.stacks = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (DuringAnimation)
        {
            if(time > animationTime)
            {
                setMoving(true);
                DuringAnimation = false;
                time = 0;
                playLaunchSe();
                popFireball();
            }
            else
            {
                time += Time.deltaTime;
            }
        }

        if (getMoving())
        {
            time += Time.deltaTime;
            if (time >= TotalTime)
            {
                time = 0;
                Destroy(fireball);
                setMoving(false);
            }
        }
    }

    private void Start()
    {
        playerAttacking = this.transform.parent.transform.parent.gameObject;
        DuringAnimation = false;
    }
}
