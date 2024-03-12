using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    public LayerMask playerLayer;
    private int hp;
    public int Hp
    {
        get => hp;
        set => hp = value;
    }
    private int atk;
    public int Atk
    {
        get => atk;
        set => atk = value;
    }
    [SerializeField]
    private bool monsterScan;

    [SerializeField] RaycastHit2D hit;
    void Start()
    {
        Hp = 100;
        Atk = 10;
    }

    // Update is called once per frame
    void Update()
    {
        Scan();

    }

    public void Scan()
    {
        float maxDistance = 2.9f;
        Debug.DrawRay(transform.position + transform.up * 0.5f, transform.right * maxDistance, Color.red);
        hit = Physics2D.Raycast(transform.position + transform.up * 0.5f, transform.right, maxDistance, playerLayer);
    }
}
