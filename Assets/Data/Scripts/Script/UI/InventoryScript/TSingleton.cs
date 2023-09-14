using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T inst = null;

    public static T Instance
    {
        get
        {
            if(inst == null)
            {
                inst = FindObjectOfType<T>();
                if(inst == null)
                {
                    GameObject obj = new();
                    obj.name = typeof(T).ToString();
                    inst = obj.AddComponent<T>();
                }
            }
            return inst;
        }
    }

    protected void Init()
    {
        if(inst != null && inst != this)
        {
            Destroy(inst.gameObject);
            inst = this as T;
        }
        DontDestroyOnLoad(gameObject);
    }
}
