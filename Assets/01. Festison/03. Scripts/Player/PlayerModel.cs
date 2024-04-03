using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Model
// ������ �����Ͱ� ����Ǵ� ��ũ��Ʈ
// ������ �ƴ� ���� �����Ͱ� ���� �Ѵ�.
// �䳪 ��Ʈ�ѷ��� ���� ��� ������ ������ ����.

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
                    // �÷��̾� ����
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

