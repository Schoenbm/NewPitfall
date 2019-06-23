using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpAttack : Attack
{
    private GameObject playerAttacking;
    public int MaxDistance;
    private float Distance;
    public float timeBeforeTp;
    public int rotationSpeed;
    private float time;

    public GameObject HaloPrefab;
    private GameObject Halo;
    public float HaloMaxScale;
    public float HaloTime;

    private Vector3 ExpulsionDirection;
    private Vector3 TpDirection;

    private bool DidTp = false;

    private AttackHitBox HaloHitBox;

    // Start is called before the first frame update
    void Start()
    {
        HaloMaxScale = HaloMaxScale - 1;
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
            ExpulsionDirection.y = 0;
            ExpulsionDirection = ExpulsionDirection.normalized;
            pPlayer.GetComponent<PlayerMovement>().ExpulsePlayer(ExpulsionDirection, ExpulsionCoef * Expulsion);
        }
    }

    public void Tp()
    {

        float yAngle = this.gameObject.transform.eulerAngles.y * Mathf.Deg2Rad;
        TpDirection = new Vector3(Mathf.Sin(yAngle), 0, Mathf.Cos(yAngle));

        Ray ray = new Ray(transform.position, TpDirection);
        RaycastHit vRaycastHit;
        float vDistance = 30;

        if (Physics.Raycast(ray, out vRaycastHit))
        {
            if (vRaycastHit.collider.tag == "Wall")
            {
                Debug.Log("TouchWall");
                vDistance = (vRaycastHit.transform.position - this.transform.position).magnitude - 3;
                Debug.Log(vDistance);
            }
        }
        Distance = Mathf.Min(vDistance, MaxDistance);
        playerAttacking.GetComponent<PlayerMovement>().TpPlayer(TpDirection, Distance);
        DidTp = true;
        Halo = Instantiate(HaloPrefab, this.transform);
        Halo.transform.localScale = new Vector3(0, 0, 0);
        Halo.transform.eulerAngles = new Vector3(90, 0, 0);
        HitBox = Halo.GetComponent<AttackHitBox>();
        HitBox.SetAttack(this);
        HitBox.Activate(true);
    }

    override
    public void Execute()
    {
        playLaunchSe();
        setMoving(true);
        this.PlayerAnimator.SetBool("TP", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (getMoving())
        {
            if (time > timeBeforeTp + HaloTime)
            {
                time = 0;
                setMoving(false);
                DidTp = false;
                Destroy(Halo);
            }
            else if (DidTp)
            {
                float vFactor = (HaloMaxScale * (time - timeBeforeTp)) / HaloTime;
                Halo.transform.localScale = new Vector3(vFactor, vFactor, 1);
                Halo.transform.localEulerAngles += new Vector3(0, rotationSpeed, 0);
            }
            else if (time > timeBeforeTp)
            {
                Tp();
            }
            time += Time.deltaTime;
        }
    }
}
