using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Veco
{
    public class DetectiveComponent : MonoBehaviour
    {
        [SerializeField] LayerMask targetLayer;
        [SerializeField] float detectiveRange;
        [SerializeField] bool isFind;
        public bool IsFind => isFind;
        public float DetectiveRange
        {
            get => detectiveRange;
            set
            {
                detectiveRange = value;
            }
        }
        public LayerMask TargetLayer => targetLayer;
        void OnEnable()
        {
            isFind = false;
        }
        void Start()
        {
            isFind = false;
        }

        void Update()
        {
            if (isFind)
                return;

            //Collider2D[] cols = Physics2D.OverlapSphere(transform.position, detectiveRange, targetLayer);
            Collider2D cols = Physics2D.OverlapCircle(transform.position, detectiveRange, targetLayer);
            if (cols != null)
            {
                isFind = true;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, detectiveRange);
        }
    }
}
