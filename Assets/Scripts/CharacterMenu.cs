using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    public Text levelText, hitpointText, coinText, upgradeCostText, xpText;
    private int currentCharacterSelection = 0;
    public Image characterImage;
    public Image weaponSprite;
    public RectTransform xpBar;

    public void OnArrowClick(bool right)
    {
       if(right)
        {
            currentCharacterSelection++;

            if (currentCharacterSelection >= GameManager.instance.playerSprites.Count)
            {
                currentCharacterSelection = 0;
            }

        } 
       else
        {
            currentCharacterSelection--;

            if (currentCharacterSelection < 0)
            {
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
            }

        } 
        OnSelectionChanged();
    }

    private void OnSelectionChanged()
    {
        characterImage.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }
    
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        } 
    }

    public void UpdateMenu()
    {
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
        {
            upgradeCostText.text = "MAX"; 
        } 
        else
        {
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        }

        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        coinText.text = GameManager.instance.coins.ToString();
        int currLevel = GameManager.instance.GetCurrentLevel();
        if (currLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = "MAX";
            xpBar.localScale = new Vector3(1, 1, 0);
        }
        else
        {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currLevel -1);
            int currLevelXp = GameManager.instance.GetXpToLevel(currLevel);

            int diff = currLevelXp - prevLevelXp;
            int currXpIntoLevel = GameManager.instance.experience - prevLevelXp;

            float completionRatio = (float)currXpIntoLevel / (float)diff;
            xpText.text = currXpIntoLevel.ToString() + " / " + diff.ToString();
            xpBar.localScale = new Vector3(completionRatio, 1, 0);
        }
    }
}
