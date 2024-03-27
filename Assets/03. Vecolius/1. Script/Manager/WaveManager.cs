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

        [Header("���̺� ����")]
        [SerializeField] float delayToWaveStart;        //���̺갡 ������ �������� ���ð�
        [SerializeField] int monsterSpawnCount;         //�̹� ���̺꿡 ��ȯ�� ���� ��
        [SerializeField] int waveIndex;                //�̹� ���̺� ����
        [SerializeField] bool isWaveOn;
        [SerializeField] float spawnCooltime;
        int waveMonsterCount;                              //�̹� ���̺꿡 ���� ���� ��

        [Header("��ȯ�� �ʿ��� ����")]
        [SerializeField] Transform spawnPos;
        [SerializeField] GameObject spiderObj;
        GameObject spawnMonster;

        IEnumerator spawnCo;
        WaitForSeconds waitforSecond;

        public int WaveNumber => waveIndex + 1;                //���̺� ���°
        public int WaveIndex => waveIndex;
        public int WaveMonsterCount
        {
            get => waveMonsterCount;
            set
            {
                waveMonsterCount = value;
                if(waveMonsterCount == waveInfoList[waveIndex].waveSpawnCount)     //��ȯ�� ���� �� ������
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
            Debug.Log(WaveNumber + "��° ���̺� : " + monsterSpawnCount);

            spawnCo = SpawnCo();
            StartCoroutine(spawnCo);
        }

        IEnumerator SpawnCo()
        {
            while (monsterSpawnCount > 0)
            {
                ObjectPoolManager.instance.PopObj(waveInfoList[waveIndex].monsterPrefab, spawnPos.position, Quaternion.identity);
                Debug.Log(waveInfoList[waveIndex].monsterPrefab.name + "��ȯ");
                monsterSpawnCount--;

                yield return waitforSecond;

            }
        }
    }
}
