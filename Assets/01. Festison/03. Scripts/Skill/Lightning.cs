using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Veco;

public class Lightning : Skill
{
    public override void Attack(IHitable hitable)
    {
        hitable.Hit(DataManager.Instance.playerData.Damage);
    }

    public void OnTriggerEnter(Collider collision)
    {
        float lastAttackTime = 0f; // 마지막 공격 시간을 저장할 변수
        float attackCooldown = 1f; // 공격 쿨다운 시간 (1초)

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            if (collision.TryGetComponent<IHitable>(out IHitable monster))
            {
                Attack(monster);
                Debug.Log("스킬 충돌");
            }
        }
    }

}
