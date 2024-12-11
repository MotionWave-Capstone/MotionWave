using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    // singleton
    private static SettingManager _instance;

    public int carIndex = -1;
    public int mapIndex = -1;
    public string carName = string.Empty;

    private void Awake()
    {
        // 중복 생성 방지 코드
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (_instance != this)
        {
            Debug.LogWarning("SettingManager 중복 생성. 기존 오브젝트 유지, 새로운 오브젝트 파괴.");
            Destroy(this.gameObject);
            return;
        }

        Debug.Log("SettingManager Instance ID: " + GetInstanceID());
    }

    public static SettingManager Instance
    {
        get
        {
            if (null == _instance)
            {
                return null;
            }
            return _instance;
        }
    }
}
