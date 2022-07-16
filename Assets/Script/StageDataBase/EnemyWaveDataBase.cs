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
   public int waveHP;//현재 Wave를 깨기 위해 넘겨야 하는 몬스터 수
   public int elementcount;//현재 Wave에 몬스터 소환 주기
}

[System.Serializable]
public struct EnemyDataBase//몬스터 데이터 베이스
{
    public GameObject EnemyObject;
    public string name;
    public int ID;

}
