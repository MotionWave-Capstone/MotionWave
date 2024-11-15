using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectWorldMenu : MonoBehaviour
{
    public Button startBtn;
    public Button backBtn;
    public TMP_Text ErrorTxt;

    public List<Button> mapButtons; 


    // Start is called before the first frame update
    void Start()
    {
        AssignButtonListener();
    }

    void AssignButtonListener()
    {
        for (int i = 0;  i < mapButtons.Count; i++)
        {
            int index = i;
            mapButtons[i].onClick.RemoveAllListeners();
            mapButtons[i].onClick.AddListener(() => OnMapBtnClicked(index));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        this.gameObject.SetActive(true);
    }

    public void Close() 
    {
        this.gameObject.SetActive(false);
    }

    void OnMapBtnClicked(int index)
    {
        SettingManager.Instance.MapIndex = index;
        Debug.Log((index + 1) + "¹øÂ° ¸Ê ¼±ÅÃµÊ");
    }

    public void OnClickNext()
    {
        if (SettingManager.Instance.MapIndex == -1)
        {
            ErrorTxt.SetText("Please Select Map.");
            ErrorTxt.gameObject.SetActive(true);
            return;
        }
        Debug.Log("Map Select -> Next Clicked");
        
        ErrorTxt.gameObject.SetActive(false);
        Debug.Log("Selected Car : " + SettingManager.Instance.CarIndex + " Selected Map : " + SettingManager.Instance.MapIndex);
    }
}
