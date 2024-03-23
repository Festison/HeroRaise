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

        Coroutine spawnCo;
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
                    StopCoroutine(spawnCo);
                    waveMonsterCount = 0;
                    waveNumber++;
                    Debug.Log("다음웨이브");
                    Invoke("WaveStart", 2.0f);
                }
            }
        }

        void Start()
        {
            wave = new Wave(waveInfoList);
            waitforSecond = new WaitForSeconds(spawnCooltime);
            //spawnCo = SpawnCo();

            Invoke("WaveStart", 2.0f);
            //StartCoroutine(spawnCo);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                GameObject.FindAnyObjectByType<MonsterAI>().Hit(100);
        }

        public void WaveStart()
        {
            //GameObject spawnMonster = ObjectPoolManager.instance.PopObj(wave.waveMonsterInfo[waveCount].monsterName);
            //spawnMonster = ObjectPoolManager.instance.PopObj(spiderObj, spawnPos.position, Quaternion.identity);
            monsterSpawnCount = waveInfoList[waveNumber].waveSpawnCount;
            Debug.Log(WaveNumber + "번째 웨이브 : " + monsterSpawnCount);
            spawnCo = StartCoroutine(SpawnCo());
        }

        IEnumerator SpawnCo()
        {
            Debug.Log("coroutine");
            while (monsterSpawnCount > 0)
            {
                ObjectPoolManager.instance.PopObj(waveInfoList[waveNumber].monsterPrefab, spawnPos.position, Quaternion.identity);
                Debug.Log(waveInfoList[waveNumber].monsterPrefab.name + "소환");
                monsterSpawnCount--;

                yield return waitforSecond;

            }
        }
    }
}
