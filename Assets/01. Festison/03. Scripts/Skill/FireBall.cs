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
    }

    public void Move()
    {
        Rigidbody2D.AddForce(transform.forward * Speed, ForceMode2D.Impulse);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<MonsterAI>(out MonsterAI monster))
        {
            Attack(monster);
            Debug.Log("스킬 충돌");
        }
    }

    public override void Attack(IHitable hitable)
    {
        hitable.Hit(DataManager.Instance.playerData.damage);
    }
}
