using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectCarMenu : MonoBehaviour
{
    public Transform ContentContainer;

    public Button nextBtn;
    public Button backBtn;
    public TMP_Text ErrorTxt; 

    public SelectWorldMenu selectWorldPopup;

    public void Open()
    {
        this.gameObject.SetActive(true);
        GameObject CarBtnPrefab = Resources.Load("Prefabs/ScrollDataBtn") as GameObject;

        Debug.Log("Button Prefab Load Start");

        for (int i = 0; i < 3; i++)
        {
            Debug.Log((i + 1) + "번째 버튼 생성");
            Transform CarBtnInst = Instantiate(CarBtnPrefab).transform;
            CarContentBtn CarBtn = CarBtnInst.GetComponent<CarContentBtn>();
            CarBtn.init(i, this);
        }
    }

    public void Close()
    {
        ErrorTxt.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void OnCarClicked(CarContentBtn CarBtn)
    {
        SettingManager.Instance.CarIndex = CarBtn.CarIndex;
        Debug.Log((CarBtn.CarIndex + 1).ToString() + "번째 데이터 선택");
    }

    public void OnClickNext()
    {
        if (SettingManager.Instance.CarIndex == -1)
        {
            ErrorTxt.SetText("Please Select Car.");
            ErrorTxt.gameObject.SetActive(true);
            return;
        }
        Debug.Log("Car Select -> Next Clicked");
        this.selectWorldPopup.backBtn.onClick.AddListener(() =>
        {
            this.selectWorldPopup.Close();
        });
        ErrorTxt.gameObject.SetActive(false);
        this.selectWorldPopup.Open();
    }
}
