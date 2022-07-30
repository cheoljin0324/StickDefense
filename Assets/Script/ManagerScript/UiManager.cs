using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoSingletone<UiManager>
{
    [SerializeField]
    GameObject Content;
    [SerializeField]
    Transform playerFiled;
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private Text waveText;

    public GameObject ButtonObject;
    public List<GameObject> usingUI;
    public Text playerEnText;
    InstAction inAct;

    private void Start()
    {
        UpdateWave();
        inAct = GetComponent<InstAction>();
    }

    public void EnergyTextUpdate(int energy)
    {
        playerEnText.text = energy.ToString();
    }

    public void UpdateWave()
    {
        GameManager.Insatnce.Save();
        waveText.text = "WAVE"+GameManager.Insatnce.userData.waveStack.ToString();
    }

    public void UISet(List<PlayerCharData> setSprite)
    {
        for(int i = 0; i<setSprite.Count-1; i++)
        {
            if (GameManager.Insatnce.userData.UseBool[i + 1] == true)
            {
                InstButton(setSprite[i + 1].UseSprite, setSprite[i + 1].UsePrefab, setSprite[i + 1].ID);
            }
        }
    }

    public void UIClear()
    {
        Button[] setButton = content.GetComponentsInChildren<Button>();
        for(int i =0;i <setButton.Length; i++)
        {
            Destroy(setButton[i].gameObject);
        }
        UISet(GameManager.Insatnce.PlayerCard);
    }

    void InstButton(Sprite setSprite, GameObject useObject, int cardID)
    {
        GameObject bttn = Instantiate(ButtonObject, Content.transform);
        usingUI.Add(bttn);
        bttn.GetComponent<Image>().sprite = setSprite;
        bttn.GetComponent<Button>().onClick.AddListener(()=>inAct.InstCard(useObject,playerFiled,cardID));
    }
}
