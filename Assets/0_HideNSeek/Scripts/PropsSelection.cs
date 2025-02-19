using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PropsSelection : MonoBehaviour
{

    public List<GameObject> PropsObjects;
    public List<GameObject> PropsBtn;
    string hatsBoughtString, spectsBoughtString, shirtBoughtString, pantsBoughtString, shoesBoughtString;
    string hatsRevealString, spectsRevealString, shirtRevealString, pantsRevealString, shoesRevealString;
    public List<int> hatCostList, spectsCostList, shirtCostList, pantsCostList, shoesCostList;
    public List<Button> hatsBuyBtns, spectsBuyBtns, shirtBuyBtns, pantsBuyBtns, shoesBuyBtns;
    public List<Image> hatsImg, spectsImg, shirtsImg, pantsImg, shoesImg;
    void OnEnable()
    {
        PropsObjects[0].SetActive(true);
        for (int i = 0; i < PropsBtn.Count; i++)
        {
            PropsBtn[i].GetComponent<Image>().color = Color.grey;
        }
        PropsBtn[0].GetComponent<Image>().color = Color.white;

        hatsBoughtString = GameEnums.HatsUnlocked;
        spectsBoughtString = GameEnums.SpectsUnlocked;
        shirtBoughtString = GameEnums.ShirtsUnlocked;
        pantsBoughtString = GameEnums.PantsUnlocked;
        shoesBoughtString = GameEnums.ShoesUnlocked;

        hatsRevealString = HatsRevealed;
        spectsRevealString = SpectsRevealed;
        shirtRevealString = ShirtsRevealed;
        pantsRevealString = PantsRevealed;
        shoesRevealString = ShoesRevealed;

        
        for (int i = 0; i < hatsBuyBtns.Count; i++)
        {
            CheckHatsUnlock(i);


            //Reveal Hats here
            if (hatsRevealString.Substring((i), 1) == "1")
            {
                hatsBuyBtns[i].gameObject.SetActive(true);
                hatsImg[i].color = Color.white;
            }
            else
            {
                hatsBuyBtns[i].gameObject.SetActive(false);
                hatsImg[i].color = Color.black;
            }
        }
        for (int i = 0; i < spectsBuyBtns.Count; i++)
        {
            CheckSpectsUnlock(i);

            //Reveal Spects here
            if (spectsRevealString.Substring((i), 1) == "1")
            {
                spectsBuyBtns[i].gameObject.SetActive(true);
                spectsImg[i].color = Color.white;
            }
            else
            {
                spectsBuyBtns[i].gameObject.SetActive(false);
                spectsImg[i].color = Color.black;
            }
        }
        for (int i = 0; i < shirtBuyBtns.Count; i++)
        {
            CheckShirtsUnlock(i);

            //Reveal shirts here
            if (shirtRevealString.Substring((i), 1) == "1")
            {
                shirtBuyBtns[i].gameObject.SetActive(true);
                shirtsImg[i].color = Color.white;
            }
            else
            {
                shirtBuyBtns[i].gameObject.SetActive(false);
                shirtsImg[i].color = Color.black;
            }
        }
        for (int i = 0; i < pantsBuyBtns.Count; i++)
        {
            CheckPantsUnlock(i);

            //Reveal pants here
            if (pantsRevealString.Substring((i), 1) == "1")
            {
                pantsBuyBtns[i].gameObject.SetActive(true);
                pantsImg[i].color = Color.white;
            }
            else
            {
                pantsBuyBtns[i].gameObject.SetActive(false);
                pantsImg[i].color = Color.black;
            }
        }
        for (int i = 0; i < shoesBuyBtns.Count; i++)
        {
            CheckShoesUnlock(i);

            //Reveal shoes here
            if (shoesRevealString.Substring((i), 1) == "1")
            {
                shoesBuyBtns[i].gameObject.SetActive(true);
                shoesImg[i].color = Color.white;
            }
            else
            {
                shoesBuyBtns[i].gameObject.SetActive(false);
                shoesImg[i].color = Color.black;
            }
        }
    }

    void EnableProps(int index)
    {
        for (int i = 0; i < PropsObjects.Count; i++)
        {
            PropsObjects[i].SetActive(false);
        }
        PropsObjects[index].SetActive(true);

        
        for (int i = 0; i < PropsBtn.Count; i++)
        {
            PropsBtn[i].transform.GetChild(1).gameObject.SetActive(false);
        }
        PropsBtn[index].transform.GetChild(1).gameObject.SetActive(true);
    }

    public void PropsBtnClicked(int propNum)
    {
        EnableProps(propNum);
    }

    public void BackBtnClicked()
    {
        MenuScript.instance.EnablePages(0);
        for (int i = 0; i < PropsObjects.Count; i++)
        {
            PropsObjects[i].SetActive(false);
        }
    }

    #region Props Buy events
    public void HatsBuyBtnClicked(int index)
    {
        char[] tempHatChar = hatsBoughtString.ToCharArray();
        if (hatCostList[index] <= GameEnums.TotalCoins)
        {
            tempHatChar[index] = '1';
            GameEnums.HatsUnlocked = "";
            foreach (char ch in tempHatChar)
            {
                GameEnums.HatsUnlocked += ch.ToString();
            }
            GameEnums.TotalCoins -= hatCostList[index];
            CheckHatsUnlock(index);
        }
    }

    public void SpectsBuyBtnClicked(int index)
    {
        char[] tempSpectsChar = spectsBoughtString.ToCharArray();
        if (spectsCostList[index] <= GameEnums.TotalCoins)
        {
            tempSpectsChar[index] = '1';
            GameEnums.SpectsUnlocked = "";
            foreach (char ch in tempSpectsChar)
            {
                GameEnums.SpectsUnlocked += ch.ToString();
            }
            GameEnums.TotalCoins -= spectsCostList[index];
            CheckSpectsUnlock(index);
        }
    }

   

    public void ShirtsBuyBtnClicked(int index)
    {
        char[] tempShirtsChar = shirtBoughtString.ToCharArray();
        if (shirtCostList[index] <= GameEnums.TotalCoins)
        {
            tempShirtsChar[index] = '1';
            GameEnums.ShirtsUnlocked = "";
            foreach (char ch in tempShirtsChar)
            {
                GameEnums.ShirtsUnlocked += ch.ToString();
            }
            GameEnums.TotalCoins -= shirtCostList[index];
            CheckShirtsUnlock(index);
        }
    }

    public void PantsBuyBtnClicked(int index)
    {
        char[] tempPantsChar = pantsBoughtString.ToCharArray();
        if (pantsCostList[index] <= GameEnums.TotalCoins)
        {
            tempPantsChar[index] = '1';
            GameEnums.PantsUnlocked = "";
            foreach (char ch in tempPantsChar)
            {
                GameEnums.PantsUnlocked += ch.ToString();
            }
            GameEnums.TotalCoins -= pantsCostList[index];
            CheckPantsUnlock(index);
        }
    }

    public void ShoesBuyBtnClicked(int index)
    {
        char[] tempShoesChar = shoesBoughtString.ToCharArray();
        if (shoesCostList[index] <= GameEnums.TotalCoins)
        {
            tempShoesChar[index] = '1';
            GameEnums.ShoesUnlocked = "";
            foreach (char ch in tempShoesChar)
            {
                GameEnums.ShoesUnlocked += ch.ToString();
            }
            GameEnums.TotalCoins -= shoesCostList[index];
            CheckShoesUnlock(index);
        }
    }
    #endregion

    #region Props Check locked or not
    void CheckHatsUnlock(int index)
    {
        hatsBoughtString = GameEnums.HatsUnlocked;
        //Debug.Log("hatboughtstring::" + hatsBoughtString);
        

        if (hatsBoughtString.Substring((index), 1) == "1")
        {
            //hatsBuyBtns[index].transform.Find("BuyBtn").GetComponentInChildren<Text>().text = "Select";
            hatsBuyBtns[index].transform.GetComponentInChildren<Text>().text = "AVAILABLE";
        }
    }

    void CheckSpectsUnlock(int index)
    {
        spectsBoughtString = GameEnums.SpectsUnlocked;
       // Debug.Log("spectsBoughtString::" + spectsBoughtString);

        if (spectsBoughtString.Substring((index), 1) == "1")
        {
            spectsBuyBtns[index].transform.GetComponentInChildren<Text>().text = "AVAILABLE";
        }
    }

   

    void CheckShirtsUnlock(int index)
    {
        shirtBoughtString = GameEnums.ShirtsUnlocked;
        //Debug.Log("shirtBoughtString::" + shirtBoughtString);

        if (shirtBoughtString.Substring((index), 1) == "1")
        {
            shirtBuyBtns[index].transform.GetComponentInChildren<Text>().text = "AVAILABLE";
        }
    }

    void CheckPantsUnlock(int index)
    {
        pantsBoughtString = GameEnums.PantsUnlocked;
        //Debug.Log("pantsBoughtString::" + pantsBoughtString);

        if (pantsBoughtString.Substring((index), 1) == "1")
        {
            pantsBuyBtns[index].transform.GetComponentInChildren<Text>().text = "AVAILABLE";
        }
    }

    void CheckShoesUnlock(int index)
    {
        shoesBoughtString = GameEnums.ShoesUnlocked;
        //Debug.Log("shoesBoughtString::" + shoesBoughtString);

        if (shoesBoughtString.Substring((index), 1) == "1")
        {
            shoesBuyBtns[index].transform.GetComponentInChildren<Text>().text = "AVAILABLE";
        }
    }
    #endregion


    #region watch video to unlock props click events

    public void WatchVideoToUnlockHatsSuccessEvent(int index)
    {
        HatsBuyBtnClicked(index);
        EventSystem.current.currentSelectedGameObject.SetActive(false);
        EventSystem.current.currentSelectedGameObject.transform.parent.transform.Find("SelectBtn").gameObject.SetActive(true);
    }

    public void WatchVideoToUnlockSpectsSuccessEvent(int index)
    {
        SpectsBuyBtnClicked(index);
        EventSystem.current.currentSelectedGameObject.SetActive(false);
        EventSystem.current.currentSelectedGameObject.transform.parent.transform.Find("SelectBtn").gameObject.SetActive(true);

    }

    public void WatchVideoToUnlockShirtsSuccessEvent(int index)
    {
        ShirtsBuyBtnClicked(index);
        EventSystem.current.currentSelectedGameObject.SetActive(false);
        EventSystem.current.currentSelectedGameObject.transform.parent.transform.Find("SelectBtn").gameObject.SetActive(true);

    }
    public void WatchVideoToUnlockPantsSuccessEvent(int index)
    {
        PantsBuyBtnClicked(index);
        EventSystem.current.currentSelectedGameObject.SetActive(false);
        EventSystem.current.currentSelectedGameObject.transform.parent.transform.Find("SelectBtn").gameObject.SetActive(true);

    }
    public void WatchVideoToUnlockShoesSuccessEvent(int index)
    {
        ShoesBuyBtnClicked(index);
        EventSystem.current.currentSelectedGameObject.SetActive(false);
        EventSystem.current.currentSelectedGameObject.transform.parent.transform.Find("SelectBtn").gameObject.SetActive(true);

    }
    #endregion


    #region props reveal playerprefs

    public static string HatsRevealed
    {
        get
        {
            //return PlayerPrefs.GetString("hatsrevealed", "0000000000");
            return PlayerPrefs.GetString("hatsrevealed", "1111111111");
        }
        set
        {
            PlayerPrefs.SetString("hatsrevealed", value);
        }
    }
    public static string SpectsRevealed
    {
        get
        {
           // return PlayerPrefs.GetString("spectsrevealed", "0000000000");
           return PlayerPrefs.GetString("spectsrevealed", "1111111111");
        }
        set
        {
            PlayerPrefs.SetString("spectsrevealed", value);
        }
    }
    public static string ShirtsRevealed
    {
        get
        {
            return PlayerPrefs.GetString("shirtsrevealed", "0000000000");
            //return PlayerPrefs.GetString("shirtsrevealed", "1111111111");
        }
        set
        {
            PlayerPrefs.SetString("shirtsrevealed", value);
        }
    }
    public static string PantsRevealed
    {
        get
        {
            return PlayerPrefs.GetString("pantsrevealed", "0000000000");
           // return PlayerPrefs.GetString("pantsrevealed", "1111111111");
        }
        set
        {
            PlayerPrefs.SetString("pantsrevealed", value);
        }
    }
    public static string ShoesRevealed
    {
        get
        {
            return PlayerPrefs.GetString("shoesrevealed", "0000000000");
           // return PlayerPrefs.GetString("shoesrevealed", "1111111111");
        }
        set
        {
            PlayerPrefs.SetString("shoesrevealed", value);
        }
    }
    #endregion
}
