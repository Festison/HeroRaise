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
        public int Level { get; set; }
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int Damage { get; set; }
        public float AttackSpeed { get; set; }
        public float CriticalChance { get; set; }
        public float CriticalDamage { get; set; }
    }
}

