using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuHandler : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Canvas gameCanvas;
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject LevelUpUI;
    [SerializeField] Sprite[] statIcons;
    [SerializeField] Sprite statIncreaseIcon;
    [SerializeField] Sprite statBackground;
    [SerializeField] GameObject[] weaponButtons;
    [SerializeField] GameObject[] currentWeapons;
    [SerializeField] Text currentWeaponText;
    [SerializeField] GameObject barBreak;
    [SerializeField] GameObject[] statBars;
    [SerializeField] GameObject[] powerBars;
    [SerializeField] GameObject[] penetrationBars;
    [SerializeField] GameObject[] fireRateBars;
    [SerializeField] GameObject[] knockbackBars;
    [SerializeField] GameObject[] spreadBars;
    [SerializeField] GameObject[] bulletsBars;
    [SerializeField] Text pointsText;
    [SerializeField] GameObject StartUI;
    [SerializeField] GameObject[] weaponBuyButtons;
    [SerializeField] GameObject[] weaponBuyUIs;
    [SerializeField] GameObject[] weaponBuyGoldDisplays;

    [Header("Other")]
    [SerializeField] GameObject player;
    [SerializeField] WeaponScript weaponScript;
    [SerializeField] PrefrenceHandler prefrenceHandler;

    [Header("AUDIO")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip musicAudio;

    public bool menuOpen = true;
    public bool levelUpOpen = false;

    public int currentWeapon = 0;

    private int TOP_BOUND = 45;
    private int HEIGHT = 90;
    private double STAT_RIGHT = 63.0;

    private int STAT_VALUE_WIDTH = 227;

    List<GameObject> limits = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        menuUI.SetActive(false);
        gameUI.SetActive(true);
        menuOpen = true;
    }

    public void StartGame()
    {
        StartUI.SetActive(false);
        menuOpen = false;
    }

    public void EndGame()
    {
        SceneManager.LoadScene("Battle");
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuOpen)
            {
                menuUI.SetActive(false);
                gameUI.SetActive(true);
                menuOpen = false;
                Time.timeScale = 1f;
                
            }
            else
            {
                menuUI.SetActive(true);
                gameUI.SetActive(false);
                menuOpen = true;
                Time.timeScale = 0f;
                Debug.Log("WHAT I SHOULD BE PAUSED");
            }
        } 
    }

    public void UpdateStatBars()
    {
        var weaponData = prefrenceHandler.getGunStats(currentWeapon);
        switch (currentWeapon)
        {
            case 0: // power, knockback 
                double powerLevelPercent0 = (double)weaponData[0] / (double)prefrenceHandler.MAX_POWER;
                var powerScale0 = powerBars[0].GetComponent<RectTransform>().localScale;
                powerScale0.x = STAT_VALUE_WIDTH * (float)powerLevelPercent0;
                powerBars[0].GetComponent<RectTransform>().localScale = powerScale0;

                var knockbackLevelPercent0 = (double)weaponData[1] / (double)prefrenceHandler.MAX_KNOCKBACK;
                var knockbackScale0 = knockbackBars[0].GetComponent<RectTransform>().localScale;
                knockbackScale0.x = STAT_VALUE_WIDTH * (float)knockbackLevelPercent0;
                knockbackBars[0].GetComponent<RectTransform>().localScale = knockbackScale0;
                break;

            case 1: // power, bullets, spread
                double powerLevelPercent1 = (double)weaponData[0] / (double)prefrenceHandler.MAX_POWER;
                var powerScale1 = powerBars[0].GetComponent<RectTransform>().localScale;
                powerScale1.x = STAT_VALUE_WIDTH * (float)powerLevelPercent1;
                powerBars[0].GetComponent<RectTransform>().localScale = powerScale1;

                double bulletsLevelPercent1 = (double)weaponData[1] / (double)prefrenceHandler.MAX_BULLETS;
                var bulletsScale1 = bulletsBars[0].GetComponent<RectTransform>().localScale;
                bulletsScale1.x = STAT_VALUE_WIDTH * (float)bulletsLevelPercent1;
                bulletsBars[0].GetComponent<RectTransform>().localScale = bulletsScale1;

                double spreadLevelPercent1 = (double)weaponData[2] / (double)prefrenceHandler.MAX_SPREAD;
                var spreadScale1 = spreadBars[0].GetComponent<RectTransform>().localScale;
                spreadScale1.x = STAT_VALUE_WIDTH * (float)spreadLevelPercent1;
                spreadBars[0].GetComponent<RectTransform>().localScale = spreadScale1;
                break;

            case 2: //power, firerate, spread
                double powerLevelPercent2 = (double)weaponData[0] / (double)prefrenceHandler.MAX_POWER;
                var powerScale2 = powerBars[0].GetComponent<RectTransform>().localScale;
                powerScale1.x = STAT_VALUE_WIDTH * (float)powerLevelPercent2;
                powerBars[0].GetComponent<RectTransform>().localScale = powerScale2;

                double fireRateLevelPercent2 = (double)weaponData[1] / (double)prefrenceHandler.MAX_FIRE_RATE;
                var fireRateScale2 = fireRateBars[0].GetComponent<RectTransform>().localScale;
                fireRateScale2.x = STAT_VALUE_WIDTH * (float)fireRateLevelPercent2;
                fireRateBars[0].GetComponent<RectTransform>().localScale = fireRateScale2;

                double spreadLevelPercent2 = (double)weaponData[2] / (double)prefrenceHandler.MAX_SPREAD;
                var spreadScale2 = spreadBars[0].GetComponent<RectTransform>().localScale;
                spreadScale2.x = STAT_VALUE_WIDTH * (float)spreadLevelPercent2;
                spreadBars[0].GetComponent<RectTransform>().localScale = spreadScale2;
                break;

            case 3: //power, firerate, knockback, penetration
                double powerLevelPercent3 = (double)weaponData[0] / (double)prefrenceHandler.MAX_POWER;
                var powerScale3 = powerBars[0].GetComponent<RectTransform>().localScale;
                powerScale3.x = STAT_VALUE_WIDTH * (float)powerLevelPercent3;
                powerBars[0].GetComponent<RectTransform>().localScale = powerScale3;

                double fireRateLevelPercent3 = (double)weaponData[1] / (double)prefrenceHandler.MAX_FIRE_RATE;
                var fireRateScale3 = fireRateBars[0].GetComponent<RectTransform>().localScale;
                fireRateScale3.x = STAT_VALUE_WIDTH * (float)fireRateLevelPercent3;
                fireRateBars[0].GetComponent<RectTransform>().localScale = fireRateScale3;

                double knockbackPercent3 = (double)weaponData[2] / (double)prefrenceHandler.MAX_KNOCKBACK;
                var knockbackScale3 = knockbackBars[0].GetComponent<RectTransform>().localScale;
                knockbackScale3.x = STAT_VALUE_WIDTH * (float)knockbackPercent3;
                knockbackBars[0].GetComponent<RectTransform>().localScale = knockbackScale3;

                double penetrationLevelPercent3 = (double)weaponData[3] / (double)prefrenceHandler.MAX_PENETRATION;
                var penetraitionScale3 = penetrationBars[0].GetComponent<RectTransform>().localScale;
                penetraitionScale3.x = STAT_VALUE_WIDTH * (float)penetrationLevelPercent3;
                penetrationBars[0].GetComponent<RectTransform>().localScale = penetraitionScale3;
                break;
        }
    }

    public void openBuy(int slot)
    {
        switch (slot)
        {
            case 0:
                if(currentWeapon == 0)
                {
                    attemptBuyWeapon(1);
                }
                break;
            case 1:
                switch (currentWeapon)
                {
                    case 0:
                        attemptBuyWeapon(2);
                        break;
                    case 1:
                        attemptBuyWeapon(2);
                        break;
                    case 2:
                        attemptBuyWeapon(1);
                        break;
                    case 3:
                        attemptBuyWeapon(1);
                        break;
                }
                break;
            case 2:
                switch (currentWeapon)
                {
                    case 0:
                        attemptBuyWeapon(3);
                        break;
                    case 1:
                        attemptBuyWeapon(3);
                        break;
                    case 2:
                        attemptBuyWeapon(3);
                        break;
                    case 3:
                        attemptBuyWeapon(2);
                        break;
                }
                break;  
        }
    }

    public void attemptBuyWeapon(int gunID)
    {
        var coins = PlayerPrefs.GetInt("PLAYER_COINS");
        switch (gunID)
        {
            case 1:
                weaponBuyUIs[0].SetActive(true);
                weaponBuyGoldDisplays[0].GetComponent<Text>().text = "(YOU HAVE " + coins + " COINS)";
                break;
            case 2:
                weaponBuyUIs[1].SetActive(true);
                weaponBuyGoldDisplays[1].GetComponent<Text>().text = "(YOU HAVE " + coins + " COINS)";
                break;
            case 3:
                weaponBuyUIs[2].SetActive(true);
                weaponBuyGoldDisplays[2].GetComponent<Text>().text = "(YOU HAVE " + coins + " COINS)";
                break;
        }
    }
    public void nahIDontFeelLikeIt()
    {
        weaponBuyUIs[0].SetActive(false);
        weaponBuyUIs[1].SetActive(false);
        weaponBuyUIs[2].SetActive(false);
    }
    public void BuyWeapon(int gunID)
    {
        var coins = PlayerPrefs.GetInt("PLAYER_COINS");
            Debug.Log("TRYING BUY");
            
            switch (gunID)
            {
                case 1:
                    if (coins >= 5)
                    {
                        PlayerPrefs.SetInt("shotgunUnlocked", 1);
                        coins -= 5;
                        changeWeapon(1);
                    }
                    weaponBuyUIs[0].SetActive(false);
                    break;
                case 2:
                    if (coins >= 10)
                    {
                        PlayerPrefs.SetInt("smgUnlocked", 1);
                        coins -= 10;
                        changeWeapon(2);
                    }
                    weaponBuyUIs[1].SetActive(false);
                    break;
                case 3:
                    if (coins >= 20)
                    {
                        PlayerPrefs.SetInt("akUnlocked", 1);
                        coins -= 20;
                        changeWeapon(3);
                    }
                    weaponBuyUIs[2].SetActive(false);
                    break;
        }
        PlayerPrefs.SetInt("PLAYER_COINS", coins);
    }
    public void changeWeapon(int id)
    {
        currentWeapons[0].SetActive(false);
        currentWeapons[1].SetActive(false);
        currentWeapons[2].SetActive(false);
        currentWeapons[3].SetActive(false);
        weaponButtons[0].SetActive(false);
        weaponButtons[1].SetActive(false);
        weaponButtons[2].SetActive(false);
        weaponButtons[3].SetActive(false);
        weaponButtons[4].SetActive(false);
        weaponButtons[5].SetActive(false);
        statBars[0].SetActive(false);
        statBars[1].SetActive(false);
        statBars[2].SetActive(false);
        statBars[3].SetActive(false);
        statBars[4].SetActive(false);
        statBars[5].SetActive(false);
        weaponBuyButtons[0].SetActive(false);
        weaponBuyButtons[1].SetActive(false);
        weaponBuyButtons[2].SetActive(false);


        for (int i = limits.Count-1; i >= 0; i--)
        {
            Destroy(limits[i]);
            limits.RemoveAt(i);
        }

        switch (id)
        {
        case 0:
            currentWeapon = 0;
            currentWeapons[0].SetActive(true);
            weaponButtons[1].SetActive(true);
            weaponButtons[3].SetActive(true);
            weaponButtons[5].SetActive(true);
            if(PlayerPrefs.GetInt("shotgunUnlocked") == 0)
                {
                    weaponBuyButtons[0].SetActive(true);
                }
            if (PlayerPrefs.GetInt("smgUnlocked") == 0)
                {
                    weaponBuyButtons[1].SetActive(true);
                }
            if (PlayerPrefs.GetInt("akUnlocked") == 0)
                {
                    weaponBuyButtons[2].SetActive(true);
                }

                GameObject powerBar0 = statBars[0];
            powerBar0.SetActive(true);
            powerBar0.GetComponent<RectTransform>().localPosition = new Vector3(32f, TOP_BOUND - (HEIGHT / 3), 0f);
            Vector3 powerBarPos0 = powerBar0.GetComponent<RectTransform>().localPosition;
            for (int i = 1; i < prefrenceHandler.MAX_POWER; i++)
            {
                Vector3 limitPosPower0 = new Vector3(1f, powerBarPos0.y, powerBarPos0.z);
                limitPosPower0.x = (float)(1 + (STAT_RIGHT / (double)prefrenceHandler.MAX_POWER) * (double)i);
                GameObject limitPower0 = Instantiate(barBreak, limitPosPower0, Quaternion.identity);
                limitPower0.name = "LIMIT #" + i;
                limitPower0.transform.SetParent(LevelUpUI.transform, false);
                limitPower0.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 185);
                limits.Add(limitPower0);
            }


            GameObject knockbackBar0 = statBars[3];
            knockbackBar0.SetActive(true);
            knockbackBar0.GetComponent<RectTransform>().localPosition = new Vector3(32f, TOP_BOUND - (HEIGHT / 3)*2, 0f);
            Vector3 knockbackBarPos0 = knockbackBar0.GetComponent<RectTransform>().localPosition;
            for (int i = 1; i < prefrenceHandler.MAX_KNOCKBACK; i++)
            {
                Vector3 limitPosKnockback0 = new Vector3(1f, knockbackBarPos0.y, knockbackBarPos0.z);
                limitPosKnockback0.x = (float)(1 + (STAT_RIGHT / (double)prefrenceHandler.MAX_KNOCKBACK) * i);
                GameObject limitKnockback0 = Instantiate(barBreak, limitPosKnockback0, Quaternion.identity);
                limitKnockback0.name = "LIMIT #" + i;
                limitKnockback0.transform.SetParent(LevelUpUI.transform, false);
                limitKnockback0.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 185);
                limits.Add(limitKnockback0);
            }

            currentWeaponText.text = "Pistol";
            break;
        case 1:

            currentWeapon = 1;
            currentWeapons[1].SetActive(true);
            weaponButtons[0].SetActive(true);
            weaponButtons[3].SetActive(true);
            weaponButtons[5].SetActive(true);
                if (PlayerPrefs.GetInt("smgUnlocked") == 0)
                {
                    weaponBuyButtons[1].SetActive(true);
                }
                if (PlayerPrefs.GetInt("akUnlocked") == 0)
                {
                    weaponBuyButtons[2].SetActive(true);
                }

                GameObject powerBar1 = statBars[0];
            powerBar1.SetActive(true);
            powerBar1.GetComponent<RectTransform>().localPosition = new Vector3(32f, TOP_BOUND - (HEIGHT / 4), 0f);
            Vector3 powerBarPos1 = powerBar1.GetComponent<RectTransform>().localPosition;
            for (int i = 1; i < prefrenceHandler.MAX_POWER; i++)
            {
                Vector3 limitPosPower1 = new Vector3(1f, powerBarPos1.y, powerBarPos1.z);
                limitPosPower1.x = (float)(1 + (STAT_RIGHT / (double)prefrenceHandler.MAX_POWER) * i);
                GameObject limitPower1 = Instantiate(barBreak, limitPosPower1, Quaternion.identity);
                limitPower1.name = "LIMIT #" + i;
                limitPower1.transform.SetParent(LevelUpUI.transform, false);
                limitPower1.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 185);
                limits.Add(limitPower1);
            }


            GameObject bulletsBar1 = statBars[4];
            bulletsBar1.SetActive(true);
            bulletsBar1.GetComponent<RectTransform>().localPosition = new Vector3(32f, TOP_BOUND - (HEIGHT / 4) * 2, 0f);
            Vector3 bulletsBarPos1 = bulletsBar1.GetComponent<RectTransform>().localPosition;
            for (int i = 1; i < prefrenceHandler.MAX_BULLETS; i++)
            {
                Vector3 limitPosBullets1 = new Vector3(1f, bulletsBarPos1.y, bulletsBarPos1.z);
                limitPosBullets1.x = (float)(1 + (STAT_RIGHT / (double)prefrenceHandler.MAX_BULLETS) * i);
                GameObject limitBullets1 = Instantiate(barBreak, limitPosBullets1, Quaternion.identity);
                limitBullets1.name = "LIMIT #" + i;
                limitBullets1.transform.SetParent(LevelUpUI.transform, false);
                limitBullets1.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 185);
                limits.Add(limitBullets1);
            }

            GameObject spreadBar1 = statBars[2];
            spreadBar1.SetActive(true);
            spreadBar1.GetComponent<RectTransform>().localPosition = new Vector3(32f, TOP_BOUND - (HEIGHT / 4) * 3, 0f);
            Vector3 spreadBarPos1 = spreadBar1.GetComponent<RectTransform>().localPosition;
            for (int i = 1; i < prefrenceHandler.MAX_SPREAD; i++)
            {
                Vector3 limitPosSpread1 = new Vector3(1f, spreadBarPos1.y, spreadBarPos1.z);
                limitPosSpread1.x = (float)(1 + (STAT_RIGHT / (double)prefrenceHandler.MAX_SPREAD) * i);
                GameObject limitSpread1 = Instantiate(barBreak, limitPosSpread1, Quaternion.identity);
                limitSpread1.name = "LIMIT #" + i;
                limitSpread1.transform.SetParent(LevelUpUI.transform, false);
                limitSpread1.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 185);
                limits.Add(limitSpread1);
            }


            currentWeaponText.text = "Shotgun";
            break;

        case 2:
            currentWeapon = 2;
            currentWeapons[2].SetActive(true);
            weaponButtons[0].SetActive(true);
            weaponButtons[2].SetActive(true);
            weaponButtons[5].SetActive(true);
                if (PlayerPrefs.GetInt("shotgunUnlocked") == 0)
                {
                    weaponBuyButtons[0].SetActive(true);
                }
                if (PlayerPrefs.GetInt("akUnlocked") == 0)
                {
                    weaponBuyButtons[2].SetActive(true);
                }

                GameObject bar21 = statBars[0];
            bar21.SetActive(true);
            bar21.GetComponent<RectTransform>().localPosition = new Vector3(32f, TOP_BOUND - (HEIGHT / 4), 0f);
            Vector3 BarPos2 = bar21.GetComponent<RectTransform>().localPosition;
            for (int i = 1; i < prefrenceHandler.MAX_POWER; i++)
            {
                Vector3 limitPos2 = new Vector3(1f, BarPos2.y, BarPos2.z);
                limitPos2.x = (float)(1 + (STAT_RIGHT / (double)prefrenceHandler.MAX_POWER) * i);
                GameObject limit2 = Instantiate(barBreak, limitPos2, Quaternion.identity);
                limit2.name = "LIMIT #" + i;
                limit2.transform.SetParent(LevelUpUI.transform, false);
                limit2.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 185);
                limits.Add(limit2);
            }

            GameObject bar22 = statBars[1];
            bar22.SetActive(true);
            bar22.GetComponent<RectTransform>().localPosition = new Vector3(32f, TOP_BOUND - (HEIGHT / 4) * 2, 0f);
            Vector3 BarPos22 = bar22.GetComponent<RectTransform>().localPosition;
            for (int i = 1; i < prefrenceHandler.MAX_FIRE_RATE; i++)
            {
                Vector3 limitPos22 = new Vector3(1f, BarPos22.y, BarPos22.z);
                limitPos22.x = (float)(1 + (STAT_RIGHT / (double)prefrenceHandler.MAX_FIRE_RATE) * i);
                GameObject limit22 = Instantiate(barBreak, limitPos22, Quaternion.identity);
                limit22.name = "LIMIT #" + i;
                limit22.transform.SetParent(LevelUpUI.transform, false);
                limit22.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 185);
                limits.Add(limit22);
            }

            GameObject bar23 = statBars[2];
            bar23.SetActive(true);
            bar23.GetComponent<RectTransform>().localPosition = new Vector3(32f, TOP_BOUND - (HEIGHT / 4) * 3, 0f);
            Vector3 BarPos23 = bar23.GetComponent<RectTransform>().localPosition;
            for (int i = 1; i < prefrenceHandler.MAX_SPREAD; i++)
            {
                Vector3 limitPos23 = new Vector3(1f, BarPos23.y, BarPos23.z);
                limitPos23.x = (float)(1 + (STAT_RIGHT / (double)prefrenceHandler.MAX_SPREAD) * i);
                GameObject limit23 = Instantiate(barBreak, limitPos23, Quaternion.identity);
                limit23.name = "LIMIT #" + i;
                limit23.transform.SetParent(LevelUpUI.transform, false);
                limit23.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 185);
                limits.Add(limit23);
            }

            currentWeaponText.text = "SMG";
            break;
        case 3:
            currentWeapon = 3;
            currentWeapons[3].SetActive(true);
            weaponButtons[0].SetActive(true);
            weaponButtons[2].SetActive(true);
            weaponButtons[4].SetActive(true);
                if (PlayerPrefs.GetInt("shotgunUnlocked") == 0)
                {
                    weaponBuyButtons[0].SetActive(true);
                }
                if (PlayerPrefs.GetInt("smgUnlocked") == 0)
                {
                    weaponBuyButtons[1].SetActive(true);
                }

                GameObject bar31 = statBars[0];
            bar31.SetActive(true);
            bar31.GetComponent<RectTransform>().localPosition = new Vector3(32f, TOP_BOUND - (HEIGHT / 5), 0f);
            Vector3 BarPos3 = bar31.GetComponent<RectTransform>().localPosition;
            for (int i = 1; i < prefrenceHandler.MAX_POWER; i++)
            {
                Vector3 limitPos3 = new Vector3(1f, BarPos3.y, BarPos3.z);
                limitPos3.x = (float)(1 + (STAT_RIGHT / (double)prefrenceHandler.MAX_POWER) * i);
                GameObject limit3 = Instantiate(barBreak, limitPos3, Quaternion.identity);
                limit3.name = "LIMIT #" + i;
                limit3.transform.SetParent(LevelUpUI.transform, false);
                limit3.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 185);
                limits.Add(limit3);
            }

            GameObject bar32 = statBars[1];
            bar32.SetActive(true);
            bar32.GetComponent<RectTransform>().localPosition = new Vector3(32f, TOP_BOUND - (HEIGHT / 5)*2, 0f);
            BarPos3 = bar32.GetComponent<RectTransform>().localPosition;
            for (int i = 1; i < prefrenceHandler.MAX_FIRE_RATE; i++)
            {
                Vector3 limitPos3 = new Vector3(1f, BarPos3.y, BarPos3.z);
                limitPos3.x = (float)(1 + (STAT_RIGHT / (double)prefrenceHandler.MAX_FIRE_RATE) * i);
                GameObject limit3 = Instantiate(barBreak, limitPos3, Quaternion.identity);
                limit3.name = "LIMIT #" + i;
                limit3.transform.SetParent(LevelUpUI.transform, false);
                limit3.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 185);
                limits.Add(limit3);
            }

            GameObject bar33 = statBars[3];
            bar33.SetActive(true);
            bar33.GetComponent<RectTransform>().localPosition = new Vector3(32f, TOP_BOUND - (HEIGHT / 5) * 3, 0f);
            BarPos3 = bar33.GetComponent<RectTransform>().localPosition;
            for (int i = 1; i < prefrenceHandler.MAX_KNOCKBACK; i++)
            {
                Vector3 limitPos3 = new Vector3(1f, BarPos3.y, BarPos3.z);
                limitPos3.x = (float)(1 + (STAT_RIGHT / (double)prefrenceHandler.MAX_KNOCKBACK) * i);
                GameObject limit3 = Instantiate(barBreak, limitPos3, Quaternion.identity);
                limit3.name = "LIMIT #" + i;
                limit3.transform.SetParent(LevelUpUI.transform, false);
                limit3.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 185);
                limits.Add(limit3);
            }

            GameObject bar34 = statBars[5];
            bar34.SetActive(true);
            bar34.GetComponent<RectTransform>().localPosition = new Vector3(32f, TOP_BOUND - (HEIGHT / 5) * 4, 0f);
            BarPos3 = bar34.GetComponent<RectTransform>().localPosition;
            for (int i = 1; i < prefrenceHandler.MAX_PENETRATION; i++)
            {
                Vector3 limitPos3 = new Vector3(1f, BarPos3.y, BarPos3.z);
                limitPos3.x = (float)(1 + (STAT_RIGHT / (double)prefrenceHandler.MAX_PENETRATION) * i);
                GameObject limit3 = Instantiate(barBreak, limitPos3, Quaternion.identity);
                limit3.name = "LIMIT #" + i;
                limit3.transform.SetParent(LevelUpUI.transform, false);
                limit3.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 185);
                limits.Add(limit3);
            }

            currentWeaponText.text = "AK-47";
            break;

        }

        UpdateStatBars();
    }

    public void LevelUp()
    {
        levelUpOpen = true;
        Debug.Log("Window Opened");
        changeWeapon(weaponScript.weaponID);
        UpdateStatBars();
        pointsText.text = "POINTS: " + PlayerPrefs.GetInt("PLAYER_POINTS");
    }

    public void Continue()
    {
        levelUpOpen = false;
        Debug.Log("Window Closed");
        LevelUpUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void IncreaseStat(string stat)
    {
        int points = PlayerPrefs.GetInt("PLAYER_POINTS");
        if (points <= 0) { return; }
        
        switch (stat)
        {
            case "Power":
                switch (currentWeapon)
                {
                    case 0:
                        if(PlayerPrefs.GetInt("pistolPower") >= prefrenceHandler.MAX_POWER)
                        {
                            Debug.Log("MAX REACHED");
                            return;
                        }
                        break;
                    case 1:
                        if (PlayerPrefs.GetInt("shotgunPower") >= prefrenceHandler.MAX_POWER)
                        {
                            Debug.Log("MAX REACHED");
                            return;

                        }
                        break;
                    case 2:
                        if (PlayerPrefs.GetInt("smgPower") >= prefrenceHandler.MAX_POWER)
                        {
                            Debug.Log("MAX REACHED");
                            return;
                        }
                        break;
                    case 3:
                        if (PlayerPrefs.GetInt("akPower") >= prefrenceHandler.MAX_POWER)
                        {
                            Debug.Log("MAX REACHED");
                            return;
                        }
                        break;
                }
                break;
            case "Knockback":
                switch (currentWeapon)
                {
                    case 0:
                        if (PlayerPrefs.GetInt("pistolKnockback") >= prefrenceHandler.MAX_KNOCKBACK)
                        {
                            Debug.Log("MAX REACHED");
                            return;
                        }
                        break;
                    case 3:
                        if (PlayerPrefs.GetInt("akKnockback") >= prefrenceHandler.MAX_KNOCKBACK)
                        {
                            Debug.Log("MAX REACHED");
                            return;
                        }
                        break;
                }
                break;
            case "Fire Rate":
                switch (currentWeapon)
                {
                    case 2:
                        if (PlayerPrefs.GetInt("smgFireRate") >= prefrenceHandler.MAX_FIRE_RATE)
                        {
                            Debug.Log("MAX REACHED");
                            return;
                        }
                        break;
                    case 3:
                        if (PlayerPrefs.GetInt("akFireRate") >= prefrenceHandler.MAX_FIRE_RATE)
                        {
                            Debug.Log("MAX REACHED");
                            return;
                        }
                        break;
                }
                break;
            case "Spread":
                switch (currentWeapon)
                {
                    case 1:
                        if (PlayerPrefs.GetInt("shotgunSpread") >= prefrenceHandler.MAX_SPREAD)
                        {
                            Debug.Log("MAX REACHED");
                            return;
                        }
                        break;
                    case 2:
                        if (PlayerPrefs.GetInt("smgSpread") >= prefrenceHandler.MAX_SPREAD)
                        {
                            Debug.Log("MAX REACHED");
                            return;
                        }
                        break;
                }
                break;
            case "Bullets":
                if (PlayerPrefs.GetInt("shotgunBullets") >= prefrenceHandler.MAX_BULLETS)
                {
                    Debug.Log("MAX REACHED");
                    return;
                }
                break;
            case "Penetration":
                if (PlayerPrefs.GetInt("akPenetration") >= prefrenceHandler.MAX_PENETRATION)
                {
                    Debug.Log("MAX REACHED");
                    return;
                }
                break;
        }
        var oldStat = 0;
        switch (stat)
        {
            case "Power":
                switch (currentWeapon)
                {
                    case 0:
                        oldStat = PlayerPrefs.GetInt("pistolPower");
                        PlayerPrefs.SetInt("pistolPower", oldStat + 1);
                        break;
                    case 1:
                        oldStat = PlayerPrefs.GetInt("shotgunPower");
                        PlayerPrefs.SetInt("shotgunPower", oldStat + 1);
                        break;
                    case 2:
                        oldStat = PlayerPrefs.GetInt("smgPower");
                        PlayerPrefs.SetInt("smgPower", oldStat + 1);
                        break;
                    case 3:
                        oldStat = PlayerPrefs.GetInt("akPower");
                        PlayerPrefs.SetInt("akPower", oldStat + 1);
                        break;
                }
                break;
            case "Knockback":
                switch (currentWeapon)
                {
                    case 0:
                        oldStat = PlayerPrefs.GetInt("pistolKnockback");
                        PlayerPrefs.SetInt("pistolKnockback", oldStat + 1);
                        break;
                    case 3:
                        oldStat = PlayerPrefs.GetInt("akKnockback");
                        PlayerPrefs.SetInt("akKnockback", oldStat + 1);
                        break;
                }
                break;
            case "Fire Rate":
                switch (currentWeapon)
                {
                    case 2:
                        oldStat = PlayerPrefs.GetInt("smgFireRate");
                        PlayerPrefs.SetInt("smgFireRate", oldStat + 1);
                        break;
                    case 3:
                        oldStat = PlayerPrefs.GetInt("akFireRate");
                        PlayerPrefs.SetInt("akFireRate", oldStat + 1);
                        break;
                }
                break;
            case "Spread":
                switch (currentWeapon)
                {
                    case 1:
                        oldStat = PlayerPrefs.GetInt("shotgunSpread");
                        PlayerPrefs.SetInt("shotgunSpread", oldStat + 1);
                        break;
                    case 2:
                        oldStat = PlayerPrefs.GetInt("smgSpread");
                        PlayerPrefs.SetInt("smgSpread", oldStat + 1);
                        break;
                }
                break;
            case "Bullets":
                oldStat = PlayerPrefs.GetInt("shotgunBullets");
                PlayerPrefs.SetInt("shotgunBullets", oldStat + 1);
                break;
            case "Penetration":
                oldStat = PlayerPrefs.GetInt("akPenetration");
                PlayerPrefs.SetInt("akPenetration", oldStat + 1);
                break;
            default:
                Debug.Log("OH NO THATS NOT A STAT NOAH");
                break;
        }
        PlayerPrefs.SetInt("PLAYER_POINTS", points - 1);
        pointsText.text = "POINTS: " + PlayerPrefs.GetInt("PLAYER_POINTS");
        UpdateStatBars();
    }
}