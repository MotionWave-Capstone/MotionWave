using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VehiclePhysics;

public enum MapType{
    City,
    Highway,
    Forest
}


public class GameManager : MonoBehaviour
{
    SettingManager settingManager;

    public GameObject cityMap;
    public GameObject highwayMap;
    public GameObject forestMap;
    public GameObject cameraController;
    public GameObject dashboard;
    public GameObject car;
    public TMP_Text errorTxt;
    public TMP_Text carNameTxt;

    VPVehicleController carController;
    Rigidbody carRigidbody;

    ResponseData.CarPhysicsData carPhysicsData;

    void Awake()
    {
        Application.targetFrameRate = 144;
    }

    // Start is called before the first frame update
    void Start()
    {
        // 맵 콘텐츠를 초기화 한 후, 선택한 맵에 맞게 active 함
        settingManager = SettingManager.Instance;

        DisableContents();

        // TODO : HTTP로 서버에서 차 데이터 받아온 다음에, 그 데이터를 car 오브젝트에 입혀야함
        StartCoroutine(LoadCarDataAndCreateCar());

        if (Time.timeScale < 1)
        {
            Time.timeScale = 1;
        }
        
    }

    void DisableContents()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("MapObject");

        foreach (GameObject obj in taggedObjects)
        {
            obj.SetActive(false);
        }
    }

    private IEnumerator LoadCarDataAndCreateCar()
    {
        yield return StartCoroutine(GetCarData());

        yield return new WaitUntil(() => carPhysicsData != null);

        Debug.Log("Car Data Insert Start");

        MapType mapType = (MapType)settingManager.mapIndex;
        switch (mapType)
        {
            case MapType.City: // 0
                cityMap.SetActive(true);
                break;

            case MapType.Highway: // 1
                highwayMap.SetActive(true);
                break;

            case MapType.Forest: // 2
                forestMap.SetActive(true);
                break;
        }


        car.SetActive(true);
        cameraController.SetActive(true);
        dashboard.SetActive(true);

        carController = car.GetComponent<VPVehicleController>();
        carRigidbody = car.GetComponent<Rigidbody>();

        carController.engine.maxRpm = (carPhysicsData.power_rpm);
        carController.engine.idleRpm = (int)(carPhysicsData.power_rpm * 0.27); // 추정치
        carController.engine.peakRpm = (int)(carPhysicsData.power_rpm * 0.8); // 추정치

        carController.engine.idleRpmTorque = (int)(carPhysicsData.torque * 0.15); // 추정치
        carController.engine.peakRpmTorque = (carPhysicsData.torque); 
        carController.engine.rpmLimiterMax = (int)(carPhysicsData.power_rpm * 0.95);

        // Manual Settings
        carController.gearbox.autoShiftFirstGearRpm = (int)(carPhysicsData.power_rpm * 0.33);
        carController.gearbox.autoShiftNeutralRpm = (int)(carPhysicsData.power_rpm * 0.13);
        carController.gearbox.autoShiftUpRevs = (int)(carPhysicsData.power_rpm * 0.7);
        carController.gearbox.autoShiftDownRevs = (int)(carPhysicsData.power_rpm * 0.38);

        // Automatic Settings
        /*
        carController.gearbox.automaticMaxRpm = (int)(carPhysicsData.power_rpm * 0.6);
        carController.gearbox.automaticGearUpRevs = (int)(carPhysicsData.power_rpm * 0.7);
        carController.gearbox.automaticGearDownRevs = (int)(carPhysicsData.power_rpm * 0.33);
        */




        switch (mapType)
        {
            case MapType.City: // 0
                carController.data.Set(Channel.Vehicle, VehicleData.FuelConsumption, (int)(carPhysicsData.urban * 1000));
                Debug.Log("Urban (InGame) : " + carController.data.Get(Channel.Vehicle, VehicleData.FuelConsumption) / 1000.0f);
                break;

            case MapType.Highway: // 1
                carController.data.Set(Channel.Vehicle, VehicleData.FuelConsumption, (int)(carPhysicsData.combined * 1000));
                Debug.Log("Combined (InGame) : " + carController.data.Get(Channel.Vehicle, VehicleData.FuelConsumption) / 1000.0f);
                break;

            case MapType.Forest: // 2
                carController.data.Set(Channel.Vehicle, VehicleData.FuelConsumption, (int)(carPhysicsData.extra_urban * 1000));
                Debug.Log("Extra_Urban (InGame) : " + carController.data.Get(Channel.Vehicle, VehicleData.FuelConsumption) / 1000.0f);
                break;
        }
        // TODO : power, torque는 그에 맞는 rpm 서버에서 기록해주면 그 때 추가해야될 듯
        // carController.data.Set(Channel.Vehicle, VehicleData.EnginePower, carPhysicsData.power * 10000);

        carRigidbody.mass = carPhysicsData.weight;

        carNameTxt.SetText(SettingManager.Instance.carName);
    }

    public IEnumerator GetCarData()
    {
        carPhysicsData = null;
        Debug.Log("Load Car Physics Data Start");
        Debug.Log("SettingManager Car Index : " + settingManager.carIndex);
        Dictionary<string, string> searchQuery = new Dictionary<string, string>
        {
            { "index", settingManager.carIndex.ToString() }
        };

        Debug.Log(searchQuery["index"]);


        yield return StartCoroutine(HttpRequests.Instance.RequestGet(
            HttpRequests.Instance.GetServerUrl(HttpRequests.ServerEndpoints.CarData), searchQuery,
            (callback) =>
            {
                if (callback == null)
                {
                    Debug.Log("Car Physics Data Not Found.");
                    errorTxt.SetText("Load Car Physics Data Failed.");
                    errorTxt.gameObject.SetActive(true);
                }
                else
                {
                    errorTxt.gameObject.SetActive(false);
                    try
                    {
                        ResponseData.CarPhysicsResponseData rawCarData = JsonUtility.FromJson<ResponseData.CarPhysicsResponseData>("{\"carPhysicsData\":" + callback + "}");
                        carPhysicsData = rawCarData.carPhysicsData[0];
                        Debug.Log("Car Physics Data Loaded");
                        Debug.Log("Power (HTTP) : " + carPhysicsData.power + " in " + carPhysicsData.power_rpm + "rpm");
                        Debug.Log("Torque (HTTP) : " + carPhysicsData.torque + " in " + carPhysicsData.torque_rpm + "rpm");
                        Debug.Log("Weight (HTTP) : " + carPhysicsData.weight);
                        Debug.Log("Urban (HTTP) : " + carPhysicsData.urban);
                        Debug.Log("Extra_urban (HTTP) : " + carPhysicsData.extra_urban);
                        Debug.Log("Combined (HTTP) : " + carPhysicsData.combined);
                    }
                    catch (Exception e)
                    {
                        Debug.Log("InGameScene Load Car Physics Data => RequestGet Error : " + e.ToString());
                        errorTxt.SetText("Load Car Physics Data Failed : " + e.ToString());
                        errorTxt.gameObject.SetActive(true);
                    }
                }
            }));
    }
}
