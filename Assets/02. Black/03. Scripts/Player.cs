using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
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
    public bool MonsterScan
    {
        get => monsterScan;
        set => monsterScan = value;
    }
    public LayerMask enemyLayerMask;
    float maxRadous = 0.5f;
    Collider2D col;
    void Start()
    {
        Hp = 100;
        Atk = 10;
    }

    void Update()
    {
        Scan();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            col = null;      
        }
        //The monster has a boolean value, so it's false for start, true for die
        //If true, change col to null
    }

    public void Scan()
    {
        if(!col)
        {
            col = Physics2D.OverlapCircle(transform.position, maxRadous, enemyLayerMask);
        }
        else
        {
            //if(col.transform.TryGetComponent(out ))
            //Get the monster hit and dispose of it
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxRadous);
    }



}
