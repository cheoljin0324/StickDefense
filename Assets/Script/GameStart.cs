using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
