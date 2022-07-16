using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BackGroundChange : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer[] backGroundSprite;
    [SerializeField]
    GameObject back;
    int backIdx = 0;

    private void Start()
    {
        for(int i = 0; i<backGroundSprite.Length; i++)
        {
            backGroundSprite[i].gameObject.SetActive(false);
        }
        backGroundSprite[backIdx].gameObject.SetActive(false);

        StartCoroutine(BackChange());
    }

    IEnumerator BackChange()
    {
        while (true)
        {
            if(backIdx == 0)
            {
                backGroundSprite[backGroundSprite.Length-1].gameObject.SetActive(false);
            }
            else
            {
                backGroundSprite[backIdx - 1].gameObject.SetActive(false);
            }
            backGroundSprite[backIdx].gameObject.SetActive(true);
            yield return new WaitForSeconds(10f);
            backIdx++;
            if (backIdx > backGroundSprite.Length - 1)
            {
                backIdx = 0;
            }

        }

    }
}
