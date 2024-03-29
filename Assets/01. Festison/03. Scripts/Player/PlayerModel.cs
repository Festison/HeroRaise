using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Model
// 게임의 데이터가 저장되는 스크립트
// 로직이 아닌 순수 데이터가 들어가야 한다.
// 뷰나 컨트롤러에 대한 어떠한 정보도 가지지 않음.

namespace Festioson
{
    public class PlayerModel
    {
        public int Level { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int Damage { get; set; }
        public float AttackSpeed { get; set; }
        public float CriticalChance { get; set; }
        public float CriticalDamage { get; set; }
    }
}

