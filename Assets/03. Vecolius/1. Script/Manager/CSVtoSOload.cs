using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Veco
{
    public class CSVtoSOload
    {
        public static void LoadToMonsterData(string fileName)
        {
            var dataList = new List<MonsterStatusSO>();
            TextAsset textData = Resources.Load(fileName) as TextAsset;

           
        }
    }
}
