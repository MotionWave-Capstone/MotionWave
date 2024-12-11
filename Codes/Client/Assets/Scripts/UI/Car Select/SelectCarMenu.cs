using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectCarMenu : MonoBehaviour
{
    public Transform contentContainer;

    public Button nextBtn;
    public Button backBtn;
    public TMP_Text errorTxt; 

    public SelectWorldMenu selectWorldPopup;
    public Transform content;

    ResponseData.CarMetaData[] carList;

    public void Open()
    {
        this.gameObject.SetActive(true);

        GameObject[] existBtns = GameObject.FindGameObjectsWithTag("CarSelectBtn");
        if ( existBtns.Length > 0 )
        {
            foreach (GameObject btn in existBtns )
            {
                Destroy(btn);
            }
        }

        StartCoroutine(LoadCarListAndCreateButtons());
    }

    private IEnumerator LoadCarListAndCreateButtons()
    {
        GameObject CarBtnPrefab = Resources.Load("Prefabs/ScrollDataBtn") as GameObject;

        yield return StartCoroutine(GetCarList());

        // carList가 로드될 때까지 대기
        yield return new WaitUntil(() => carList != null && carList.Length > 0);

        Debug.Log("Button Prefab Load Start");

        for (int i = 0; i < carList.Length; i++)
        {
            Debug.Log((i + 1) + "번째 버튼 생성");
            Transform CarBtnInst = Instantiate(CarBtnPrefab).transform;
            CarContentBtn CarBtn = CarBtnInst.GetComponent<CarContentBtn>();
            CarBtn.CarIndex = carList[i].inum;
            Debug.Log("CarIndex : " + CarBtn.CarIndex);
            CarBtn.CarName.SetText(carList[i].generation);
            CarBtn.Manufacturer.SetText(carList[i].brand);
            CarBtn.YearOfManufacture.SetText(carList[i].prdate);
            CarBtn.init(this);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(content.GetComponent<RectTransform>());

    }

    public void Close()
    {
        errorTxt.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void OnCarClicked(CarContentBtn carBtn)
    {
        SettingManager.Instance.carIndex = carBtn.CarIndex;
        SettingManager.Instance.carName = carBtn.CarName.text;
        Debug.Log((carBtn.CarIndex + 1).ToString() + "번째 데이터 선택");
    }

    public void OnClickNext()
    {
        if (SettingManager.Instance.carIndex == -1)
        {
            errorTxt.SetText("Please Select Car.");
            errorTxt.gameObject.SetActive(true);
            return;
        }
        Debug.Log("Car Select -> Next Clicked");
        this.selectWorldPopup.backBtn.onClick.AddListener(() =>
        {
            this.selectWorldPopup.Close();
        });
        errorTxt.gameObject.SetActive(false);
        this.selectWorldPopup.Open();
    }

    public IEnumerator GetCarList()
    {
        carList = null;
        Debug.Log("Load Car List Start");
        yield return StartCoroutine(HttpRequests.Instance.RequestGet(
            HttpRequests.Instance.GetServerUrl(HttpRequests.ServerEndpoints.CarList),
            (callback) =>
            {
                if (callback == null)
                {
                    Debug.Log("Select Car => RequestGet Error : No Data Found.");
                    errorTxt.SetText("Load Car List Failed.");
                    errorTxt.gameObject.SetActive(true);
                }
                else
                {
                    errorTxt.gameObject.SetActive(false);
                    try
                    {
                        carList = JsonUtility.FromJson<ResponseData.CarListResponseData>("{\"carDataList\":" + callback + "}").carDataList.ToArray();
                        foreach (var car in carList)
                        {
                            Debug.Log(car.inum);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.Log("Select Car => RequestGet Error : " + e.ToString());
                        errorTxt.SetText("Load Car List Failed : " + e.ToString());
                        errorTxt.gameObject.SetActive(true);
                    }
                }
            }
        ));
    }
}
