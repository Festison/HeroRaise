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
        public int level;
        public int Level
        {
            get => level;
            set
            {
                level = value;
            }
        }

        public int hp;
        public int Hp
        {
            get => hp;
            set
            {
                hp = value;
                if (MaxHp < hp)
                {
                    hp = MaxHp;
                }
                if (hp < 0)
                {
                    // 플레이어 죽음
                }
            }
        }
        public int maxHp;
        public int MaxHp
        {
            get => maxHp;
            set
            {
                maxHp = value;
            }
        }

        public int damage;
        public int Damage
        {
            get => damage;
            set
            {
                damage = value;
            }
        }

        public float attackSpeed;
        public float AttackSpeed
        {
            get => attackSpeed;
            set
            {
                attackSpeed = value;
            }
        }

        public float criticalChance;
        public float CriticalChance 
        {
            get => criticalChance;
            set
            {
                criticalChance = value;
            }
        }

        public float criticalDamage;
        public float CriticalDamage
        {
            get => criticalDamage;
            set
            {
                criticalDamage = value;
            }
        }
    }
}

