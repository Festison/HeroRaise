using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    public float moveSpeed;
    public Vector3 moveVec;

    void Start()
    {
        moveVec = Vector3.zero;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            moveVec += Vector3.forward;
            Debug.Log("æ’¿∏∑Œ~");
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            moveVec += Vector3.zero;
            Debug.Log("∏ÿ√„");
        }
        transform.Translate(moveVec * moveSpeed);
    }
}
