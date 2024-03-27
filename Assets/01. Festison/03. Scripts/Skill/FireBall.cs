using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour, IMoveable
{

    [SerializeField] private Transform initTranform;
    private Transform transform;
    private Rigidbody2D Rigidbody2D;

    private float speed = -0.04f;
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
        StartCoroutine(DelayCo(2));
    }

    IEnumerator DelayCo(float delay)
    {
        yield return new WaitForSeconds(delay);
        Move();
    }

    public void Move()
    {
        Rigidbody2D.AddForce(transform.forward * Speed, ForceMode2D.Impulse);
    }

}
