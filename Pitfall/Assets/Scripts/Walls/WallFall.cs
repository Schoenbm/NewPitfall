
using UnityEngine;
using System.Collections;

public class WallFall : MonoBehaviour
{
    public float health = 100f; 

    void OnCollisionEnter(Collision CollisionInfo)
    {
        // 10 is the layer of PlayerHitBox
        if (CollisionInfo.gameObject.layer == 10)
        {
            health = health - 35;
          
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
