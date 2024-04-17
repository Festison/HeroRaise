using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Veco;

public abstract class Skill : MonoBehaviour, IAttackable
{
    public abstract void Attack(IHitable hitable);
}

public class FireBall : Skill, IMoveable
{

    [SerializeField] private Transform initTranform;
    private Transform transform;
    private Rigidbody2D Rigidbody2D;
    WaitForSeconds waitForSeconds = new WaitForSeconds(2f);

    private float speed = -0.06f;
    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    private void Start()
    {
        transform = GetComponent<Transform>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        transform.position = initTranform.position;
    }

    void Update()
    {
        StartCoroutine(DelayCo());       
    }

    IEnumerator DelayCo()
    {
        yield return waitForSeconds;
        Move();
        SkillAttackRayCast();
    }

    public void Move()
    {
        Rigidbody2D.AddForce(transform.forward * Speed, ForceMode2D.Impulse);
    }

    float lastAttackTime = 0f; // ������ ���� �ð��� ������ ����
    float attackCooldown = 0.1f; // ���� ��ٿ� �ð� (1��)
    public void SkillAttackRayCast()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            Debug.DrawRay(transform.position, transform.forward, Color.blue);

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.forward, 0.5f, LayerMask.GetMask("Monster"));

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
