using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Veco
{
    [Serializable]
    public class Enemy
    {
        public GameObject monsterPrefab;
        public string monsterName;
    }

    public class WaveManager : SingleTon<WaveManager>
    {
        static readonly int defaultMonsterSpawnCount = 2;           //몬스터 소환 수 디폴트 값

        [Header("몬스터 리스트")]
        public List<Enemy> monsterList = new List<Enemy>();     //monster spawn List

        [Header("웨이브 정보")]
        [SerializeField] float delayToWaveStart;        //웨이브가 시작할 때까지의 대기시간
        [SerializeField] int waveIndex;                     //이번 웨이브 숫자
        [SerializeField] float spawnCooltime;           //MonsterSpawnCo의 쿨타임
        [SerializeField] int waveSpawnCount;             //이번 웨이브에 소환될 몬스터 수(기준값)
        int waveMonsterCount;                              //이번 웨이브에 죽은 몬스터 수

        [Header("소환에 필요한 정보")]
        [SerializeField] Transform spawnPos;
        [SerializeField] List<GameObject> spawnMonsterList;       //소환될 몬스터들 list
        [SerializeField] int monsterSpawnCount;                     //이번 웨이브에 남은 소환 수 

        IEnumerator spawnCo;
        WaitForSeconds waitforSecond;

        public int WaveNumber => waveIndex + 1;                //웨이브 몇번째
        public int WaveIndex => waveIndex;
        public int WaveMonsterCount
        {
            get => waveMonsterCount;
            set
            {
                waveMonsterCount = value;
                if(waveMonsterCount == waveSpawnCount)     //소환된 몹이 다 죽으면
                {
                    StopCoroutine(spawnCo);
                    waveMonsterCount = 0;
                    waveIndex++;

                    Invoke("WaveStart", delayToWaveStart);
                }
            }
        }

        void Start()
        {
            waitforSecond = new WaitForSeconds(spawnCooltime);
            spawnMonsterList = new List<GameObject>();
            //spawnCo = SpawnCo();

            Invoke("WaveStart", delayToWaveStart);
            //StartCoroutine(spawnCo);
        }

        public void WaveStart()
        {
            spawnMonsterList.Clear();
            waveSpawnCount = defaultMonsterSpawnCount + waveIndex;
            monsterSpawnCount = waveSpawnCount;

            Debug.Log(WaveNumber + "번째 웨이브 : " + monsterSpawnCount);

            SpawnMonsterSave();
            spawnCo = MonsterSpawnCo();
            StartCoroutine(spawnCo);
        }

        void SpawnMonsterSave()
        {
            int listCount = monsterList.Count < waveIndex ? monsterList.Count+1 : waveIndex+1;
            int spawnValue = monsterSpawnCount;
            while(spawnValue > 0)
            {
                int randEnemyIndex = UnityEngine.Random.Range(0, listCount);
                spawnMonsterList.Add(monsterList[randEnemyIndex].monsterPrefab);
                spawnValue--;
            }
        }

        IEnumerator MonsterSpawnCo()
        {
            while (monsterSpawnCount > 0)
            {
                GameObject spawnMonsterObj = ObjectPoolManager.instance.PopObj(spawnMonsterList[0], spawnPos.position, Quaternion.identity);
                spawnMonsterList.RemoveAt(0);
                spawnMonsterObj.SetActive(true);
                //spawnMonsterObj.GetComponent<MonsterAI>().MonsterInit();

                Debug.Log(spawnMonsterObj.name + "소환");

                monsterSpawnCount--;

                yield return waitforSecond;

            }
        }
    }
}
