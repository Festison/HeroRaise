using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VHierarchy.Libs;

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
        public int WaveIndex
        {
            get=> waveIndex;
            set => waveIndex = value;
        }

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
                    DataManager.Instance.PlayerItem.waveData++;

                    Invoke("WaveStart", delayToWaveStart);
                }
            }
        }

        void Start()
        {
            waitforSecond = new WaitForSeconds(spawnCooltime);
            spawnMonsterList = new List<GameObject>();
            //spawnCo = SpawnCo();

            //Debug.Log("waveIndex : "+DataManager.Instance.PlayerItem.waveData);
            //waveIndex = DataManager.Instance.PlayerItem.waveData;

            //Invoke("WaveStart", delayToWaveStart);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                GameObject.FindAnyObjectByType<MonsterStateMono>().Dead();
        }

        public void InvokeWaveStart()
        {
            waveIndex = DataManager.Instance.PlayerItem.waveData;
            Invoke("WaveStart", delayToWaveStart);
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
            int listCount = monsterList.Count < waveIndex ? monsterList.Count : waveIndex;
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
