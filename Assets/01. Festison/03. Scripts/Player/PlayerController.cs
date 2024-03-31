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
        public event Action StartController;

        private void Start()
        {
            skeletonAnimation = GetComponent<SkeletonAnimation>();
            playerView = GetComponent<PlayerView>();
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
    }
}

