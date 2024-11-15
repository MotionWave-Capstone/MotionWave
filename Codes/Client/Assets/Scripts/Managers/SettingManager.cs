using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    // singleton
    private static SettingManager instance = null;

    private int carIndex = -1;
    private int mapIndex = -1;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static SettingManager Instance
    {
        get 
        { 
            if (null == instance)
            {
                return null;
            }
            return instance; 
        }
    }

    public int CarIndex
    {
        get
        {
            if (null != instance)
                return instance.carIndex;
            else
                return -1;
        }
        set { instance.carIndex = value; }
    }

    public int MapIndex
    {
        get
        {
            if (null != instance)
                return instance.mapIndex;
            else 
                return -1;
        }

        set { instance.mapIndex = value; }
    }
}
