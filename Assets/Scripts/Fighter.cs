using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int hitpoint;
    public int maxHitpoint;
    public float pushRecoverySpeed = 0.2f;

    protected float immuneTime = 1.0f;
    protected float lastImmune = 0;

    protected Vector3 pushDirection;

    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;
            pushDirection.z = 0;

            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 15, Color.red, transform.position, Vector3.zero, 1.0f);

            if (hitpoint <= 0)
            {
                hitpoint = 0;
                Die();
            }
        }
    }

    protected virtual void Update()
    {
        if (hitpoint <= 0)
        {
            Die();
        }
    }


    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
