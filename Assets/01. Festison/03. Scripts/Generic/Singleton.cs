using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTon<T> : MonoBehaviour where T : SingleTon<T>
{
    protected static T instance = null;

    public static T Instance { get => instance; }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = (T)this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}