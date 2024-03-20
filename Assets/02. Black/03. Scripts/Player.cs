using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BK;
using Veco;
using UnityEngine.Playables;
using System.Reflection;
using static UnityEditor.Progress;
using JetBrains.Annotations;

namespace BK
{
    public class Player : PlayerStatusMono
    {
        private int hp;
        public int Hp
        {
            get => hp;
            set
            {
                hp = value;
                if(hp < 0)
                {
                    PlayerStatusChange("DIe");
                }
            }
        }
        private int damage;
        public int Damage
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
                {
                    Lv++;
                    exp = 0;
                }

            }
        }
        private float atkSpeed;


        [SerializeField]
        private PlayerStatSo playerStatSo;
        PlayerStatusMono playerStatus;

        [SerializeField]
        private ItemSo[] itemSlot;

        public ItemSo item;
        void Start()
        {
            itemSlot = new ItemSo[4]; //Slot
            sm.AddState("Idle", new PlayerIdleState());
            sm.AddState("attack", new PlayerAttackState());
            sm.AddState("Skiil", new PlayerSkillState());
            sm.AddState("Die", new PlayerDieState());
            sm.SetState("Idle");
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
            sm.Update();
            //Skill has the highest priority
            if (playerDC.col != null)
            {
                PlayerStatusChange("Idle");
            }
            else
            {
                PlayerStatusChange("attack");
            }
        }

        private void PlayerStatusChange(string statusName)
        {
            sm.SetState(statusName);
        }

        private void EquipItem(int value , ItemSo item)
        {
            if (itemSlot[value]== null)
            {
                itemSlot[value] = item;
                SetItem(item);
            }
            else
            {
                
                itemSlot[value] = item;
                SetItem(item);
            }
        }

        
        private void SetItem(ItemSo item)
        {
            switch (item.itemType)
            {
                case ItemSo.ItemType.Armor:
                    Hp += (int)item.value;
                    break;
                case ItemSo.ItemType.Sword: 
                    break;
                case ItemSo.ItemType.Shoes:
                    break;
                case ItemSo.ItemType.Gloves: 
                    break;
            }
        }
    }
}
