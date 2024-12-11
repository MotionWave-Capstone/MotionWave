using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseData
{
    [System.Serializable]
    public class CarMetaData
    {
        public int inum;
        public string generation;
        public string brand;
        public string prdate;
    }

    [System.Serializable]
    public class CarListResponseData
    {
        public List<CarMetaData> carDataList;
    }

    [System.Serializable]
    public class CarPhysicsData
    {
        public int dnum;
        public int inum;
        public int power; 
        public int power_rpm;
        public int torque; 
        public int torque_rpm;
        public int weight;
        public float urban;
        public float extra_urban;
        public float combined;
    }

    [System.Serializable]
    public class CarPhysicsResponseData
    {
        public List<CarPhysicsData> carPhysicsData;
    }
}
