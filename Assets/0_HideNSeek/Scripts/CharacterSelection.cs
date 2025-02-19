using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{

    public List<GameObject> petCharacters;
   

    [SerializeField]
    GameObject SelectBtn;

    [SerializeField]
    List<GameObject> buyBtnList;
    [SerializeField]
    GameObject watchVideoToUnlock;
    int index;
    
    string petsUnlockString;

    [SerializeField]
    List<int> petsPrice;

    [SerializeField]
    List<GameObject> selectBtnsList;

    [SerializeField]
    ParticleSystem petEnableParticle;

    [SerializeField]
    GameObject petGlowObj;

    public static int petIndex
    {
        get
        {
            return PlayerPrefs.GetInt("petindex", 0);
        }
        set
        {
            PlayerPrefs.SetInt("petindex", value);
        }
    }
    void OnEnable()
    {
        //SetHatsPlayerprefs();
        //SetSpectsPlayerprefs();
        //SetShirtsPlayerprefs();
        //SetPantsPlayerprefs();
        //SetShoesPlayerprefs();

        for (int i = 0; i < petCharacters.Count; i++)
        {
            petCharacters[i].SetActive(false);
        }
        petCharacters[petIndex].SetActive(true);
        petCharacters[petIndex].transform.position = new Vector3(0, 2.2f, 0);
        iTween.RotateFrom(petCharacters[petIndex].gameObject, iTween.Hash("y", 270, "time", 0.5, "islocal", true));
        EnablePetParticle();
        petGlowObj.SetActive(true);
        selectBtnsList[petIndex].transform.GetChild(0).gameObject.GetComponent<Text>().text = "Selected".ToUpper();
        petsUnlockString = GameEnums.PetsUnlocked;
        for (int i = 0; i < petCharacters.Count; i++)
        {
            //Debug.Log("i::" + i + "unlock string::" + petsUnlockString.Substring((i), 1));
            if (petsUnlockString.Substring((i), 1) == "1")
            {
                buyBtnList[i].SetActive(false);

                selectBtnsList[i].SetActive(true);
            }
        }

    }

    public void WatchVideoBtn(int petPrice)
    {

    }
   
    
    public void BuyBtn(int petNum)
    {
        char[] tempPetsChar = petsUnlockString.ToCharArray();
        index = petNum;
        if (petsPrice[petNum] <= GameEnums.TotalCoins)
        {
            tempPetsChar[petNum] = '1';
            GameEnums.PetsUnlocked = "";
            foreach (char ch in tempPetsChar)
            {
                GameEnums.PetsUnlocked += ch.ToString();
            }
            GameEnums.TotalCoins -= petsPrice[petNum];//petsPrice[index]
            GameObject thisBtn;
            thisBtn = EventSystem.current.currentSelectedGameObject;
            thisBtn.SetActive(false);
            selectBtnsList[index].SetActive(true);
            PetBtnClicked(petNum + 1);
            //CheckPetsUnlock(index);
        }
    }

    public void SelectBtnClicked(int num)
    {
        petIndex = num;
        for (int i = 0; i < selectBtnsList.Count; i++)
        {
            selectBtnsList[i].transform.GetChild(0).gameObject.GetComponent<Text>().text = "Select".ToUpper();
        }
        selectBtnsList[num].transform.GetChild(0).gameObject.GetComponent<Text>().text = "Selected".ToUpper();

        for (int i = 0; i < petCharacters.Count; i++)
        {
            petCharacters[i].SetActive(false);
        }
        petCharacters[num].SetActive(true);

        // MenuScript.instance.EnablePages(0);
    }

    public void BackBtn()
    {
        for (int i = 0; i < petCharacters.Count; i++)
        {
            petCharacters[i].SetActive(false);
        }
        petGlowObj.SetActive(false);

        MenuScript.instance.EnablePages(0);
    }
    public void PetBtnClicked(int petNum)
    {
        index = petNum;
        for (int i = 0; i < petCharacters.Count; i++)
        {
            petCharacters[i].SetActive(false);
        }
        petCharacters[petNum - 1].SetActive(true);
        petCharacters[petNum - 1].transform.rotation = Quaternion.Euler(0, 180, 0);
        iTween.RotateFrom(petCharacters[petNum - 1], iTween.Hash("y", 270, "time", 0.5, "islocal", true));
        EnablePetParticle();
        SelectBtnClicked(petNum - 1);
    }

    void EnablePetParticle()
    {
        petEnableParticle.Play();
    }


    //public static int[] hatvalueArray;
    //public static int[] spectsvalueArray;
    //public static int[] shirtsvalueArray;
    //public static int[] pantsvalueArray;
    //public static int[] shoesvalueArray;
    //public static string hatPurchasedIndex = "purchasedHatIndex";
    //public static string spectsPurchasedIndex = "purchasedSpetcsIndex";
    //public static string shirtsPurchasedIndex = "purchasedShirtsIndex";
    //public static string pantsPurchasedIndex = "purchasedPantsIndex";
    //public static string shoesPurchasedIndex = "purchasedShoesIndex";
    //public void SetHatsPlayerprefs()
    //{
    //    hatvalueArray = PlayerPrefsX.GetIntArray(hatPurchasedIndex, 0, 6);
    //    PlayerPrefsX.SetIntArray(hatPurchasedIndex, hatvalueArray);
    //    for (int i = 0; i < 6; i++)
    //    {
    //        Debug.LogError("hat values " + i + " " + hatvalueArray[i]);
    //    }
    //}
    //public void SetSpectsPlayerprefs()
    //{
    //    spectsvalueArray = PlayerPrefsX.GetIntArray(spectsPurchasedIndex, 0, 6);
    //    PlayerPrefsX.SetIntArray(spectsPurchasedIndex, spectsvalueArray);
    //    for (int i = 0; i < 6; i++)
    //    {
    //        Debug.LogError("spectsvalueArray " + i + " " + spectsvalueArray[i]);
    //    }
    //}

    //public void SetShirtsPlayerprefs()
    //{
    //    shirtsvalueArray = PlayerPrefsX.GetIntArray(shirtsPurchasedIndex, 0, 6);
    //    PlayerPrefsX.SetIntArray(shirtsPurchasedIndex, shirtsvalueArray);
    //    for (int i = 0; i < 6; i++)
    //    {
    //        Debug.LogError("shirtsvalueArray " + i + " " + shirtsvalueArray[i]);
    //    }
    //}

    //public void SetPantsPlayerprefs()
    //{
    //    pantsvalueArray = PlayerPrefsX.GetIntArray(pantsPurchasedIndex, 0, 6);
    //    PlayerPrefsX.SetIntArray(pantsPurchasedIndex, pantsvalueArray);
    //    for (int i = 0; i < 6; i++)
    //    {
    //        Debug.LogError("pantsvalueArray " + i + " " + pantsvalueArray[i]);
    //    }
    //}

    //public void SetShoesPlayerprefs()
    //{
    //    shoesvalueArray = PlayerPrefsX.GetIntArray(shoesPurchasedIndex, 0, 6);
    //    PlayerPrefsX.SetIntArray(shoesPurchasedIndex, shoesvalueArray);
    //    for (int i = 0; i < 6; i++)
    //    {
    //        Debug.LogError("shoesvalueArray " + i + " " + shoesvalueArray[i]);
    //    }
    //}
}
