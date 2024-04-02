using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using System;
using Veco;

// Controller
// ������ �ٽ� �������� ����Ѵ�.
// Model���� �����ϰ� ������Ʈ�� Model���� View�� �������ش�.

namespace Festioson
{
    public class PlayerController : MonoBehaviour, IAttackable, IHitable
    {
        private SkeletonAnimation skeletonAnimation;
        private Animator animator;
        private PlayerView playerView;
        private event Action StartLogic;

        private void Start()
        {
            skeletonAnimation = GetComponent<SkeletonAnimation>();
            playerView = GetComponent<PlayerView>();
            animator = GetComponent<Animator>();
            StartLogic += ViewUpdate;
            StartLogic();
        }

        private void Update()
        {
            AttackRayCast();
        }

        #region �̺�Ʈ ����
        public void ViewUpdate()
        {
            playerView.UpdateUI(DataManager.Instance.playerData);
            playerView.ChangeSkin(skeletonAnimation, DataManager.Instance.playerData.Level);
        }

        public void LevelUp()
        {
            DataManager.Instance.playerData.Level++;
            DataManager.Instance.playerData.MaxHp += 10;
            DataManager.Instance.playerData.Hp += 10;
            DataManager.Instance.playerData.Damage += 2;

            playerView.ChangeSkin(skeletonAnimation, DataManager.Instance.playerData.Level);
        }
        #endregion

        #region ���� ����
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

        #region �浹 ����
        public void AttackRayCast()
        {
            Debug.DrawRay(transform.position + new Vector3(0, 0.3f, 0), transform.right, Color.yellow);

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + new Vector3(0, 0.3f, 0), Vector3.right, DataManager.Instance.playerData.attackRange); ;

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.TryGetComponent<MonsterAI>(out MonsterAI monster))
                {
                    Attack(monster);
                }
            }
        }

        public void StartDealDamage()
        {
            DataManager.Instance.playerData.canDealDamage = true;
        }
        public void EndDealDamage()
        {
            DataManager.Instance.playerData.canDealDamage = false;
        }

        public void Attack(IHitable hitable)
        {
            hitable.Hit(DataManager.Instance.playerData.damage);
        }

        public void Hit(int damage)
        {
            DataManager.Instance.playerData.Hp -= damage;
        }
        #endregion
    }
}

