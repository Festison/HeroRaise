using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// View
// ���� �� �������� ���̴� ��� ��Ҹ� �����Ѵ�.
// Controller���� ���� �����͸� ȭ�鿡 ����Ѵ�.
// Model�� ���� ������ ���� �����ؼ��� �ȵ�.
// Model�̳� Controller�� ���� ������ ������ �ȵ�.
// ������ �����ϸ� �ְ�!

namespace Festioson
{
    public class PlayerView : MonoBehaviour
    {
        [Header("�÷��̾� ���� ��")]
        public TextMeshProUGUI[] playerTextView = new TextMeshProUGUI[6];

        public void UpdateLevel(int level)
        {
            playerTextView[0].text = "Lv. " + level;
        }
        public void UpdateHp(int hp, int maxHp)
        {
            playerTextView[1].text = hp + " / " + maxHp;
        }
        public void UpdateDamage(int damage)
        {
            playerTextView[2].text = "���ݷ� : " + damage + "%";
        }
        public void UpdateAttackSpeed(float attackSpeed)
        {
            playerTextView[3].text = "���ݼӵ� : " + attackSpeed;
        }
        public void UpdateCriticalChance(float criticalChance)
        {
            playerTextView[4].text = "ġ��Ÿ Ȯ�� : " + criticalChance + "%";
        }
        public void CriticalDamage(float criticalDamage)
        {
            playerTextView[5].text = "ġ��Ÿ ������ : " + criticalDamage * 100;
        }
    }
}

