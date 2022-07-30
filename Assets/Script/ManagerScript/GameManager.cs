using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class User
{
    public string userName;
    public int UserLevel;
    public float MoneyUpLevel;
    public int MaxMoneyLevel;
    public int point;
    public int waveStack;
    public List<bool> UseBool;
}

public class GameManager : MonoSingletone<GameManager>
{
    public User userData;
    [SerializeField]
    WaveManager waveManager;
    [SerializeField]
    public IngameUserManager UserSet;
    [SerializeField]
    public List<PlayerCharData> PlayerCard;
    [SerializeField]
    public GameObject playerField;
    [SerializeField]
    UiManager _uiManager;
    public Transform EnemyTransform;

    public List<GameObject> PlayerList;


    public int nowWave;
    public List<GameObject> nowEnemy;

    public enum GameState {Idle ,WaveReady , WaveStart,WaveGaming ,WaveEnd, Die}

    public GameState useState = GameState.Idle;


    string path;
    string Json;
    public void Save()
    {
        Json  = JsonUtility.ToJson(userData);
        File.WriteAllText(path, Json);
    }

    public void SaveLoad()
    {
        if (File.Exists(path))
        {
            Json = File.ReadAllText(path);
            userData = JsonUtility.FromJson<User>(Json);
        }
    }

    private void Start()
    {
        Time.timeScale = 0;
        path = Application.dataPath + "/PlayerData.txt";
        SaveLoad();
        _uiManager.UISet(PlayerCard);
        WaveReady();
        nowWave = 0;
    }

    public void WaveReady()
    {
        useState = GameState.WaveReady;
        StartCoroutine(waveManager.WaveSet());
    }

    public void WaveStart()
    {
        UiManager.Insatnce.UpdateWave();
        useState = GameState.WaveStart;
        WaveGaming();
    }

    public void WaveGaming()
    {
        Save(); 
        StartCoroutine(UserSet.SetMoney());
        useState = GameState.WaveGaming;
        StartCoroutine(waveManager.element());
    }

    public void WaveEnd()
    {
        useState = GameState.WaveEnd;
        userData.waveStack++;
        waveManager.WaveDest();
        Save(); 

    }

    public void Die()
    {
        useState = GameState.Die;
    }
}



[System.Serializable]
public struct PlayerCharData
{
    public string charName;
    public int ID;
    public int nedMoney;
    public GameObject UsePrefab;
    public Sprite UseSprite;
    public int UsePoint;
}
