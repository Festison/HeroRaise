using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Veco
{
    //wave round ���� ���� �� �ҷ����� 
    public class CSVtoWaveData
    {
        //���̺� ���� �ҷ�����
        public static void LoadToWaveData(string fileName)
        {
           var dataList = new List<WaveInfo>();
           TextAsset textData = Resources.Load(fileName) as TextAsset;

           
        }
        //���̺� ���� ����
        public static void SaveToWaveData(string fileName, WaveInfo waveInfo)
        {

        }
    }
}
