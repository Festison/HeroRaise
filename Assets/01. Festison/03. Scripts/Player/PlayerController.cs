using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

namespace Festioson
{
    public class PlayerController : MonoBehaviour
    {
        private SkeletonAnimation skeletonAnimation;

        private void Start()
        {
            skeletonAnimation = GetComponent<SkeletonAnimation>();
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
    }
}

