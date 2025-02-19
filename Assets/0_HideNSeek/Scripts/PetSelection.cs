using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetSelection : MonoBehaviour
{
    public List<GameObject> petsButtonsGroup;
    public List<GameObject> petsModelsGroup;
    string catBoughtString, dogBoughtString, rabbitBoughtString,
        pandaBoughtString, monkeyBoughtString;
    public GameObject SelectBtn, catBuyBtn, dogBuyBtn, rabbitBuyBtn, pandaBuyBtn, monkeyBuyBtn;
    public List<GameObject> catModels, dogsModels, rabbitModels, pandaModels, monkeyModels;
    public List<int> catsCostList, dogCostList, rabbitCostList, pandaCostList, monkeyCostList;
    public Text petNameText;
    int catID, dogID, rabbitID, pandaID, monkeyID;

    void OnEnable()
    {
        EnablePetsButtonGroup(0);
        EnablePetsModelsGroup(0);
        //catBoughtString = GameEnums.CatsUnlocked;
        //dogBoughtString = GameEnums.DogsUnlocked;
        //rabbitBoughtString = GameEnums.RabbitsUnlocked;
        //pandaBoughtString = GameEnums.PandasUnlocked;
        //monkeyBoughtString = GameEnums.MonkeysUnlocked;
        //CheckCatsUnlock(0);
        //CheckDogsUnlock(0);
        //CheckRabbitsUnlock(0);
        //CheckPandasUnlock(0);
        //CheckMonkeysUnlock(0);
        petNameText.text = "Cat_" + (catID + 1);

        //Debug.Log("Gameenums.CatsUnlocked::" + GameEnums.CatsUnlocked);
        //Debug.Log("Gameenums.DogsUnlocked::" + GameEnums.DogsUnlocked);
        //Debug.Log("Gameenums.RabbitsUnlocked::" + GameEnums.RabbitsUnlocked);
        //Debug.Log("Gameenums.PandasUnlocked::" + GameEnums.PandasUnlocked);
        //Debug.Log("Gameenums.MonkeysUnlocked::" + GameEnums.MonkeysUnlocked);

    }
    void DisableAllBuyBtns()
    {
        catBuyBtn.SetActive(false);
        dogBuyBtn.SetActive(false);
        rabbitBuyBtn.SetActive(false);
        pandaBuyBtn.SetActive(false);
        monkeyBuyBtn.SetActive(false);
    }

    #region Check Pets Unlock
    //void CheckCatsUnlock(int index)
    //{
    //    catBoughtString = GameEnums.CatsUnlocked;
    //    //Debug.Log("carboughtstring::" + catBoughtString);
    //    DisableAllBuyBtns();
    //    if (catBoughtString.Substring((index), 1) == "1")
    //    {
    //        SelectBtn.SetActive(true);
    //        catBuyBtn.SetActive(false);
    //    }
    //    else
    //    {
    //        SelectBtn.SetActive(false);
    //        catBuyBtn.SetActive(true);
    //    }
    //}
    //void CheckDogsUnlock(int index)
    //{
    //    dogBoughtString = GameEnums.DogsUnlocked;
    //   // Debug.Log("dogboughtstring::" + dogBoughtString);

    //    DisableAllBuyBtns();
    //    if (dogBoughtString.Substring((index), 1) == "1")
    //    {
    //        SelectBtn.SetActive(true);
    //        dogBuyBtn.SetActive(false);
    //    }
    //    else
    //    {
    //        SelectBtn.SetActive(false);
    //        dogBuyBtn.SetActive(true);
    //    }
    //}
    //void CheckRabbitsUnlock(int index)
    //{
    //    rabbitBoughtString = GameEnums.RabbitsUnlocked;
    //   // Debug.Log("rabbitBoughtString::" + rabbitBoughtString);

    //    DisableAllBuyBtns();
    //    if (rabbitBoughtString.Substring((index), 1) == "1")
    //    {
    //        SelectBtn.SetActive(true);
    //        rabbitBuyBtn.SetActive(false);
    //    }
    //    else
    //    {
    //        SelectBtn.SetActive(false);
    //        rabbitBuyBtn.SetActive(true);
    //    }
    //}
    //void CheckPandasUnlock(int index)
    //{
    //    pandaBoughtString = GameEnums.PandasUnlocked;
    //    //Debug.Log("pandaBoughtString::" + pandaBoughtString + " index::" + index);

    //    DisableAllBuyBtns();
    //    if (pandaBoughtString.Substring((index), 1) == "1")
    //    {
    //       // Debug.Log("Enable select btn");
    //        SelectBtn.SetActive(true);
    //        pandaBuyBtn.SetActive(false);
    //    }
    //    else
    //    {
    //        SelectBtn.SetActive(false);
    //        pandaBuyBtn.SetActive(true);
    //    }
    //}
    //void CheckMonkeysUnlock(int index)
    //{
    //    monkeyBoughtString = GameEnums.MonkeysUnlocked;
    //   // Debug.Log("monkeyBoughtString::" + monkeyBoughtString);

    //    DisableAllBuyBtns();
    //    if (monkeyBoughtString.Substring((index), 1) == "1")
    //    {
    //        SelectBtn.SetActive(true);
    //        monkeyBuyBtn.SetActive(false);
    //    }
    //    else
    //    {
    //        SelectBtn.SetActive(false);
    //        monkeyBuyBtn.SetActive(true);
    //    }
    //}
    #endregion

    public void PetsButtonClicked(int ID)
    {
        EnablePetsButtonGroup(ID);
        EnablePetsModelsGroup(ID);
    }
    void EnablePetsButtonGroup(int ID)
    {
        for (int i = 0; i < petsButtonsGroup.Count; i++)
        {
            petsButtonsGroup[i].SetActive(false);
        }
        petsButtonsGroup[ID].SetActive(true);
    }
    void EnablePetsModelsGroup(int ID)
    {
        for (int i = 0; i < petsModelsGroup.Count; i++)
        {
            petsModelsGroup[i].SetActive(false);
        }
        petsModelsGroup[ID].SetActive(true);
    }
    public void BackBtnClicked()
    {
        MenuScript.instance.EnablePages(0);
        for (int i = 0; i < petsModelsGroup.Count; i++)
        {
            petsModelsGroup[i].SetActive(false);
        }
    }
    //public void CatsBuyBtnClicked(int index)
    //{
    //    char[] tempCatChar = catBoughtString.ToCharArray();
    //    index = catID;
    //    if (catsCostList[index] <= GameEnums.TotalCoins)
    //    {
    //        tempCatChar[index] = '1';
    //        GameEnums.CatsUnlocked = "";
    //        foreach (char ch in tempCatChar)
    //        {
    //            GameEnums.CatsUnlocked += ch.ToString();
    //        }
    //        GameEnums.TotalCoins -= catsCostList[index];
    //        CheckCatsUnlock(index);
    //    }
    //}
    //public void DogsBuyBtnClicked(int index)
    //{
    //    char[] tempDogChar = dogBoughtString.ToCharArray();
    //    index = dogID;
    //    if (dogCostList[index] <= GameEnums.TotalCoins)
    //    {
    //        tempDogChar[index] = '1';
    //        GameEnums.DogsUnlocked = "";
    //        foreach (char ch in tempDogChar)
    //        {
    //            GameEnums.DogsUnlocked += ch.ToString();
    //        }
    //        GameEnums.TotalCoins -= dogCostList[index];
    //        CheckDogsUnlock(index);
    //    }
    //}
    //public void RabbitsBuyBtnClicked(int index)
    //{
    //    char[] tempRabbitChar = rabbitBoughtString.ToCharArray();
    //    index = rabbitID;
    //    if (rabbitCostList[index] <= GameEnums.TotalCoins)
    //    {
    //        tempRabbitChar[index] = '1';
    //        GameEnums.RabbitsUnlocked = "";
    //        foreach (char ch in tempRabbitChar)
    //        {
    //            GameEnums.RabbitsUnlocked += ch.ToString();
    //        }
    //        GameEnums.TotalCoins -= rabbitCostList[index];
    //        CheckRabbitsUnlock(index);
    //    }
    //}
    //public void PandasBuyBtnClicked(int index)
    //{
    //    //Debug.Log("panda btn clicked");
    //    char[] tempPandaChar = pandaBoughtString.ToCharArray();
    //    index = pandaID;
    //    if (pandaCostList[index] <= GameEnums.TotalCoins)
    //    {
    //       // Debug.Log("coins are there");

    //        tempPandaChar[index] = '1';
    //        GameEnums.PandasUnlocked = "";
    //        foreach (char ch in tempPandaChar)
    //        {
    //            GameEnums.PandasUnlocked += ch.ToString();
    //        }
    //        GameEnums.TotalCoins -= pandaCostList[index];
    //        CheckPandasUnlock(index);
    //    }
    //}
    //public void MonkeysBuyBtnClicked(int index)
    //{
    //    char[] tempMonkeysChar = monkeyBoughtString.ToCharArray();
    //    index = monkeyID;
    //    if (monkeyCostList[index] <= GameEnums.TotalCoins)
    //    {
    //        tempMonkeysChar[index] = '1';
    //        GameEnums.MonkeysUnlocked = "";
    //        foreach (char ch in tempMonkeysChar)
    //        {
    //            GameEnums.MonkeysUnlocked += ch.ToString();
    //        }
    //        GameEnums.TotalCoins -= monkeyCostList[index];
    //        CheckMonkeysUnlock(index);
    //    }
    //}

    public void CatsBtnClicked(int CatNum)
    {
        catID = CatNum;
        petNameText.text = "Cat_" + (catID + 1);

        for (int i = 0; i < catModels.Count; i++)
        {
            catModels[i].SetActive(false);
        }
        catModels[CatNum].SetActive(true);
       // CheckCatsUnlock(CatNum);
        catBuyBtn.transform.GetChild(0).GetComponent<Text>().text = catsCostList[CatNum].ToString(); 
    }
    public void DogsBtnClicked(int DogNum)
    {
        dogID = DogNum;
        petNameText.text = "Dog_" + (dogID + 1);
        for (int i = 0; i < dogsModels.Count; i++)
        {
            dogsModels[i].SetActive(false);
        }
        dogsModels[DogNum].SetActive(true);
       // CheckDogsUnlock(DogNum);
        dogBuyBtn.transform.GetChild(0).GetComponent<Text>().text = dogCostList[DogNum].ToString();
    }
    public void RabbitsBtnClicked(int RabbitNum)
    {
        rabbitID = RabbitNum;
        petNameText.text = "Rabbit_" + (rabbitID + 1);
        for (int i = 0; i < rabbitModels.Count; i++)
        {
            rabbitModels[i].SetActive(false);
        }
        rabbitModels[RabbitNum].SetActive(true);
        //CheckRabbitsUnlock(RabbitNum);
        rabbitBuyBtn.transform.GetChild(0).GetComponent<Text>().text = rabbitCostList[RabbitNum].ToString();
    }
    public void PandasBtnClicked(int PandaNum)
    {
        pandaID = PandaNum;
        petNameText.text = "Panda_" + (pandaID + 1);
        for (int i = 0; i < pandaModels.Count; i++)
        {
            pandaModels[i].SetActive(false);
        }
        pandaModels[PandaNum].SetActive(true);
       // CheckPandasUnlock(PandaNum);
        pandaBuyBtn.transform.GetChild(0).GetComponent<Text>().text = pandaCostList[PandaNum].ToString();
    }
    public void MonkeysBtnClicked(int MonkeyNum)
    {
        monkeyID = MonkeyNum;
        petNameText.text = "Monkey_" + (monkeyID + 1);
        for (int i = 0; i < monkeyModels.Count; i++)
        {
            monkeyModels[i].SetActive(false);
        }
        monkeyModels[MonkeyNum].SetActive(true);
       // CheckMonkeysUnlock(MonkeyNum);
        monkeyBuyBtn.transform.GetChild(0).GetComponent<Text>().text = monkeyCostList[MonkeyNum].ToString();
    }

    
}

