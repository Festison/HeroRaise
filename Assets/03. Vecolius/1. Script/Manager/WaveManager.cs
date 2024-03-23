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
        public List<WaveInfo> waveInfoList = new List<WaveInfo>();     //���� �Ľ� ���� ����������
        [SerializeField] Wave wave;

        [SerializeField] int monsterSpawnCount;         //�̹� ���̺꿡 ��ȯ�� ���� ��
        [SerializeField] int waveNumber;                   //�̹� ���̺� ����
        [SerializeField] bool isWaveOn;
        [SerializeField] float spawnCooltime;
        int waveMonsterCount;                              //�̹� ���̺꿡 ���� ���� ��

        [SerializeField] Transform spawnPos;
        [SerializeField] GameObject spiderObj;
        GameObject spawnMonster;

        Coroutine spawnCo;
        WaitForSeconds waitforSecond;

        public int WaveNumber => waveNumber + 1;                //���̺� ���°
        public int WaveMonsterCount
        {
            get => waveMonsterCount;
            set
            {
                waveMonsterCount = value;
                if(waveMonsterCount == waveInfoList[waveNumber].waveSpawnCount)     //��ȯ�� ���� �� ������
                {
                    StopCoroutine(spawnCo);
                    waveMonsterCount = 0;
                    waveNumber++;
                    Debug.Log("�������̺�");
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
            Debug.Log(WaveNumber + "��° ���̺� : " + monsterSpawnCount);
            spawnCo = StartCoroutine(SpawnCo());
        }

        IEnumerator SpawnCo()
        {
            Debug.Log("coroutine");
            while (monsterSpawnCount > 0)
            {
                ObjectPoolManager.instance.PopObj(waveInfoList[waveNumber].monsterPrefab, spawnPos.position, Quaternion.identity);
                Debug.Log(waveInfoList[waveNumber].monsterPrefab.name + "��ȯ");
                monsterSpawnCount--;

                yield return waitforSecond;

            }
        }
    }
}
