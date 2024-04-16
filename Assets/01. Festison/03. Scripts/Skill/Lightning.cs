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
        float lastAttackTime = 0f; // ������ ���� �ð��� ������ ����
        float attackCooldown = 1f; // ���� ��ٿ� �ð� (1��)

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            if (collision.TryGetComponent<IHitable>(out IHitable monster))
            {
                Attack(monster);
                Debug.Log("��ų �浹");
            }
        }
    }

}
