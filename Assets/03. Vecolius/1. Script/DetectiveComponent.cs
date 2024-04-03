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
            Debug.DrawRay(transform.position, transform.right * (-1 * detectiveRange), Color.green);
            if (isFind)
                return;

            //Collider2D cols = Physics2D.OverlapCircle(transform.position, detectiveRange, targetLayer);
            RaycastHit2D[] cols = Physics2D.RaycastAll(transform.position, Vector3.left, detectiveRange, TargetLayer);
            if (cols.Length > 0)
            {
                Debug.Log(cols[0].transform.name);
                isFind = true;
            }
        }
    }
}
