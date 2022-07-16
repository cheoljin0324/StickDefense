using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    EnemyWaveDataBase waveData;//���̺� �����͸� ������ �ִ� Ŭ����
    [SerializeField]
    UsingPlayerChar playerData;

    public int elementAcount;//���� ��ȯ �ֱ�
    public int nowElement;
    public int nowWaveHp;

    public IEnumerator WaveSet()//���̺긦 �ʱ�ȭ �� ��ŸƮ �б����� �����ִ� �ڷ�ƾ
    {
        yield return new WaitForSeconds(2f);
        nowWaveHp = GameManager.Insatnce.nowWave+1;
        for(int i =0; i<GameManager.Insatnce.nowEnemy.Count; i++)
        {
            GameManager.Insatnce.nowEnemy.RemoveAt(i);
        }
        GameManager.Insatnce.WaveStart();
    }

    public IEnumerator element()//���� �ڷ�ƾ
    {
        if (nowWaveHp > 0)
        {
            elementAcount = 5 - GameManager.Insatnce.nowWave / 50;//�ֱ⸦ �ش� ���̺��� �ֱ�� �ʱ�ȭ

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
