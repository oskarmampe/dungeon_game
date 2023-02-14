using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover { 
    private SpriteRenderer spriteRenderer;
    private bool isAlive = true;

    private void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    internal void SwapSprite(int currentCharacterSelection)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (isAlive)
        {
            UpdateMotor(new Vector3(x, y, 0));
        }
    }

    public void OnLevelUp()
    {
        maxHitpoint += 20;
        hitpoint = maxHitpoint;
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if (!isAlive)
        {
            return;
        }
        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitpointChange();
    }

    public void Heal(int healAmount)
    {
        if (hitpoint > maxHitpoint)
        {
            return;
        }
        hitpoint += healAmount;
        if (hitpoint > maxHitpoint)
        {
            hitpoint = maxHitpoint;
        }
        GameManager.instance.ShowText("+" + healAmount.ToString() + " hp", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
        GameManager.instance.OnHitpointChange();
    }

    protected override void Die()
    {
        isAlive = false;
        GameManager.instance.deathMenu.SetTrigger("Show");
    }

    public void Respawn()
    {
        Heal(maxHitpoint);
        isAlive = true;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }
}
