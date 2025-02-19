using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorePage : MonoBehaviour
{
    public List<GameObject> storeGroupsList;
    public List<Button> storeBtns;
    void Start()
    {
        EnableGroup(0);
        for (int i = 0; i < storeBtns.Count; i++)
        {
            storeBtns[i].GetComponent<Image>().color = Color.grey;
        }
        storeBtns[0].GetComponent<Image>().color = Color.white;
    }
    void EnableGroup(int ID)
    {
        for (int i = 0; i < storeGroupsList.Count; i++)
        {
            storeGroupsList[i].SetActive(false);
        }
        storeGroupsList[ID].SetActive(true);
    }
    
    public void BackBtnClicked()
    {
        MenuScript.instance.EnablePages(0);
    }
    public void StoreButtonClick(int id)
    {
        EnableGroup(id);
        for (int i = 0; i < storeBtns.Count; i++)
        {
           // storeBtns[i].GetComponent<Image>().color = Color.grey;
            storeBtns[i].transform.GetChild(1).gameObject.SetActive(false);

        }
       // storeBtns[id].GetComponent<Image>().color = Color.white;
        storeBtns[id].transform.GetChild(1).gameObject.SetActive(true);


    }
    public void CoinsBuyBtnClicked(int value)
    {
        GameEnums.TotalCoins += value;
    }
}
