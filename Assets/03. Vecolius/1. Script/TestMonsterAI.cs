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
        TestMonsterAI owner = null;
        [SerializeField] int hp;
        [SerializeField] int damage;
        [SerializeField] int maxHp;

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
                    owner.StopCoroutine(owner.attackCo);
                    owner.ChangeMonsterState(MonsterState.die);
                }
            }
        }

        public MonsterStatus(MonsterStatusSO so, float time, object owner)
        {
            this.owner = (TestMonsterAI)owner;
            this.maxHp = (int)(so.defaultMaxHp * (1 + time % 60));
            this.hp = this.maxHp;
            this.damage = (int)(so.defaultAttackDamage * (1 + time % 60));
        }
    }
    public class TestMonsterAI : MonsterStateMono, IHitable
    {
        [SerializeField] MonsterStatusSO so;
        [SerializeField] MonsterStatus status;
        [SerializeField] bool isAttackCooltime;
        [SerializeField] bool isDie;
        public IEnumerator attackCo;

        [SerializeField] DetectiveComponent detective;
        protected override void Awake()
        {
            base.Awake();
            detective = GetComponent<DetectiveComponent>();
            
        }
        protected override void Start()
        {
            base.Start();
            status = new MonsterStatus(so, Time.time, this);
            detective.DetectiveRange = so.attackRange;
            isAttackCooltime = false;
            isDie = false;
            attackCo = MonsterAttackCo(so.attackSpeed);

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

            if (isDie) return;

            if (Input.GetKeyDown(KeyCode.V))
                status.Hp -= status.Hp;

            if (detective.IsFind && !isAttackCooltime)
            {
                StartCoroutine(attackCo);
            }
            else if (detective.IsFind && isAttackCooltime)
            {
                //ChangeMonsterState(MonsterState.idle);
            }
            else
            {
                ChangeMonsterState(MonsterState.run);
                transform.Translate(Vector3.left * so.moveSpeed);
            }
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out IAttackable attackable))
            {
                //StartCoroutine(MonsterAttackCo());
            }
        }

        public void ChangeMonsterState(MonsterState state)
        {
            this.state = state;
            sm.SetState(state);
        }

        IEnumerator MonsterAttackCo(float attackCooltime)
        {
            while (!isAttackCooltime)
            {
                ChangeMonsterState(MonsterState.attack);
                isAttackCooltime = true;
                yield return new WaitForSeconds(attackCooltime);
                isAttackCooltime = false;
            }
        }

        public void Hit(int damage)
        {
            status.Hp -= damage;
        }

        public void AnimationChangeMonsterState(int selectNumber)
        {
            ChangeMonsterState((MonsterState)selectNumber);
        }

    }
}
