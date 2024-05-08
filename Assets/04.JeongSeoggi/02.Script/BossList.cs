using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Veco;

public class BossList : MonoBehaviour
{
    public List<Monster_Boss> bossList;
    void Start()
    {
        bossList[UIManager.Instance.index].gameObject.SetActive(true);
    }
}
