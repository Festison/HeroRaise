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
        public List<WaveInfo> waveInfos = new List<WaveInfo>();     //추후 파싱 대비용 정보모음집
        [SerializeField] Wave wave;

        public int monsterSpawnCount;
        public int waveCount;
        [SerializeField] bool isWaveOn;
        [SerializeField] float spawnCooltime;

        [SerializeField] Transform spawnPos;
        [SerializeField] GameObject spiderObj;
        GameObject spawnMonster;

        IEnumerator spawnCo;
        WaitForSeconds waitforSecond;
        void Start()
        {
            wave = new Wave(waveInfos);
            waitforSecond = new WaitForSeconds(spawnCooltime);
            spawnCo = Spawn(spiderObj);

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
            StartCoroutine(spawnCo);
        }

        IEnumerator Spawn(GameObject monsterObj)
        {
            while (isWaveOn)
            {
                ObjectPoolManager.instance.PopObj(spiderObj, spawnPos.position, Quaternion.identity);
                yield return waitforSecond;
            }
        }
    }
}
