using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        GameManager.instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    // Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;

    // Logic
    public int coins;
    public int experience;

    private void Update()
    {
        Debug.Log(GetCurrentLevel());
    }


    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    public int GetCurrentLevel()
    {
        int result = 0;
        int add = 0;

        while (experience >= add)
        {
            add += xpTable[result];
            result++;

            if (result == xpTable.Count)
            {
                return result;
            }
        }

        return result;
    }

    public int GetXpToLevel(int level)
    {
        int result = 0;
        int xp = 0;
        while (result < level)
        {
            xp += xpTable[result];
            result++;
            if (result >= xpTable.Count)
            {
                return xp;
            }
        }

        return xp;
    }

    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if (currLevel < GetCurrentLevel())
        {
            OnLevelUp();
        }
    }

    private void OnLevelUp()
    {
        player.OnLevelUp();
    }

    public bool TryUpgradeWeapon()
    {
        if(weaponPrices.Count <= weapon.weaponLevel)
        {
            return false;
        }

        if(coins >= weaponPrices[weapon.weaponLevel])
        {
            coins -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        return false; 
    }


    public void SaveState() 
    {
        string s = "";

        s += "0" + "|";
        s += coins.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();


        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
        {
            return;
        }
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        // change player skin

        coins = int.Parse(data[1]);
        experience = int.Parse(data[2]);

        int level = GetCurrentLevel();
        if (level != 1)
        {
            player.SetLevel(level);
        }
        weapon.SetWeaponLevel(int.Parse(data[3]));


    }
        
}
