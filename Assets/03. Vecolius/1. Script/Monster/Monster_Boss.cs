using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Veco
{
    public class Monster_Boss : MonsterStateMono, IHitable
    {
        [SerializeField] MonsterStatusSO so;

        [SerializeField] Collider2D attackCol;
        DetectiveComponent detective;

        bool isAttackCooltime;
        IEnumerator attackCo;

        [SerializeField] Collider2D col;

        protected override void Awake()
        {
            base.Awake();
            detective = GetComponent<DetectiveComponent>();
            col = GetComponent<Collider2D>();
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
                ChangeMonsterState(MonsterState.idle);
                isAttackCooltime = false;
                StopCoroutine(attackCo);
            }
        }

        //status 초기화
        protected override void MonsterInit()
        {
            base.MonsterInit();

        }

        //attackType 설정
        public void AnimationAttackTypeSet()
        {
            int attackState = Random.Range(0, 2);
            Animator.SetFloat("attackState", attackState);
        }

        public void AnimationChangeMonsterState(int selectNumber)
        {
            ChangeMonsterState((MonsterState)selectNumber);
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

        public override void Dead()
        {
            isDie = true;
            col.enabled = false;
            attackCol.enabled = false;

            StopCoroutine(attackCo);
            ChangeMonsterState(MonsterState.die);
        }
    }
}
