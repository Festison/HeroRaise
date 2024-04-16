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
        if (collision.TryGetComponent<IHitable>(out IHitable monster))
        {
            Attack(monster);
            Debug.Log("스킬 충돌");
        }
    }

}
