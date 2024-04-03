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
        static readonly int defaultMonsterSpawnCount = 2;           //���� ��ȯ �� ����Ʈ ��

        [Header("���� ����Ʈ")]
        public List<Enemy> monsterList = new List<Enemy>();     //monster spawn List

        [Header("���̺� ����")]
        [SerializeField] float delayToWaveStart;        //���̺갡 ������ �������� ���ð�
        [SerializeField] int waveIndex;                     //�̹� ���̺� ����
        [SerializeField] float spawnCooltime;           //MonsterSpawnCo�� ��Ÿ��
        [SerializeField] int waveSpawnCount;             //�̹� ���̺꿡 ��ȯ�� ���� ��(���ذ�)
        int waveMonsterCount;                              //�̹� ���̺꿡 ���� ���� ��

        [Header("��ȯ�� �ʿ��� ����")]
        [SerializeField] Transform spawnPos;
        [SerializeField] List<GameObject> spawnMonsterList;       //��ȯ�� ���͵� list
        [SerializeField] int monsterSpawnCount;                     //�̹� ���̺꿡 ���� ��ȯ �� 

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
                if(waveMonsterCount == waveSpawnCount)     //��ȯ�� ���� �� ������
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

            Debug.Log(WaveNumber + "��° ���̺� : " + monsterSpawnCount);

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

                Debug.Log(spawnMonsterObj.name + "��ȯ");

                monsterSpawnCount--;

                yield return waitforSecond;

            }
        }
    }
}
