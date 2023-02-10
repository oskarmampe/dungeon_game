using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Damage struct
    public int[] damagePoint = { 1, 2, 3, 4, 5, 6, 75 };
    public float[] pushForce = { 2.1f, 2.2f, 2.5f, 3.0f, 4.5f, 5.0f, 6.5f };

    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    private Animator anim;
    private float swingTimer = 0.5f;
    private float lastSwing;
   
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }


    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSwing > swingTimer)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter" && coll.name != "Player")
        {
            Damage dmg = new Damage() 
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel],
            };

            coll.SendMessage("ReceiveDamage", dmg);
        }
    }

    private void Swing()
    {
        anim.SetTrigger("Swing");
    }

    public void UpgradeWeapon() 
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];    
    }

    public void SetWeaponLevel(int lvl)
    {
        weaponLevel = lvl;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
        
    }
}
