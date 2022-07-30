using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstAction : MonoBehaviour
{

    Vector3 instPos;
    public void InstCard(GameObject Card,Transform playerField, int a)
    {  
        if(GameManager.Insatnce.useState == GameManager.GameState.WaveGaming)
        {
            if (GameManager.Insatnce.UserSet.userMoney >= GameManager.Insatnce.PlayerCard[a].nedMoney)
            {
                GameManager.Insatnce.UserSet.userMoney -= GameManager.Insatnce.PlayerCard[a].nedMoney;
                UiManager.Insatnce.EnergyTextUpdate(GameManager.Insatnce.UserSet.userMoney);
                Debug.Log(GameManager.Insatnce.UserSet.userMoney);
                GameObject useCard = Card;
                instPos = new Vector3(playerField.position.x, -1.23f, 0f);
                PoolManager._instance.InstantiateAPS(a-1,useCard.transform.position,useCard.transform.rotation,useCard.transform.localScale);
                UiManager.Insatnce.UIClear();
            }
           
        }
       
    }
}
