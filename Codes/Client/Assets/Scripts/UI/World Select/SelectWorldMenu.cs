using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectWorldMenu : MonoBehaviour
{
    public Button startBtn;
    public Button backBtn;


    // Start is called before the first frame update
    void Start()
    {
        
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
}
