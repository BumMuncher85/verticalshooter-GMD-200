using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] GameObject player;

    [Header("Prefabs")]
    [SerializeField] GameObject[] enemyPrefabs;

    [Header("UI")]
    [SerializeField] MenuHandler menuHandler;


    private int wave = 1;
    private int enemiesKilled = 0;
    private float enemyAmountForWave= 10;
    List<GameObject> enemies = new List<GameObject>();
    List<string> enemyType = new List<string>();
    public float spawnDistance = 5;
    System.Random RAND = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        enemiesKilled = 0;
    }

    // Update is called once per frame
    void Update()
    { 
        if(menuHandler.menuOpen == false && menuHandler.levelUpOpen == false)
        {
            if (enemies.Count > 0)
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    string enemyName = enemyType[i];
                    switch (enemyName)
                    {
                        case "Slime":
                            SlimeController script = enemies[i].GetComponent<SlimeController>();
                            if (script.despawned == true)
                            {
                                // destroy object
                                Destroy(enemies[i]);
                                // Remove from enemies array
                                enemies.RemoveAt(i);
                            }
                            else if (script.dead == true)
                            {
                                // Remove from enemies array
                                enemies.RemoveAt(i);
                                enemiesKilled += 1;
                            }
                            break;
                    }
                }
            }
            if (enemiesKilled >= enemyAmountForWave)
            {
                wave += 1;
                enemiesKilled = 0;
                Debug.Log("NEXT WAVE: " + wave);
                enemyAmountForWave = Mathf.RoundToInt(enemyAmountForWave * 1.1f);
            }
            if (enemies.Count < enemyAmountForWave * 3)
            {
                int spawnChance = RAND.Next(1, 1000);
                if (spawnChance > 1000 - Mathf.Round(wave / 3) - 2)
                {
                    Vector3 spawnPos = player.transform.position;
                    int spawnX = RAND.Next(-3, 4);
                    int spawnY = RAND.Next(0, 2);
                    spawnPos.x = spawnX;
                    if (spawnY == 0)
                    {
                        spawnPos.y += spawnDistance;
                    }
                    else
                    {
                        spawnPos.y -= spawnDistance;
                    }
                    spawnPos.z = -1.5f;
                    if (wave < 5)
                    {
                        enemies.Add(Instantiate(enemyPrefabs[0], spawnPos, Quaternion.identity));
                        enemyType.Add("Slime");
                    }
                    else if (wave < 15)
                    {
                        int enemychance = RAND.Next(1, 20);
                        if (enemychance > 19 - MathF.Round((wave - 5) / 2))
                        {
                            enemies.Add(Instantiate(enemyPrefabs[1], spawnPos, Quaternion.identity));
                            enemyType.Add("Slime");
                        }
                        else
                        {
                            enemies.Add(Instantiate(enemyPrefabs[0], spawnPos, Quaternion.identity));
                            enemyType.Add("Slime");
                        }
                    } else
                    {
                        int enemychance = RAND.Next(1, 50);
                        if (enemychance > 50 - MathF.Round((wave - 15) / 2))
                        {
                            enemies.Add(Instantiate(enemyPrefabs[2], spawnPos, Quaternion.identity));
                            enemyType.Add("Slime");
                        }
                        else
                        {
                            enemies.Add(Instantiate(enemyPrefabs[1], spawnPos, Quaternion.identity));
                            enemyType.Add("Slime");
                        }
                    }
                }
            }
        }
    }
}
