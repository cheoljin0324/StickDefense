using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    GameObject shopPanel;
    [SerializeField]
    Text GoldText;
    bool isShop = false;

    public void SetShop()
    {
        GameManager.Insatnce.Save();
        if (isShop == false)
        {
            isShop = true;
            shopPanel.active = isShop;
        }
        else
        {
            isShop = false;
            shopPanel.active = isShop;
        }
    }

    public void Update()
    {
        GoldText.text = GameManager.Insatnce.userData.point.ToString();
    }

    public void Buy(int number)
    {
        GameManager.Insatnce.Save();
        if (GameManager.Insatnce.PlayerCard[number].UsePoint < GameManager.Insatnce.userData.point && GameManager.Insatnce.userData.UseBool[number]!=true)
        {
            GameManager.Insatnce.userData.point -= GameManager.Insatnce.PlayerCard[number].UsePoint;
            GameManager.Insatnce.userData.UseBool[number] = true;
            UiManager.Insatnce.UIClear();
        }  
    }
}
