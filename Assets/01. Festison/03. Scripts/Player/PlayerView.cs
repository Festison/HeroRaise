using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Festioson;

// View
// ���� �� �������� ���̴� ��� ��Ҹ� �����Ѵ�.
// Controller���� ���� �����͸� ȭ�鿡 ����Ѵ�.
// Model�� ���� ������ ���� �����ؼ��� �ȵ�.
// Model�̳� Controller�� ���� ������ ������ �ȵ�.
// ������ �����ϸ� �ְ�!

public interface IView
{
    public void UpdateUI(PlayerModel playerData);
}

namespace Festioson
{
    public class PlayerView : MonoBehaviour, IView
    {
        [Header("�÷��̾� ���� ��")]
        public TextMeshProUGUI[] playerTextView = new TextMeshProUGUI[6];

        public void UpdateUI(PlayerModel playerData)
        {
            playerTextView[0].text = "Lv. " + playerData.Level;
            playerTextView[1].text = playerData.Hp + " / " + playerData.MaxHp;
            playerTextView[2].text = "���ݷ� : " + playerData.Damage;
            playerTextView[3].text = "���ݼӵ� : " + playerData.AttackSpeed;
            playerTextView[4].text = "ġ��Ÿ Ȯ�� : " + playerData.CriticalChance + "%";
            playerTextView[5].text = "ġ��Ÿ ������ : " + playerData.CriticalDamage * 100 + "%";
        }
    }
}

