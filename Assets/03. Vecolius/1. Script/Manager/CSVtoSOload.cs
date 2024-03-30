using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Veco
{
    //wave round 정보 저장 및 불러오기 
    public class CSVtoWaveData
    {
        //웨이브 정보 불러오기
        public static void LoadToWaveData(string fileName)
        {
           var dataList = new List<Enemy>();
           TextAsset textData = Resources.Load(fileName) as TextAsset;

           
        }
        //웨이브 정보 저장
        public static void SaveToWaveData(string fileName, Enemy waveInfo)
        {

        }
    }
}
