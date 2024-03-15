using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BK;
using Veco;

public class Player : MonoBehaviour
{
    private int hp;
    public int Hp
    {
        get => hp;
        set => hp = value;
    }
    private int damage;
    public int Atk
    {
        get => damage;
        set => damage = value;
    }
    private float range;
    private int lv;
    public int Lv
    {
        get => lv;  
        set 
        {
            lv = value;
            Hp += 10 * value;
        }
    }
    private float exp;
    public float Exp
    {
        get => exp;
        set
        {
            exp = value;
            if (exp > 1)
                Lv++;
        }
    }
    private float atkSpeed;

    [SerializeField]
    private bool monsterScan;
    public bool MonsterScan
    {
        get => monsterScan;
        set => monsterScan = value;
    }
    [SerializeField]
    private PlayerStatSo playerStatSo;
    public LayerMask enemyLayerMask;
    float maxRadous = 0.5f;
    Collider2D col;


    void Start()
    {
        Init();
    }

    void Init()
    {
        Hp = playerStatSo.hp;
        damage = playerStatSo.damage;
        Lv = playerStatSo.lv;
        range = playerStatSo.range;
        atkSpeed = playerStatSo.atkSpeed;
    }


    void Update()
    {
        Scan();
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
            if(col.transform.TryGetComponent(out Enemy enemy))
            {
                enemy.Hit(Atk);
                if (enemy.die)
                {
                    col = null;
                    exp += 0.50f;
                }
            }
            //Get the monster hit and dispose of it
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxRadous);
    }



}
