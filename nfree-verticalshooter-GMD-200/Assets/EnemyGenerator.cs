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


    private int wave = 0;
    private int enemiesKilled;
    private float enemyAmountForWave;
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
                            Debug.Log("ENEMY #" + i + " DESPAWNED");
                        }
                        else if (script.dead == true)
                        {
                            // Remove from enemies array
                            enemies.RemoveAt(i);
                            enemiesKilled += 1;
                            Debug.Log("ENEMY #" + i + " WAS KILLED");
                        }
                        break;
                }
            }
        }
        if (enemiesKilled >= enemyAmountForWave)
        {
            wave += 1;
            enemiesKilled = 0;
            enemyAmountForWave = Mathf.RoundToInt(enemyAmountForWave * 1.1f);
        }
        if(enemies.Count < enemyAmountForWave*3)
        {
            int spawnChance = RAND.Next(1, 1000);
            if(spawnChance > 995)
            {
                Vector3 spawnPos = player.transform.position;
                int spawnX = RAND.Next(-3, 4);
                int spawnY = RAND.Next(0, 2);
                if(spawnY == 0)
                {
                    spawnPos.y += spawnDistance;
                } else
                {
                    spawnPos.y -= spawnDistance;
                }
                spawnPos.z = -1.5f;
                enemies.Add(Instantiate(enemyPrefabs[0], spawnPos, Quaternion.identity));
                enemyType.Add("Slime");
            }
        }
    }
}
