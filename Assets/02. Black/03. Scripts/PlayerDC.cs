using BK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDC : MonoBehaviour
{
    public LayerMask enemyLayerMask;
    public float maxRadous = 0.5f;
    public Collider2D col = null;
    public void Scan()
    {
        col = Physics2D.OverlapCircle(transform.position, maxRadous, enemyLayerMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxRadous);
    }
}
