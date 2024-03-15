using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Veco;

namespace BK
{
    public class PState : State
    {
        public PlayerStatusMono PStatus = null; 
        public override void Init(IStateMachine sm)
        {
            base.Init(sm);
            PStatus = (PlayerStatusMono)sm.GetOwner();
        }
        public override void Update()
        {
            
        }
    }
    public class PlayerIdleState : PState
    {
        public override void Update()
        {
            PStatus.playerDC.Scan();
            Debug.Log("Å½Áö");
        }
    }
    public class PlayerSkillState : PState
    {
        public override void Update()
        {
            Debug.Log("½ºÅ³");
            //skill
        }
    }
    public class PlayerAttackState : PState
    {
        public override void Update()
        {
            if (PStatus.playerDC.col.TryGetComponent(out Enemy enemy))
            {
                if (enemy.die)
                    PStatus.playerDC.col = null;
            }
            //attack ani
        }
    }
    public class PlayerDieState : PState
    {
        public override void Update()
        {
            Debug.Log("Á×¾ú´Ù");
            //die ani
        }
    }



    public class PlayerStatusMono : MonoBehaviour
    {
        protected StateMachine<PlayerStatusMono> sm = null;
        [SerializeField] protected SkeletonAnimation skeletonAnimation = null;
        public PlayerDC playerDC = null;
        private void Awake()
        {
            skeletonAnimation = GetComponent<SkeletonAnimation>();
            sm = new StateMachine<PlayerStatusMono>();
            playerDC = GetComponent<PlayerDC>();
            sm.owner = this;
        }



    }
}
