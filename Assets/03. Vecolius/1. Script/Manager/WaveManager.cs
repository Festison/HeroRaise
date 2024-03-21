using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Veco
{
    [Serializable]
    public struct WaveInfo
    {
        public int waveCount;
        public int waveSpawnCount;
        public string monsterName;
        public GameObject monsterPrefab;
    }

    [Serializable]
    public class Wave
    {
        public List<WaveInfo> waveMonsterInfo;

        public Wave(List<WaveInfo> waveMonsterInfo)
        {
            this.waveMonsterInfo = waveMonsterInfo;
        }
    }

    public class WaveManager : SingleTon<WaveManager>
    {
        public List<WaveInfo> waveInfoList = new List<WaveInfo>();     //추후 파싱 대비용 정보모음집
        [SerializeField] Wave wave;

        [SerializeField] int monsterSpawnCount;         //이번 웨이브에 소환될 몬스터 수
        [SerializeField] int waveNumber;                   //이번 웨이브 숫자
        [SerializeField] bool isWaveOn;
        [SerializeField] float spawnCooltime;
        int waveMonsterCount;                              //이번 웨이브에 죽은 몬스터 수

        [SerializeField] Transform spawnPos;
        [SerializeField] GameObject spiderObj;
        GameObject spawnMonster;

        IEnumerator spawnCo;
        WaitForSeconds waitforSecond;

        public int WaveNumber => waveNumber + 1;                //웨이브 몇번째
        public int WaveMonsterCount
        {
            get => waveMonsterCount;
            set
            {
                waveMonsterCount = value;
                if(waveMonsterCount == waveInfoList[waveNumber].waveSpawnCount)     //소환된 몹이 다 죽으면
                {
                    waveMonsterCount = 0;
                    waveNumber++;
                    WaveStart();
                }
            }
        }

        void Start()
        {
            wave = new Wave(waveInfoList);
            waitforSecond = new WaitForSeconds(spawnCooltime);
            spawnCo = Spawn(waveInfoList[waveNumber].monsterPrefab);

            Invoke("WaveStart", 2.0f);
            //StartCoroutine(spawnCo);
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.Space))
                spawnMonster.GetComponent<MonsterAI>().Hit(100);
        }

        public void WaveStart()
        {
            //GameObject spawnMonster = ObjectPoolManager.instance.PopObj(wave.waveMonsterInfo[waveCount].monsterName);
            //spawnMonster = ObjectPoolManager.instance.PopObj(spiderObj, spawnPos.position, Quaternion.identity);
            monsterSpawnCount = waveInfoList[waveNumber].waveSpawnCount;
            StartCoroutine(spawnCo);
        }

        IEnumerator Spawn(GameObject monsterObj)
        {
            while (isWaveOn && monsterSpawnCount > 0)
            {
                ObjectPoolManager.instance.PopObj(spiderObj, spawnPos.position, Quaternion.identity);
                monsterSpawnCount--;

                yield return waitforSecond;

                if(monsterSpawnCount <= 0)
                {
                    isWaveOn = false;
                    StopCoroutine(spawnCo);
                }
            }
        }
    }
}
