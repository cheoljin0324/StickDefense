using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveDataBase : MonoBehaviour
{
    public GameObject[] waveEnemy;
}

[System.Serializable]
public struct StageEnemy
{
   public int WaveNumber;
   public int useEnemyId;
   public int waveHP;//���� Wave�� ���� ���� �Ѱܾ� �ϴ� ���� ��
   public int elementcount;//���� Wave�� ���� ��ȯ �ֱ�
}

[System.Serializable]
public struct EnemyDataBase//���� ������ ���̽�
{
    public GameObject EnemyObject;
    public string name;
    public int ID;

}
