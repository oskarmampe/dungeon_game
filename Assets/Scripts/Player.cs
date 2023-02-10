using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover { 
    private SpriteRenderer spriteRenderer;

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

        UpdateMotor(new Vector3(x, y, 0));
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
}
