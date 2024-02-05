using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrefrenceHandler : MonoBehaviour
{

    public int MAX_POWER;
    public int MAX_KNOCKBACK;
    public int MAX_FIRE_RATE;
    public int MAX_BULLETS;
    public int MAX_SPREAD;
    public int MAX_PENETRATION;

    // Start is called before the first frame update
    void Start()
    {
        //if (PlayerPrefs.GetInt("INIT_SETUP") == 0)
       // {
            InitializePrefrences();
        //}


        MAX_POWER = 7;
        MAX_KNOCKBACK = 10;
        MAX_FIRE_RATE = 10;
        MAX_BULLETS = 5;
        MAX_SPREAD = 10;
        MAX_PENETRATION = 5;
    }
    
    public void InitializePrefrences()
    {
        //
        PlayerPrefs.SetInt("INIT_SETUP", 1);
        PlayerPrefs.SetInt("PLAYER_POINTS", 0);
        PlayerPrefs.SetInt("PLAYER_COINS", 0);

        //Initalize Gun Unlocks
        PlayerPrefs.SetInt("pistolUnlocked", 1);
        PlayerPrefs.SetInt("shotgunUnlocked", 0);
        PlayerPrefs.SetInt("smgUnlocked", 0);
        PlayerPrefs.SetInt("akUnlocked", 0);

        //Pistol Stats
        PlayerPrefs.SetInt("pistolPower", 1);
        PlayerPrefs.SetInt("pistolKnockback", 1);

        //Shotgun Stats
        PlayerPrefs.SetInt("shotgunPower", 1);
        PlayerPrefs.SetInt("shotgunBullets", 1);
        PlayerPrefs.SetInt("shotgunSpread", 1);

        //SMG Stats
        PlayerPrefs.SetInt("smgPower", 1);
        PlayerPrefs.SetInt("smgFireRate", 1);
        PlayerPrefs.SetInt("smgSpread", 1);

        //AK Stats
        PlayerPrefs.SetInt("akPower", 1);
        PlayerPrefs.SetInt("akFireRate", 1);
        PlayerPrefs.SetInt("akKnockback", 1);  
        PlayerPrefs.SetInt("akPenetration", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void unlockGun(int id)
    {   
        switch (id)
        {
            case 1:
                PlayerPrefs.SetInt("shotgunUnlocked", 1);
                break;
            case 2:
                PlayerPrefs.SetInt("smgUnlocked", 1);
                break;
            case 3:
                PlayerPrefs.SetInt("akUnlocked", 1);
                break;
        }
        
    }

    public int[] getGunStats(int id)
    {
        switch (id)
        {
            case 0:
                int[] stats0 = new int[2];
                stats0[0] = PlayerPrefs.GetInt("pistolPower");
                stats0[1] = PlayerPrefs.GetInt("pistolKnockback");
                return stats0;
            case 1:
                int[] stats1 = new int[3];
                stats1[0] = PlayerPrefs.GetInt("shotgunPower");
                stats1[1] = PlayerPrefs.GetInt("shotgunBullets");
                stats1[2] = PlayerPrefs.GetInt("shotgunSpread");
                return stats1;
            case 2:
                int[] stats2 = new int[3];
                stats2[0] = PlayerPrefs.GetInt("smgPower");
                stats2[1] = PlayerPrefs.GetInt("smgFireRate");
                stats2[2] = PlayerPrefs.GetInt("smgSpread");
                return stats2;
            case 3:
                int[] stats3 = new int[4];
                stats3[0] = PlayerPrefs.GetInt("akPower");
                stats3[1] = PlayerPrefs.GetInt("akFireRate");
                stats3[2] = PlayerPrefs.GetInt("akKnockback");
                stats3[3] = PlayerPrefs.GetInt("akPenetration");
                return stats3;
        }
        return new int[0];
    }


}

