using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using System;
using Veco;

// Controller
// 게임의 핵심 로직들을 담당한다.
// Model들을 조작하고 업데이트된 Model들을 View에 통지해준다.

namespace Festioson
{
    public class PlayerController : MonoBehaviour, IAttackable, IHitable
    {
        private SkeletonAnimation skeletonAnimation;
        private Animator animator;
        private PlayerView playerView;
        private event Action StartLogic;
        [SerializeField] LayerMask monsterLayer;
        private bool isDamage = false;

        private void Start()
        {
            skeletonAnimation = GetComponent<SkeletonAnimation>();
            playerView = GetComponent<PlayerView>();
            animator = GetComponent<Animator>();
            StartLogic += ViewUpdate;
            StartLogic();
        }


        private void FixedUpdate()
        {
            AttackRayCast();
        }
        #region 이벤트 로직
        public void ViewUpdate()
        {
            playerView.UpdateUI(DataManager.Instance.playerData);
            playerView.ChangeSkin(skeletonAnimation, DataManager.Instance.playerData.Level);
        }

        public void LevelUp()
        {          
            if (DataManager.Instance.PlayerItem.gold > 0)
            {
                DataManager.Instance.PlayerItem.gold -= 1000;
                DataManager.Instance.playerData.Level++;
                DataManager.Instance.playerData.MaxHp += 10;
                DataManager.Instance.playerData.Hp += 10;
                DataManager.Instance.playerData.Damage += 2;
            }
            
            playerView.ChangeSkin(skeletonAnimation, DataManager.Instance.playerData.Level);
        }
        #endregion

        #region 상태 로직
        public void SkillAnimation()
        {
            skeletonAnimation.AnimationName = "skill";
            Invoke("DefalutAniamtion", 2.167f);
            animator.Play("skill");
        }

        public void DefalutAniamtion()
        {
            skeletonAnimation.AnimationName = "attack";
        }
        #endregion

        #region 충돌 로직

        float lastAttackTime = 0f; // 마지막 공격 시간을 저장할 변수
        float attackCooldown = 0.65f; // 공격 쿨다운 시간 (1초)

        public void AttackRayCast()
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                Debug.DrawRay(transform.position + new Vector3(0, 0.3f, 0), transform.right, Color.yellow);

                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + new Vector3(0, 0.3f, 0), transform.right, 0.5f, monsterLayer);

                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider.TryGetComponent<IHitable>(out IHitable monster) && isDamage)
                    {
                        Attack(monster);
                        Debug.Log("공격");
                        lastAttackTime = Time.time; // 공격 시간을 업데이트
                    }
                }
            }
        }

        public void StartDmage()
        {
            isDamage = true;
        }

        public void EndDmage()
        {
            isDamage = false;
        }
        public void Attack(IHitable hitable)
        {
            hitable.Hit(DataManager.Instance.playerData.Damage);
        }

        public void Hit(int damage)
        {
            Debug.Log("플레이어 피격");
            DataManager.Instance.playerData.Hp -= damage;
            UIManager.Instance.HpLerpUI();
        }
        #endregion
    }
}

