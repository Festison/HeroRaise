using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Veco {

    public class TestSpawner : MonoBehaviour
    {
        public List<Enemy> enemies = new List<Enemy>();     //소환할 몬스터 종류
        public List<GameObject> enemiesToSpawn;              //실제로 소환되는 몬스터들

        public int currentWave;                                 //현재 웨이브
        public int waveValue;                                   //현재 웨이브의 소환 가능값


        public Transform spawnPos;                  //소환 위치
        public int waveDuration;
        float waveTimer;
        float spawnInterval;
        float spawnTimer;

        void Start()
        {
            enemiesToSpawn = new List<GameObject>();
            GenerateWave();
        }

        void Update()
        {
            if(spawnTimer <= 0)
            {
                if(enemiesToSpawn.Count > 0)
                {
                    Instantiate(enemiesToSpawn[0], spawnPos.position, Quaternion.identity);
                    enemiesToSpawn.RemoveAt(0);
                    spawnTimer = spawnInterval;
                }
                else
                {
                    waveTimer = 0;

                }
            }
            else
            {
                spawnTimer -= Time.deltaTime;
                waveTimer -= Time.deltaTime;
            }
        }

        public void GenerateWave()
        {
            waveValue = currentWave * 10;
            Debug.Log(waveValue);
            //GenerateEnermies();

            spawnInterval = enemiesToSpawn.Count > 0 ? waveDuration / enemiesToSpawn.Count : 0;
            waveTimer = waveDuration;
        }
        /*
        public void GenerateEnermies()
        {
            List<GameObject> generatedEnermies = new List<GameObject>();
            while(waveValue > 0)
            {
                int randEnemyId = UnityEngine.Random.Range(0, enemies.Count);
                int randEnemyCost = enemies[randEnemyId].cost;

                if (waveValue - randEnemyCost >= 0)
                {
                    generatedEnermies.Add(enemies[randEnemyId].monsterPrefab);
                    waveValue -= randEnemyCost;
                }
                else if (waveValue <= 0)
                    break;

            }
            enemiesToSpawn.Clear();
            enemiesToSpawn = generatedEnermies;
        }
        */
    }
}
