using System.Collections;
using UnityEngine;
using Veco;

public class Hydro : Skill
{
    WaitForSeconds waitForSeconds = new WaitForSeconds(2f);

    void Update()
    {
        StartCoroutine(DelayCo());
    }

    IEnumerator DelayCo()
    {
        yield return waitForSeconds;
        SkillAttackRayCast();
    }

    float lastAttackTime = 0f; // ������ ���� �ð��� ������ ����
    float attackCooldown = 2f; // ���� ��ٿ� �ð� (1��)
    float rayDistance = 2f;
    public void SkillAttackRayCast()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            Debug.DrawRay(transform.position + new Vector3(-1.5f, 0.5f, 0), transform.right + new Vector3(2f, 0, 0), Color.red);

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + new Vector3(-1.5f, 0.5f, 0), transform.right + new Vector3(2f, 0, 0), rayDistance, LayerMask.GetMask("Monster"));

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.TryGetComponent(out IHitable monster))
                {
                    Attack(monster);
                    lastAttackTime = Time.time; // ���� �ð��� ������Ʈ
                }
            }
        }
    }

    public override void Attack(IHitable hitable)
    {
        hitable.Hit(10);
    }
}
