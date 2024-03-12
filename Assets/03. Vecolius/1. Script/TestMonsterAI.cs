using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Veco
{
    public enum MonsterState
    {
        idle,
        run,
        attack,
        die,
    }

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
        [SerializeField] MonsterState state;

        public Animator animator;
        void Awake()
        {
            animator = GetComponent<Animator>();
        }
        void Start()
        {
            status = new MonsterStatus(so, Time.time);
        }

        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                state = MonsterState.idle;
                ChangeAnimation(state);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                state = MonsterState.run;
                ChangeAnimation(state);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                state = MonsterState.attack;
                ChangeAnimation(state);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                state = MonsterState.die;
                ChangeAnimation(state);
            }
            
        }

        void ChangeAnimation(MonsterState state)
        {
            animator.SetInteger("StateNumber", (int)state);
        }
    }
}
