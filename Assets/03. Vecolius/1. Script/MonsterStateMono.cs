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
            
        }
    }

    //Monster Move State
    public class MonsterMoveState : MState
    {
        public override void Update()
        {

        }
    }

    //Monster Attack State
    public class MonsterAttackState : MState
    {
        public override void Update()
        {

        }
    }

    //Montster Die State
    public class MonsterDieState : MState
    {
        public override void Update()
        {

        }
    }


    public class MonsterStateMono : MonoBehaviour
    {
        protected StateMachine<MonsterStateMono> sm = null;
        public Animator animator = null;
        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        void Start()
        {
            sm = new StateMachine<MonsterStateMono>();
            sm.owner = this;
        }
    }
}
