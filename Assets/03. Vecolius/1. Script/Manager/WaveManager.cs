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

        [SerializeField] Transform spawnPos;
        [SerializeField] GameObject spiderObj;
        void Start()
        {
            wave = new Wave(waveInfos);
            Invoke("WaveStart", 2.0f);
        }

        void Update()
        {

        }

        public void WaveStart()
        {
            //GameObject spawnMonster = ObjectPoolManager.instance.PopObj(wave.waveMonsterInfo[waveCount].monsterName);
            GameObject spawnMonster = ObjectPoolManager.instance.PopObj(spiderObj, spawnPos.position, Quaternion.identity);
        }
    }
}
