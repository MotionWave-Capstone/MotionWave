using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarContentBtn : MonoBehaviour
{
    public int CarIndex;
    public TextMeshProUGUI CarName;
    public TextMeshProUGUI Manufacturer;
    public TextMeshProUGUI YearOfManufacture;

    SelectCarMenu selectCarMenu;

    public void init(int index, SelectCarMenu selectCarManager)
    {
        this.selectCarMenu = selectCarManager;
        CarIndex = index;
        transform.SetParent(selectCarManager.ContentContainer);
    }

    public void OnClicked()
    {
        selectCarMenu.OnCarClicked(this);
    }

}
