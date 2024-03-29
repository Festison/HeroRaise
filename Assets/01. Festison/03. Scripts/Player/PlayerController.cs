using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using System;

// Controller
// 게임의 핵심 로직들을 담당한다.
// Model들을 조작하고 업데이트된 Model들을 View에 통지해준다.

namespace Festioson
{
    public class PlayerController : MonoBehaviour
    {
        private SkeletonAnimation skeletonAnimation;
        private PlayerView playerView;
        private PlayerModel playerModel;
        public event Action StartController;

        private void Start()
        {
            skeletonAnimation = GetComponent<SkeletonAnimation>();
            playerView = GetComponent<PlayerView>();
            playerModel = new PlayerModel();
            StartController += InitStat;
            StartController += ViewUpdate;
            StartController();
        }

        public void SkillAnimation()
        {
            skeletonAnimation.AnimationName = "skill";
            Invoke("DefalutAniamtion", 2.167f);
        }

        public void DefalutAniamtion()
        {
            skeletonAnimation.AnimationName = "attack";
        }

        public void ViewUpdate()
        {
            playerView.UpdateLevel(playerModel.Level);
            playerView.UpdateHp(playerModel.Hp, playerModel.MaxHp);
            playerView.UpdateDamage(playerModel.Damage);
            playerView.UpdateAttackSpeed(playerModel.AttackSpeed);
            playerView.UpdateCriticalChance(playerModel.CriticalChance);
            playerView.CriticalDamage(playerModel.CriticalDamage);
        }

        public void InitStat()
        {
            playerModel.Level = 1;
            playerModel.Hp = 100;
            playerModel.MaxHp = 100;
            playerModel.Damage = 10;
            playerModel.AttackSpeed = 1.0f;
            playerModel.CriticalChance = 5f;
            playerModel.CriticalDamage = 1.25f;
        }

        public void LevelUp()
        {
            playerModel.Level++;
            playerModel.Hp += 10;
            playerModel.MaxHp += 10;
            playerModel.Damage += 2;
        }
    }
}

