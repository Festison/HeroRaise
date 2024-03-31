using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using TMPro;
using Festioson;
using UnityEngine.UI;

// View
// ���� �� �������� ���̴� ��� ��Ҹ� �����Ѵ�.
// Controller���� ���� �����͸� ȭ�鿡 ����Ѵ�.
// Model�� ���� ������ ���� �����ؼ��� �ȵ�.
// Model�̳� Controller�� ���� ������ ������ �ȵ�.
// ������ �����ϸ� �ְ�!

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
        [Header("�÷��̾� ���� ��")]
        public TextMeshProUGUI[] playerTextView = new TextMeshProUGUI[6];
        public Image playerImage;

        public void UpdateUI(PlayerModel playerData)
        {
            playerTextView[0].text = "Lv. " + playerData.Level;
            playerTextView[1].text = playerData.Hp + " / " + playerData.MaxHp;
            playerTextView[2].text = "���ݷ� : " + playerData.Damage;
            playerTextView[3].text = "���ݼӵ� : " + playerData.AttackSpeed;
            playerTextView[4].text = "ġ��Ÿ Ȯ�� : " + playerData.CriticalChance + "%";
            playerTextView[5].text = "ġ��Ÿ ������ : " + playerData.CriticalDamage * 100 + "%";
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

