
using UnityEngine;
using System.Collections;

public class WallFall : MonoBehaviour
{
    public float health = 100f;
    public float speedDamage = 1.2f;

    void OnCollisionEnter(Collision CollisionInfo)
    {
        // 9 is the layer of Player
        // 10 is the layer of PlayerHitBox
        if (CollisionInfo.gameObject.tag == "Player")
        {
            if (CollisionInfo.gameObject.GetComponent<Rigidbody>().velocity.magnitude > speedDamage)
            {
                Debug.Log("DAMAAGE BY PLAYERSZ :" + CollisionInfo.gameObject.GetComponent<Rigidbody>().velocity.magnitude);
                takeDamage(50);
            }

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void takeDamage(int pDamage)
    {
        health -= pDamage;
    }
}
