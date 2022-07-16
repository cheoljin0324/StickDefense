using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDamaging : MonoBehaviour
{
    WaveManager waveManager;
    private void Awake()
    {
        waveManager = GetComponent<WaveManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerCard"))
        {
            collision.gameObject.SetActive(false);
            waveManager.nowWaveHp--;
            Debug.Log(waveManager.nowWaveHp);
            if(waveManager.nowWaveHp <= 0)
            {
                GameManager.Insatnce.WaveEnd();
            }
        }
    }
}
