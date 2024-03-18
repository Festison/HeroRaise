using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Veco
{
    [CreateAssetMenu]
    public class MonsterStatusSO : ScriptableObject
    {
        public string monsterName;
        public int defaultMaxHp;
        public float moveSpeed;
        public float attackSpeed;
        public int defaultAttackDamage;
        public float attackRange;
        public int gold;
    }
}
