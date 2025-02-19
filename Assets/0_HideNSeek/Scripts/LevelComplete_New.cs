using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelComplete_New : MonoBehaviour
{
    string petBoughtString;
    bool isProgressFilling;
    [SerializeField] Image progressBar, progressbarbg;
    [SerializeField] Text progressBarFillText;


    [SerializeField]
    GameObject unlockCharacterPopup;

    [SerializeField]
    GameObject chestBoxPage;

    [SerializeField]
    GameObject Hide_LC, Seek_LC, bonusLevel_LC;

    Text WatchVideoCoinsText;
    [SerializeField] Button extraTimeBtn;

    [Header("Hide Score Details")]
    [SerializeField] Text hide_rescuedPetsCountText;   
    [SerializeField] Text hide_RescuesPetsRewardText;
    [SerializeField] Text hide_coinsCollectedText;
    [SerializeField] Text hide_totalCoinsText;
    [SerializeField] Text hide_DoubleRewardText;

    [Header("Seek Score Details")]
    [SerializeField] Text seek_coinsCollectedText;
    [SerializeField] Sprite redBtn, greenBtn;
    

    [SerializeField] List<Text> petNamesList;

    [Header("BonusLevel Score Details")]
    [SerializeField] Text bonusLevel_totalCoinsText;
    [SerializeField] Text bonusLevel_DoubleRewardText;

    int totalCoins;


    [Header("Props Images")]
    [SerializeField] List<Sprite> hatImages;
    [SerializeField] List<Sprite> spectsImages;
    [SerializeField] List<Sprite> shirtsImages;
    [SerializeField] List<Sprite> pantsImages;
    [SerializeField] List<Sprite> shoesImages;

    [Space(20)]
    [SerializeField] List<Sprite> hatOriginalImages;
    [SerializeField] List<Sprite> spectsOriginalImages;
    [SerializeField] List<Sprite> shirtsOriginalImages;
    [SerializeField] List<Sprite> pantsOriginalImages;
    [SerializeField] List<Sprite> shoesOriginalImages;

    [SerializeField] Image unlockChar_PropsImg;

    public static int PetPropsCount
    {
        get
        {
            return PlayerPrefs.GetInt("petpropscount", 0);
        }
        set
        {
            PlayerPrefs.SetInt("petpropscount", value);
        }
    }

    public static int PetPropsSelected
    {
        get
        {
            return PlayerPrefs.GetInt("petpropsselected", 0);
        }
        set
        {
            PlayerPrefs.SetInt("petpropsselected", value);
        }
    }

    public static int PetHatIndex
    {
        get
        {
            return PlayerPrefs.GetInt("hatindex", 0);
        }
        set
        {
            PlayerPrefs.SetInt("hatindex", value);
        }
    }
    public static int PetSpectsIndex
    {
        get
        {
            return PlayerPrefs.GetInt("spectsindex", 0);
        }
        set
        {
            PlayerPrefs.SetInt("spectsindex", value);
        }
    }
    public static int PetShirtIndex
    {
        get
        {
            return PlayerPrefs.GetInt("shirtindex", 0);
        }
        set
        {
            PlayerPrefs.SetInt("shirtindex", value);
        }
    }
    public static int PetPantsIndex
    {
        get
        {
            return PlayerPrefs.GetInt("pantsindex", 0);
        }
        set
        {
            PlayerPrefs.SetInt("pantsindex", value);
        }
    }
    public static int PetShoesIndex
    {
        get
        {
            return PlayerPrefs.GetInt("shoesindex", 0);
        }
        set
        {
            PlayerPrefs.SetInt("shoesindex", value);
        }
    }
    public static float PetProgressValue
    {
        get
        {
            return PlayerPrefs.GetFloat("petprogressval", 0);
        }
        set
        {
            PlayerPrefs.SetFloat("petprogressval", value);
        }
    }

    void Start()
    {
        petBoughtString = GameEnums.PetsUnlocked;

        AssignPropsImageToProgressBar();
        
        SetScores();
    }

    void AssignPropsImageToProgressBar()
    {
        if (PetPropsCount == 0)
        {
            progressBar.sprite = hatImages[PetHatIndex];
            progressbarbg.sprite = hatImages[PetHatIndex];
        }
        else if (PetPropsCount == 1)
        {
            progressBar.sprite = spectsImages[PetSpectsIndex];
            progressbarbg.sprite = spectsImages[PetSpectsIndex];
        }
        else if (PetPropsCount == 2)
        {
            progressBar.sprite = shirtsImages[PetShirtIndex];
            progressbarbg.sprite = shirtsImages[PetShirtIndex];
        }
        else if (PetPropsCount == 3)
        {
            progressBar.sprite = pantsImages[PetPantsIndex];
            progressbarbg.sprite = pantsImages[PetPantsIndex];
        }
        else if (PetPropsCount == 4)
        {
            progressBar.sprite = shoesImages[PetShoesIndex];
            progressbarbg.sprite = shoesImages[PetShoesIndex];
        }
        progressBar.SetNativeSize();
        progressbarbg.SetNativeSize();
        SetProgressBarValues();
    }

    void SetProgressBarValues()
    {
        if (!GameController.isBonusLevel)
        {
            isProgressFilling = true;
        }

        if (PetProgressValue == 0)
        {
            PetProgressValue = 0.5f;
            progressBar.fillAmount = 0;
            progressBarFillText.text = (progressBar.fillAmount * 100).ToString("F0") + "%";
        }
        else if (PetProgressValue == 0.5f)
        {
            progressBar.fillAmount = 0.5f;
            PetProgressValue = 1f;
            progressBarFillText.text = (progressBar.fillAmount * 100).ToString("F0") + "%";
        }
    }


    void SetScores()
    {
        if (MenuScript.isHideGame)
        {
            Hide_LC.SetActive(true);
            Seek_LC.SetActive(false);

            hide_rescuedPetsCountText.text = "" + GameController.instance.savedLifeCount;
            hide_RescuesPetsRewardText.text = "" + (GameController.instance.savedLifeCount * 10);
            hide_coinsCollectedText.text = "" + GameController.instance.coinsCollectedInLevel;
            totalCoins= (GameController.instance.savedLifeCount * 10) + GameController.instance.coinsCollectedInLevel;
            hide_totalCoinsText.text = "" + totalCoins;
            hide_DoubleRewardText.text = "" + (totalCoins * 4);
        }
        else if (MenuScript.isSeekGame)
        {
            Hide_LC.SetActive(false);
            Seek_LC.SetActive(true);
            seek_coinsCollectedText.text = "" + GameController.instance.coinsCollectedInLevel;
            totalCoins = GameController.instance.coinsCollectedInLevel;

            for (int i = 0; i < GameController.instance.petnamesList.Count; i++)
            {
                petNamesList[i].text = "" + GameController.instance.petnamesList[i].ToUpper();

                if (GameController.instance.aiPlayersList[i].GetComponent<AIController>().isObjFound)
                {
                    //petNamesList[i].transform.parent.GetComponent<Button>().image.color = Color.green;
                    petNamesList[i].transform.parent.GetComponent<Button>().image.sprite = greenBtn;
                }
                else
                {
                    //petNamesList[i].transform.parent.GetComponent<Button>().image.color = Color.red;
                    petNamesList[i].transform.parent.GetComponent<Button>().image.sprite = redBtn;
                }
            }
            //Debug.Log("Level target::" + GameController.instance.LevelTarget);
            if (GameController.instance.LevelTarget < 6)
            {
                extraTimeBtn.gameObject.SetActive(true);
            }
            else
            {
                extraTimeBtn.gameObject.SetActive(false);
            }
        }
        else if (GameController.isBonusLevel)
        {
            Hide_LC.SetActive(false);
            Seek_LC.SetActive(false);
            bonusLevel_LC.SetActive(true);
            bonusLevel_totalCoinsText.text = "" + GameController.instance.coinsCollectedInLevel;
            bonusLevel_DoubleRewardText.text = "" + (GameController.instance.coinsCollectedInLevel * 4);
        }
    }


    void Update()
    {
        if (isProgressFilling && !GameController.isBonusLevel)
        {
            progressBar.fillAmount += 0.5f * Time.deltaTime;
            progressBarFillText.text = (progressBar.fillAmount * 100).ToString("F0") + "%";


            if (progressBar.fillAmount >= PetProgressValue)
            {
                progressBar.fillAmount = PetProgressValue;
                progressBarFillText.text = (PetProgressValue * 100).ToString("F0") + "%";

            }
        }
    }

    public void BonusLC_NextBtnClicked()
    {
        if (GameController.isBonusLevel)
            GameController.isBonusLevel = false;

        SetProgressBarValues();

        if (GameEnums.KeysCount >= 3)
        {
            chestBoxPage.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }
    public void NextBtnClicked()
    {
        //Debug.Log("petprogressValue::" + PetProgressValue);

        if (PetProgressValue == 1)
        {
            PetProgressValue = 0;

            unlockCharacterPopup.SetActive(true);

            if (PetPropsCount == 0)
            {
                unlockChar_PropsImg.sprite = hatOriginalImages[PetHatIndex];

                //Reveal the hat in props selection 
                char[] tempHatChar = PropsSelection.HatsRevealed.ToCharArray();
                tempHatChar[PetHatIndex] = '1';
                PropsSelection.HatsRevealed = "";
                foreach (char ch in tempHatChar)
                {
                    PropsSelection.HatsRevealed += ch.ToString();
                }
                Debug.Log("HatsRevealed::" + PropsSelection.HatsRevealed);

                PetHatIndex++;
                PetPropsCount = 1;

            }
            else if (PetPropsCount == 1)
            {
                unlockChar_PropsImg.sprite = spectsOriginalImages[PetSpectsIndex];

                //Reveal the spects in props selection 
                char[] tempSpectsChar = PropsSelection.SpectsRevealed.ToCharArray();
                tempSpectsChar[PetSpectsIndex] = '1';
                PropsSelection.SpectsRevealed = "";
                foreach (char ch in tempSpectsChar)
                {
                    PropsSelection.SpectsRevealed += ch.ToString();
                }
                Debug.Log("SpectsRevealed::" + PropsSelection.SpectsRevealed);

                PetSpectsIndex++;
                PetPropsCount = 2;
            }
            else if (PetPropsCount == 2)
            {
                unlockChar_PropsImg.sprite = shirtsOriginalImages[PetShirtIndex];

                //Reveal the shirts in props selection 
                char[] tempShirtChar = PropsSelection.ShirtsRevealed.ToCharArray();
                tempShirtChar[PetShirtIndex] = '1';
                PropsSelection.ShirtsRevealed = "";
                foreach (char ch in tempShirtChar)
                {
                    PropsSelection.ShirtsRevealed += ch.ToString();
                }
                Debug.Log("ShirtsRevealed::" + PropsSelection.ShirtsRevealed);

                PetShirtIndex++;
                PetPropsCount = 3;
            }
            else if (PetPropsCount == 3)
            {
                unlockChar_PropsImg.sprite = pantsOriginalImages[PetPantsIndex];

                //Reveal the pants in props selection 
                char[] tempPantsChar = PropsSelection.PantsRevealed.ToCharArray();
                tempPantsChar[PetPantsIndex] = '1';
                PropsSelection.PantsRevealed = "";
                foreach (char ch in tempPantsChar)
                {
                    PropsSelection.PantsRevealed += ch.ToString();
                }
                Debug.Log("PantsRevealed::" + PropsSelection.PantsRevealed);

                PetPantsIndex++;
                PetPropsCount = 4;
            }
            else if (PetPropsCount == 4)
            {
                unlockChar_PropsImg.sprite = shoesOriginalImages[PetShoesIndex];

                //Reveal the shoes in props selection 
                char[] tempShoesChar = PropsSelection.ShoesRevealed.ToCharArray();
                tempShoesChar[PetShoesIndex] = '1';
                PropsSelection.ShoesRevealed = "";
                foreach (char ch in tempShoesChar)
                {
                    PropsSelection.ShoesRevealed += ch.ToString();
                }
                Debug.Log("ShoesRevealed::" + PropsSelection.ShoesRevealed);

                PetShoesIndex++;
                PetPropsCount = 0;
            }
            unlockChar_PropsImg.SetNativeSize();


        }
        else
        {
            if (GameEnums.KeysCount == 3)
            {
                chestBoxPage.SetActive(true);
            }
            else
            {
                if (GameEnums.LevelsUnlocked % 3 == 0)
                {
                    GameController.isBonusLevel = true;
                    GameEnums.BonusLevelsUnlocked += 1;
                    GameEnums.LevelsUnlocked += 1;
                    SceneManager.LoadScene("InGame");
                }
                else
                {
                    GameEnums.LevelsUnlocked += 1;
                    SceneManager.LoadScene("Menu");
                }

            }
        }
    }
    public void NoThanksBtnClicked()
    {
        unlockCharacterPopup.SetActive(false);
        if (GameEnums.KeysCount == 3)
        {
            chestBoxPage.SetActive(true);
        }
        else
        {
            if (GameEnums.LevelsUnlocked % 3 == 0)
            {
                GameController.isBonusLevel = true;
                GameEnums.BonusLevelsUnlocked += 1;
                GameEnums.LevelsUnlocked += 1;
                SceneManager.LoadScene("InGame");
            }
            else
            {
                GameEnums.LevelsUnlocked += 1;
                SceneManager.LoadScene("Menu");
            }
        }
        //if (UnlockPetsinLC == 0) UnlockPetsinLC = 1;
        //else UnlockPetsinLC = 0;
    }

    IEnumerator WatchVideoText(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        WatchVideoCoinsText.text = "2x";
        yield return new WaitForSeconds(1);
        WatchVideoCoinsText.text = "3x";
        yield return new WaitForSeconds(1);
        WatchVideoCoinsText.text = "4x";
    }

    public void PetRescue_NextBtn()
    {
      //  GameEnums.PetRescueLevels += 1;
        SceneManager.LoadScene("Menu");
    }
}
