using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Veco;

namespace BK
{
    public class Enemy : MonoBehaviour, IHitable
    {
        [SerializeField]
        private int hp = 100;
        public bool die = false;
        public int HP 
        {
            get { return hp; } 
            set
            { 
                hp = value; 
                if(hp <0)
                {
                    die = true;
                    gameObject.SetActive(false);
                }    
            }
        }  

        public void Hit(int damage)
        {
            HP -= damage;
        }
    }

}

