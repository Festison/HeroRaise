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
    public class TestMonsterAI : MonsterStateMono, IHitable
    {
        [SerializeField] MonsterStatusSO so;
        public MonsterStatus status;

        protected override void Awake()
        {
            base.Awake();
        }
        protected override void Start()
        {
            base.Start();
            status = new MonsterStatus(so, Time.time);

            sm.AddState(MonsterState.idle, new MonsterIdleState());
            sm.AddState(MonsterState.run, new MonsterRunState());
            sm.AddState(MonsterState.attack, new MonsterAttackState());
            sm.AddState(MonsterState.die, new MonsterDieState());

            state = MonsterState.idle;
            sm.SetState(state);
        }

        void Update()
        {
            sm.Update();

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ChangeAnimation(MonsterState.idle);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ChangeAnimation(MonsterState.run);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ChangeAnimation(MonsterState.attack);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ChangeAnimation(MonsterState.die);
            }
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out IAttackable attackable))
            {

            }
        }

        void ChangeAnimation(MonsterState state)
        {
            this.state = state;
            sm.SetState(state);
        }

        public void Hit(int damage)
        {
            status.Hp -= damage;
        }
    }
}
