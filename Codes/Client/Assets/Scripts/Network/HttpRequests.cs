using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class HttpRequests : MonoBehaviour
{
    private static HttpRequests instance = null;
    public string serverURL;

    public enum ServerEndpoints
    {
        CarList,
        CarData
    }

    public static HttpRequests Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

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

    private void Start()
    {
        serverURL = ConfigManager.Instance.GetConfigData("serverURL");
        Debug.Log("serverIP : " + serverURL);
    }

    public string GetServerUrl(ServerEndpoints endpointCode)
    {
        switch (endpointCode)
        {
            case ServerEndpoints.CarList:
                return serverURL + "/cart_list";
            case ServerEndpoints.CarData:
                return serverURL + "/car_data";
        }
        return null;
    }

    public IEnumerator RequestGet(string url, Action<string> callback)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        string result;

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            result = "{\"code\" : \"0\", " +
                    "\"message\" : \"" + request.error + "\"}";
        }
        else
        {
            result = request.downloadHandler.text;
        }
        // Debug.Log("Request Text : " + result);
        callback(result);
    }

    public IEnumerator RequestGet(string url, Dictionary<string, string> queryPair, Action<string> callback)
    {
        string urlWithQuery = url;

        if (queryPair != null && queryPair.Count > 0)
        {
            urlWithQuery += "?" + string.Join("&", queryPair.ToArray().Select(pair => pair.Key + "=" + pair.Value));
        }
        UnityWebRequest request = UnityWebRequest.Get(urlWithQuery);

        string result;

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            result = "{\"code\" : \"0\", " +
                    "\"message\" : \"" + request.error + "\"}";
        }
        else
        {
            result = request.downloadHandler.text;
        }
        // Debug.Log("Request Text : " + result +"\n" + urlWithQuery);
        callback(result);
    }
}
