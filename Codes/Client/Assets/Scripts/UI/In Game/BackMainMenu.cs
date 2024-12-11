using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackMainMenu : MonoBehaviour
{

    public GameObject backToMainPannel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ESC Ű �Է� ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMainMenuPanel();
        }
    }

    public void ToggleMainMenuPanel()
    {
        if (backToMainPannel != null)
        {
            bool isActive = backToMainPannel.activeSelf;
            backToMainPannel.SetActive(!isActive); // ���� ���� ����

            Time.timeScale = isActive ? 1 : 0;
        }
    }

    public void OnClickToMainMenu()
    {
        SceneManager.LoadScene("SettingScene");
    }



}
