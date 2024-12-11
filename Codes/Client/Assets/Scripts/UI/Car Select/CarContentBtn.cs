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

    public void init(SelectCarMenu selectCarManager)
    {
        this.selectCarMenu = selectCarManager;
        transform.SetParent(selectCarManager.contentContainer);
    }

    public void OnClicked()
    {
        selectCarMenu.OnCarClicked(this);
    }

}
