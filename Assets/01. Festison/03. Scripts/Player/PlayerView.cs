using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// View
// 게임 내 외적으로 보이는 모든 요소를 조작한다.
// Controller에서 받은 데이터를 화면에 출력한다.
// Model에 대한 정보를 따로 저장해서는 안됨.
// Model이나 Controller에 대한 정보를 가지면 안됨.
// 재사용이 가능하면 최고!

namespace Festioson
{
    public class PlayerView : MonoBehaviour
    {
        [Header("플레이어 스텟 뷰")]
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
            playerTextView[2].text = "공격력 : " + damage + "%";
        }
        public void UpdateAttackSpeed(float attackSpeed)
        {
            playerTextView[3].text = "공격속도 : " + attackSpeed;
        }
        public void UpdateCriticalChance(float criticalChance)
        {
            playerTextView[4].text = "치명타 확률 : " + criticalChance + "%";
        }
        public void CriticalDamage(float criticalDamage)
        {
            playerTextView[5].text = "치명타 데미지 : " + criticalDamage * 100;
        }
    }
}

