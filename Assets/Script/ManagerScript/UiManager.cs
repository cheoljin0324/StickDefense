using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    GameObject Content;
    [SerializeField]
    Transform playerFiled;

    public GameObject ButtonObject;
    public List<GameObject> usingUI;
    InstAction inAct;

    private void Start()
    {
        inAct = GetComponent<InstAction>();
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

    void InstButton(Sprite setSprite, GameObject useObject, int cardID)
    {
        GameObject bttn = Instantiate(ButtonObject, Content.transform);
        usingUI.Add(bttn);
        bttn.GetComponent<Image>().sprite = setSprite;
        bttn.GetComponent<Button>().onClick.AddListener(()=>inAct.InstCard(useObject,playerFiled,cardID));
    }
}
