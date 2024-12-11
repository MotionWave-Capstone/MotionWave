using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public SelectCarMenu selectCarPopup;
    public Button StartBtn;
    public Button QuitBtn;


    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetSceneByName("InGameScene").isLoaded)
        {
            SceneManager.UnloadSceneAsync("InGameScene");
        }
    }


    public void OnClickStart()
    {
        this.selectCarPopup.backBtn.onClick.AddListener(() =>
        {
            this.selectCarPopup.Close();
        });
        this.selectCarPopup.Open();
    }

    public void OnClickQuit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
    #else
        Application.Quit();
    #endif
    }


}
