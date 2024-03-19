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
        MonsterAI owner = null;
        [SerializeField] int hp;
        public int damage;
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
                    owner.Dead();
                }
            }
        }

        public MonsterStatus(MonsterStatusSO so, float time, object owner)
        {
            this.owner = (MonsterAI)owner;
            this.maxHp = (int)(so.defaultMaxHp * (1 + time % 60));
            this.hp = this.maxHp;
            this.damage = (int)(so.defaultAttackDamage * (1 + time % 60));
        }
    }
    public class MonsterAI : MonsterStateMono, IHitable
    {
        [SerializeField] MonsterStatusSO so;
        [SerializeField] MonsterStatus status;
        [SerializeField] bool isAttackCooltime;
        IEnumerator attackCo;

        [SerializeField] DetectiveComponent detective;
        public Collider2D attackCol;

        public MonsterStatus MonsterStatus => status;

        protected override void Awake()
        {
            base.Awake();
            detective = GetComponent<DetectiveComponent>();
            status = new MonsterStatus(so, Time.time, this);
        }
        protected override void Start()
        {
            base.Start();
            detective.DetectiveRange = so.attackRange;
            isAttackCooltime = false;
            isDie = false;
            attackCo = MonsterAttackCo(so.attackSpeed);

            sm.AddState(MonsterState.idle.ToString(), new MonsterIdleState());
            sm.AddState(MonsterState.run.ToString(), new MonsterRunState());
            sm.AddState(MonsterState.attack.ToString(), new MonsterAttackState());
            sm.AddState(MonsterState.die.ToString(), new MonsterDieState());

            sm.GetState(MonsterState.attack.ToString()).onEnter += () => { attackCol.enabled = true; };
            sm.GetState(MonsterState.attack.ToString()).onExit += () => { attackCol.enabled = false; };

            state = MonsterState.idle;
            sm.SetState(state.ToString());
        }

        void Update()
        {
            sm.Update();

            if (isDie) return;



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

        //몬스터 status 초기화
        public override void MonsterInit()
        {
            base.MonsterInit();
            status = new MonsterStatus(so, Time.time, this);
        }

        //몬스터 상태 변화
        public void ChangeMonsterState(MonsterState state)
        {
            this.state = state;
            sm.SetState(state.ToString());
        }

        public void Dead()
        {
            isDie = true;
            GetComponent<Collider2D>().enabled = false;
            attackCol.enabled = false;

            StopCoroutine(attackCo);
            ChangeMonsterState(MonsterState.die);
        }

        public void AnimationChangeMonsterState(int selectNumber)
        {
            ChangeMonsterState((MonsterState)selectNumber);
        }

        public void GameObjectDead()
        {
            ObjectPoolManager.Instance.ReturnPool(gameObject);

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
            Debug.Log(gameObject.name + " Hp : " + status.Hp);
        }

    }
}
