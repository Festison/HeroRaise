using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Veco
{
    public class MState : State
    {
        public MonsterStateMono MSmono = null;
        public override void Init(IStateMachine sm)
        {
            base.Init(sm);
            MSmono = (MonsterStateMono)sm.GetOwner();
        }
    }

    //Monster Idle State
    public class MonsterIdleState : MState
    {
        public override void Update()
        {
            MSmono.Animator.SetInteger("StateNumber", (int)MSmono.State);
            Debug.Log("�⺻���");
        }
    }

    //Monster Move State
    public class MonsterRunState : MState
    {
        public override void Update()
        {
            MSmono.Animator.SetInteger("StateNumber", (int)MSmono.State);
            Debug.Log("�̵����");
        }
    }

    //Monster Attack State
    public class MonsterAttackState : MState
    {
        public override void Update()
        {
            MSmono.Animator.SetInteger("StateNumber", (int)MSmono.State);
            Debug.Log("���ݸ��");
        }
    }

    //Montster Die State
    public class MonsterDieState : MState
    {
        public override void Update()
        {
            MSmono.Animator.SetInteger("StateNumber", (int)MSmono.State);
            Debug.Log("������");
        }
    }


    public class MonsterStateMono : MonoBehaviour
    {
        protected StateMachine<MonsterStateMono> sm = null;
        [SerializeField] protected Animator animator = null;
        [SerializeField] protected MonsterState state;

        public Animator Animator => animator;
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
    }
}
