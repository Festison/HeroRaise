using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using TMPro;
using Festioson;
using UnityEngine.UI;

// View
// 게임 내 외적으로 보이는 모든 요소를 조작한다.
// Controller에서 받은 데이터를 화면에 출력한다.
// Model에 대한 정보를 따로 저장해서는 안됨.
// Model이나 Controller에 대한 정보를 가지면 안됨.
// 재사용이 가능하면 최고!

public interface ISkinChanger
{
    void ChangeSkin(SkeletonAnimation skeletonAnimation, int level);
}

public interface IView
{
    public void UpdateUI(PlayerModel playerData);
}

namespace Festioson
{
    public class PlayerView : MonoBehaviour, IView, ISkinChanger
    {
        [Header("플레이어 스텟 뷰")]
        public TextMeshProUGUI[] playerTextView = new TextMeshProUGUI[6];
        public Image playerImage;

        public void UpdateUI(PlayerModel playerData)
        {
            playerTextView[0].text = "Lv. " + playerData.Level;
            playerTextView[1].text = playerData.Hp + " / " + playerData.MaxHp;
            playerTextView[2].text = "공격력 : " + playerData.Damage;
            playerTextView[3].text = "공격속도 : " + playerData.AttackSpeed;
            playerTextView[4].text = "치명타 확률 : " + playerData.CriticalChance + "%";
            playerTextView[5].text = "치명타 데미지 : " + playerData.CriticalDamage * 100 + "%";
        }

        public void ChangeSkin(SkeletonAnimation skeletonAnimation, int level)
        {
            if (level < 10)
            {
                skeletonAnimation.skeleton.SetSkin("p1");
                playerImage.sprite = Resources.Load<Sprite>("p1") as Sprite;
            }
            else if (level >= 10 && level < 25)
            {
                skeletonAnimation.skeleton.SetSkin("p2");
                playerImage.sprite = Resources.Load<Sprite>("p2") as Sprite;
            }
            else if (level >= 25 && level <= 50)
            {
                skeletonAnimation.skeleton.SetSkin("p3");
                playerImage.sprite = Resources.Load<Sprite>("p3") as Sprite;
            }
            else if (level > 50 && level <= 70)
            {
                skeletonAnimation.skeleton.SetSkin("p4");
                playerImage.sprite = Resources.Load<Sprite>("p4") as Sprite;
            }
            else if (level >= 70 && level < 99)
            {
                skeletonAnimation.skeleton.SetSkin("p5");
                playerImage.sprite = Resources.Load<Sprite>("p5") as Sprite;
            }
            else if (level >= 99)
            {
                skeletonAnimation.skeleton.SetSkin("p6");
                playerImage.sprite = Resources.Load<Sprite>("p6") as Sprite;
            }
            
            

            skeletonAnimation.skeleton.SetSlotsToSetupPose();
        }
    }
}

