using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public float[] projectileSpeed = { 2.5f, -2.5f };
    public float distance = 0.5f;
    public Transform[] projectile;

    private void Update()
    {
        for (int i = 0; i < projectile.Length; i++)
        {
            projectile[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * projectileSpeed[i]) * distance, Mathf.Sin(Time.time * projectileSpeed[i]) * distance, 0);
        }
    }
}
