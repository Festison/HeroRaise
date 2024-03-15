using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Veco
{
    public class TestPlayer : MonoBehaviour, IHitable
    {
        public int hp;
        void Start()
        {
            hp = 100;
        }

        public void Hit(int damage)
        {
            hp -= damage;
            Debug.Log("Player Hp : " + hp);
        }
    }
}
