using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    EnemyWaveDataBase waveData;//웨이브 데이터를 가지고 있는 클래스
    [SerializeField]
    UsingPlayerChar playerData;

    public int elementAcount;//몬스터 소환 주기
    public int nowElement;
    public int nowWaveHp;

    public IEnumerator WaveSet()//웨이브를 초기화 및 스타트 분기점을 나눠주는 코루틴
    {
        yield return new WaitForSeconds(2f);
        nowWaveHp = GameManager.Insatnce.nowWave+1;
        for(int i =0; i<GameManager.Insatnce.nowEnemy.Count; i++)
        {
            GameManager.Insatnce.nowEnemy.RemoveAt(i);
        }
        GameManager.Insatnce.WaveStart();
    }

    public IEnumerator element()//생성 코루틴
    {
        if (nowWaveHp > 0)
        {
            elementAcount = 5 - GameManager.Insatnce.nowWave / 50;//주기를 해당 웨이브의 주기로 초기화

            while (GameManager.Insatnce.useState == GameManager.GameState.WaveGaming)
            {
                nowElement = 2;
                yield return new WaitForSeconds(elementAcount);
                GameObject SetEnemy = PoolManager._instance.InstantiateAPS(GameManager.Insatnce.PlayerCard.Count - nowElement);
                GameManager.Insatnce.nowEnemy.Add(SetEnemy);
                if (GameManager.Insatnce.nowEnemy.Count != 0)
                {
                    SetEnemy.transform.position = new Vector3(GameManager.Insatnce.EnemyTransform.position.x, -1.16f, 0f);
                }

            }
        }
       
    }

    public void WaveDest()
    {
        for(int i = 0; i<GameManager.Insatnce.nowEnemy.Count; i++)
        {
            PoolManager.DestroyAPS(GameManager.Insatnce.nowEnemy[i]);
            GameManager.Insatnce.nowEnemy.RemoveAt(i);
        }
        GameManager.Insatnce.WaveReady();
    }

}
