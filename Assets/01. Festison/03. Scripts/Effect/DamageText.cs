using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    private float moveSpeed=0.5f;
    private float destroyTime=0.5f;
    public TextMeshProUGUI damageText;
    public float damage;

    private void OnEnable()
    {
        damageText.text = damage.ToString();      
        Invoke("DisActive", destroyTime);     
    }

    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
    }

    public void DisActive()
    {
        gameObject.SetActive(false);
    }
}
