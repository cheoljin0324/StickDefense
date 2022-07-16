using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]


public class PoolManager : MonoBehaviour
{
    //-----------------------------
    [System.Serializable]           //  �ν����� â���� Ŭ������ �Ӽ��� ���� �ְ� ��.
    public class PoolingUnit
    {
        public string name;         //  ������Ʈ �̸�.
        public GameObject _prefObj; //  ������ �̸�
        public int _amount;         //  Ǯ�� �̸� �������� ����.
        int _curAmount;             //  ������Ʈ Ǯ�� ����� Ǯ ����.
        public int CurAmount
        {
            get { return _curAmount; }
            set { _curAmount = value; }
        }

    }//	public class _poolingUnits
    public static PoolManager _instance;              //  ���������� ������.
    //-----------------------------
    public PoolingUnit[] _poolingUnits;          //	������Ʈ Ǯ�� ������ ������.
    public List<GameObject>[] _pooledUnitsList;       //	���� ������ ������Ʈ Ǯ.
    //-----------------------------
    public int _defPoolAmount = 10;                    //	����Ʈ ����.
    //-----------------------------
    public bool _canPoolExpand = true;                  //	������Ʈ Ǯ Ȯ�� �÷���.
    //-----------------------------
    void Awake()
    {
        _instance = this;

        //  ���� ����� ������Ʈ Ǯ�� �޸𸮰����� ����� ������ ������ŭ �Ҵ���..
        //  2���� �迭�� ��..
        _pooledUnitsList = new List<GameObject>[_poolingUnits.Length];

        for (int i = 0; i < _poolingUnits.Length; i++)
        {
            _pooledUnitsList[i] = new List<GameObject>();

            //	���������� ������ ���������� ������ ����Ʈ ������ ����.
            if (_poolingUnits[i]._amount > 0)
                _poolingUnits[i].CurAmount = _poolingUnits[i]._amount;
            else
                _poolingUnits[i].CurAmount = _defPoolAmount;

            int idx = 0;
            for (int j = 0; j < _poolingUnits[i].CurAmount; j++)
            {
                //	������Ʈ�� ����.
                GameObject newItem = (GameObject)Instantiate(_poolingUnits[i]._prefObj);

                //	���̻� ����.
                string suffix = "_" + idx;

                //	_pooledUnitsList �� �߰�.
                AddToPooledUnitList(i, newItem, suffix);

                ++idx;

            }//	for(int j=0; j<poolingAmount; j++)

        }//	for (int j = 0; j < _poolingUnits[i].CurAmount; j++)

    }//	void Start ()
    //-----------------------------    
    //  ������Ʈ Ǯ�� ���� ���� ������Ʈ�� ��������..
    GameObject GetPooledItem(string pooledObjName)
    {
        for (int unitIdx = 0; unitIdx < _poolingUnits.Length; ++unitIdx)
        {
            //	������Ʈ Ǯ�� ��ϵ� ������Ʈ���� �ش� �̸��� �ִ��� Ž��.
            if (_poolingUnits[unitIdx]._prefObj.name == pooledObjName)
            {
                int listIdx = 0;
                for (listIdx = 0; listIdx < _pooledUnitsList[unitIdx].Count; ++listIdx)
                {
                    //	������Ʈ Ǯ�� ����� ���� ������Ʈ�� �ִ��� üũ.
                    if (_pooledUnitsList[unitIdx][listIdx] == null)
                        return null;

                    if (_pooledUnitsList[unitIdx][listIdx].activeInHierarchy == false)
                        return _pooledUnitsList[unitIdx][listIdx];

                }//	for( int listIdx = 0; listIdx < _pooledUnitsList[unitIdx].Count; ++listIdx )

                //	Ȯ�� �������� üũ.
                if (_canPoolExpand)
                {
                    //	�����ϸ� ������Ʈ Ǯ�� �߰��ϰ� ��ȯ.
                    GameObject tmpObj = (GameObject)Instantiate(_poolingUnits[unitIdx]._prefObj);

                    //  Ȯ��� ������Ʈ���� ����ϱ� ���� �̸��� ��������..
                    string suffix = "_" + listIdx.ToString() + "( " + (listIdx - _poolingUnits[unitIdx].CurAmount + 1).ToString() + " )";

                    //	_pooledUnitsList �� �߰�.
                    AddToPooledUnitList(unitIdx, tmpObj, suffix);

                    return tmpObj;

                }//	if(_canPoolExpand)

                break;

            }//	if(_poolingUnits[unitIdx]._prefObj.name == pooledObjName)

        }//	for(int i=0; i<_poolingUnits.Length; i++)

        return null;

    }//	public GameObject GetPooledItem (string pooledObjName)
     //-----------------------------
    void AddToPooledUnitList(int idx, GameObject newItem, string suffix)
    {
        //  Ȯ��� ������Ʈ���� ����ϱ� ���� �̸��� ��������..
        //	�̸� ����.
        newItem.name += suffix;

        //	��Ȱ��ȭ.
        newItem.SetActive(false);

        //	������Ʈ Ǯ �Ŵ��� ������Ʈ�� ���ϵ� ȭ.
        newItem.transform.parent = transform;

        //	����Ʈ�� �߰�.
        _pooledUnitsList[idx].Add(newItem);

    }//	void AddToPooledUnitList(int idx, GameObject newItem, string name)
     //-----------------------------
    public GameObject InstantiateAPS(int idx, GameObject parent = null)
    {
        string pooledObjName = _poolingUnits[idx].name;

        GameObject tmp = InstantiateAPS(pooledObjName, Vector3.zero,
                                        _poolingUnits[idx]._prefObj.transform.rotation,
                                        _poolingUnits[idx]._prefObj.transform.localScale,
                                        parent);

        return tmp;

    }// public GameObject InstantiateAPS(int idx, GameObject parent = null)
    //-----------------------------
    public GameObject InstantiateAPS(int idx, Vector3 pos, Quaternion rot, Vector3 scale, GameObject parent = null)
    {
        string pooledObjName = _poolingUnits[idx].name;

        GameObject tmp = InstantiateAPS(pooledObjName, pos, rot, scale, parent);

        return tmp;

    }// public GameObject InstantiateAPS(int idx, Vector3 pos, Quaternion rot, Vector3 scale, GameObject parent = null)
    //-----------------------------
    public GameObject InstantiateAPS(string pooledObjName, GameObject parent = null)
    {
        GameObject tmpObj = GetPooledItem(pooledObjName);

        tmpObj.SetActive(true);

        return tmpObj;

    }// public GameObject InstantiateAPS(string pooledObjName, GameObject parent = null)
    //-----------------------------
    public GameObject InstantiateAPS(string pooledObjName, Vector3 pos, Quaternion rot, Vector3 scale, GameObject parent = null)
    {
        GameObject tmpObj = GetPooledItem(pooledObjName);

        if (tmpObj != null)
        {
            if (parent != null)
                tmpObj.transform.parent = parent.transform;

            tmpObj.transform.position = pos;
            tmpObj.transform.rotation = rot;
            tmpObj.transform.localScale = scale;
            tmpObj.SetActive(true);

        }//	if(newObject != null)

        return tmpObj;

    }//	public GameObject InstantiateAPS (string itemType, Vector3 itemPosition, Quaternion itemRotation, GameObject myParent = null )
    //-----------------------------
    //  ���� Ȱ��ȭ�� ������Ʈ��..
    public List<GameObject> GetActivatePooledItem()
    {
        List<GameObject> tmps = new List<GameObject>();

        for (int unitIdx = 0; unitIdx < _poolingUnits.Length; ++unitIdx)
        {
            for (int listIdx = 0; listIdx < _pooledUnitsList[unitIdx].Count; ++listIdx)
            {
                if (_pooledUnitsList[unitIdx][listIdx].activeInHierarchy)
                    tmps.Add(_pooledUnitsList[unitIdx][listIdx]);

            }//	if(_poolingUnits[unitIdx]._prefObj.name == pooledObjName)

        }//	for(int i=0; i<_poolingUnits.Length; i++)

        return tmps;
    }
    //-----------------------------
    public static void DestroyAPS(GameObject obj) { obj.SetActive(false); }
    //-----------------------------


}
