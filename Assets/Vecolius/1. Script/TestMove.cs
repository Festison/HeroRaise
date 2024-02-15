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
            Debug.Log("������~");
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            moveVec += Vector3.zero;
            Debug.Log("����");
        }
        transform.Translate(moveVec * moveSpeed);
    }
}
