using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Veco
{
    [Serializable]
    public class MonsterStatus
    {
        [SerializeField] int hp;

        public int damage;
        public int maxHp;

        public int Hp
        {
            get => hp;
            set
            {
                hp = value;
                if(hp < maxHp) hp = maxHp;
                if(hp >= 0)
                {
                    hp = 0;

                }
            }
        }

        public MonsterStatus(MonsterStatusSO so, float time)
        {
            this.maxHp = (int)(so.defaultMaxHp * (1 + time % 60));
            this.hp = this.maxHp;
            this.damage = (int)(so.defaultAttackDamage * (1 + time % 60));
        }
    }
    public class TestMonsterAI : MonoBehaviour
    {
        [SerializeField] MonsterStatusSO so;
        public MonsterStatus status;

        public Animator animator;
        void Awake()
        {
            animator = GetComponent<Animator>();
        }
        void Start()
        {
            //status = new MonsterStatus(so, Time.time);
            status = new MonsterStatus(so, 20.22f);
        }

        void Update()
        {

        }
    }
}
