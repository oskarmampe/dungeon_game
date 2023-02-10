using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{

    public Sprite emptyChest;
    public int coins = 5;
    private void Start()
    {
        base.Start();
        coins = Random.Range(1, 10)*15;
    }
    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.coins += coins;
            GameManager.instance.ShowText("+" + coins + " coins!", 25, Color.yellow, transform.position, Vector3.up * 50, 3.0f);
        }
    }
}
