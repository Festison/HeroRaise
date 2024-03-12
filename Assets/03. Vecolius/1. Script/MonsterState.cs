using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Veco
{
    public class MonsterIdleState : State
    {
        public override void Enter()
        {
            base.Enter();
        }
        public override void Update()
        {
            
        }
        public override void Exit()
        {
            base.Exit();
        }
    }
    public class MonsterMoveState : State
    {

    }
    public class MonsterAttackState : State
    {

    }
    public class MonsterDieState : State
    {

    }

    public class MonsterState : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
