using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameUserManager : MonoBehaviour
{
    public int userMoney;
    public float WaitTime ;
    public float nowTime;

    private void Start()
    {
        WaitTime = 5.0f - GameManager.Insatnce.userData.MoneyUpLevel / 100;
    }

    private void Update()
    {
        
    }

    public IEnumerator SetMoney()
    {
        while (true)
        {
            yield return new WaitForSeconds(WaitTime);
            userMoney++;
            if(GameManager.Insatnce.useState != GameManager.GameState.WaveGaming)
            {
                StopCoroutine(SetMoney());
            }

        }
    } 
    }
       

