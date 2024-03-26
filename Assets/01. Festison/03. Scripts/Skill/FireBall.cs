using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public Transform initTranform;
    public Transform transform;
    public Rigidbody2D Rigidbody2D;

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
        StartCoroutine(MoveCo(2));
    }

    IEnumerator MoveCo(float delay)
    {   
        yield return new WaitForSeconds(delay);
        Move();
    }

    private void Move()
    {
        Rigidbody2D.AddForce(transform.forward * -0.02f, ForceMode2D.Impulse);
    }
}
