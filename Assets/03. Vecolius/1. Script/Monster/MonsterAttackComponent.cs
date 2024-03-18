using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Veco {
    public class MonsterAttackComponent : MonoBehaviour, IAttackable
    {
        [SerializeField] MonsterStatus status = null;
        [SerializeField] LayerMask targetLayer;

        void Start()
        {
            status = transform.GetComponentInParent<MonsterAI>().MonsterStatus;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
                return;
            
            if (collision.TryGetComponent(out IHitable hitable))
                Attack(hitable);
        }

        public void Attack(IHitable hitable)
        {
            hitable.Hit(status.damage);
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
