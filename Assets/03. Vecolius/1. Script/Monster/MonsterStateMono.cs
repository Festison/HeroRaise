using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Veco
{
    public class MState : State
    {
        protected MonsterStateMono MSmono = null;
        public override void Init(IStateMachine sm)
        {
            base.Init(sm);
            MSmono = (MonsterStateMono)sm.GetOwner();
        }
        public override void Update()
        {
            MSmono.Animator.SetInteger("StateNumber", (int)MSmono.State);
        }
    }

    //Monster Idle State
    public class MonsterIdleState : MState
    {
        public override void Update()
        {
            base.Update();
        }
    }

    //Monster Move State
    public class MonsterRunState : MState
    {
        public override void Update()
        {
            base.Update();
        }
    }

    //Monster Attack State
    public class MonsterAttackState : MState
    {
        public override void Update()
        {
            base.Update();
        }
    }

    //Montster Die State
    public class MonsterDieState : MState
    {
        public override void Update()
        {
            base.Update();
        }
    }


    public abstract class MonsterStateMono : MonoBehaviour
    {
        protected StateMachine<MonsterStateMono> sm = null;
        [SerializeField] protected Animator animator = null;
        [SerializeField] protected MonsterStatus status;
        [SerializeField] protected MonsterState state;
        [SerializeField] protected bool isDie;

        public Animator Animator => animator;
        public MonsterStatus MonsterStatus => status;
        public MonsterState State => state;

        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
        }

        protected virtual void Start()
        {
            sm = new StateMachine<MonsterStateMono>();
            sm.owner = this;
        }

        protected virtual void MonsterInit()
        {
            state = MonsterState.idle;
            isDie = false;

        }

        //몬스터 상태 변화
        public void ChangeMonsterState(MonsterState state)
        {
            this.state = state;
            sm.SetState(state.ToString());
        }

        public abstract void Dead();
    }
}
