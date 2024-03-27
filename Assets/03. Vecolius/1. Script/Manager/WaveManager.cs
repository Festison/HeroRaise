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

        [Header("웨이브 정보")]
        [SerializeField] float delayToWaveStart;        //웨이브가 시작할 때까지의 대기시간
        [SerializeField] int monsterSpawnCount;         //이번 웨이브에 소환될 몬스터 수
        [SerializeField] int waveIndex;                //이번 웨이브 숫자
        [SerializeField] bool isWaveOn;
        [SerializeField] float spawnCooltime;
        int waveMonsterCount;                              //이번 웨이브에 죽은 몬스터 수

        [Header("소환에 필요한 정보")]
        [SerializeField] Transform spawnPos;
        [SerializeField] GameObject spiderObj;
        GameObject spawnMonster;

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
                if(waveMonsterCount == waveInfoList[waveIndex].waveSpawnCount)     //소환된 몹이 다 죽으면
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
            wave = new Wave(waveInfoList);
            waitforSecond = new WaitForSeconds(spawnCooltime);
            //spawnCo = SpawnCo();

            Invoke("WaveStart", delayToWaveStart);
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
            monsterSpawnCount = waveInfoList[waveIndex].waveSpawnCount;
            Debug.Log(WaveNumber + "번째 웨이브 : " + monsterSpawnCount);

            spawnCo = SpawnCo();
            StartCoroutine(spawnCo);
        }

        IEnumerator SpawnCo()
        {
            while (monsterSpawnCount > 0)
            {
                ObjectPoolManager.instance.PopObj(waveInfoList[waveIndex].monsterPrefab, spawnPos.position, Quaternion.identity);
                Debug.Log(waveInfoList[waveIndex].monsterPrefab.name + "소환");
                monsterSpawnCount--;

                yield return waitforSecond;

            }
        }
    }
}
