using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{

    bool isProgressFilling;

    public static float ProgressValue
    {
        get
        {
            return PlayerPrefs.GetFloat("progressval", 0);
        }
        set
        {
            PlayerPrefs.SetFloat("progressval", value);
        }
    }
    public static int PetsCount
    {
        get
        {
            return PlayerPrefs.GetInt("petcount", 0);
        }
        set
        {
            PlayerPrefs.SetInt("petcount", value);
        }
    }
    public static int PropsCount
    {
        get
        {
            return PlayerPrefs.GetInt("propscount", 0);
        }
        set
        {
            PlayerPrefs.SetInt("propscount", value);
        }
    }

    public static int UnlockPetsinLC
    {
        get
        {
            return PlayerPrefs.GetInt("unlockpetsinlc", 0);
        }
        set
        {
            PlayerPrefs.SetInt("unlockpetsinlc", value);
        }
    }


    [SerializeField] Image progressBar, progressbarbg;
    [SerializeField] Sprite catSpr, dogSpr, rabbitSpr, pandaSpr, monkeySpr;
    [SerializeField] Sprite hatSpr, SpectsSpr, chainSpr, shirtSpr, pantsSpr, shoesSpr;

    public GameObject unlockCharacterPopup;

    string catBoughtString, dogBoughtString, rabbitBoughtString,
        pandaBoughtString, monkeyBoughtString;

    string hatsBoughtString, spectsBoughtString, chainBoughtString, shirtBoughtString, pantsBoughtString, shoesBoughtString;
    [SerializeField] List<int> catLockIndex, dogLockIndex, rabbitLockIndex, pandaLockIndex, monkeyLockIndex;
    [SerializeField] List<int> hatLockIndex, spectsLockIndex, chainLockIndex, shirtLockIndex, pantsLockIndex,shoesLockIndex;
    void Start()
    {

        if (!GameController.isBonusLevel)
            isProgressFilling = true;



        if (ProgressValue == 0)
        {
            ProgressValue = 0.5f;
            progressBar.fillAmount = 0;
        }
        else if (ProgressValue == 0.5f)
        {
            progressBar.fillAmount = 0.5f;
            ProgressValue = 1f;
        }

        GetPetsInfo();
        GetPropsInfo();
        AssignToProgressBar();
    }

    void Update()
    {
        if (GameController.isBonusLevel)
            return;

        if (isProgressFilling)
        {
            progressBar.fillAmount += 0.5f * Time.deltaTime;
            if (progressBar.fillAmount >= ProgressValue)
            {
                progressBar.fillAmount = ProgressValue;
            }
        }
    }
    void AssignToProgressBar()
    {
        if (UnlockPetsinLC == 0)
        {
            AssignPetsSprite(PetsCount);
        }
        else
        {
            AssignPropsSprite(PropsCount);
        }
    }

    public void GetPetsInfo()
    {
        //catBoughtString = GameEnums.CatsUnlocked;
        //dogBoughtString = GameEnums.DogsUnlocked;
        //rabbitBoughtString = GameEnums.RabbitsUnlocked;
        //pandaBoughtString = GameEnums.PandasUnlocked;
        //monkeyBoughtString = GameEnums.MonkeysUnlocked;

        //Debug.Log("Gameenums.CatsUnlocked::" + GameEnums.CatsUnlocked);
        //Debug.Log("Gameenums.DogsUnlocked::" + GameEnums.DogsUnlocked);
        //Debug.Log("Gameenums.RabbitsUnlocked::" + GameEnums.RabbitsUnlocked);
        //Debug.Log("Gameenums.PandasUnlocked::" + GameEnums.PandasUnlocked);
        //Debug.Log("Gameenums.MonkeysUnlocked::" + GameEnums.MonkeysUnlocked);

        #region Adding locked pets in a list
        //add locked cats in a list
        char[] tempCatChar = catBoughtString.ToCharArray();
        for (int i = 0; i < 10; i++)
        {
            if(tempCatChar[i] == '0')
                catLockIndex.Add(i);
        }

        //add locked dogs in a list
        char[] tempDogChar = dogBoughtString.ToCharArray();
        for (int i = 0; i < 10; i++)
        {
            if (tempDogChar[i] == '0')
                dogLockIndex.Add(i);
        }

        //add locked rabbit in a list
        char[] tempRabbitChar = rabbitBoughtString.ToCharArray();
        for (int i = 0; i < 10; i++)
        {
            if (tempRabbitChar[i] == '0')
                rabbitLockIndex.Add(i);
        }

        //add locked panda in a list
        char[] tempPandaChar = pandaBoughtString.ToCharArray();
        for (int i = 0; i < 10; i++)
        {
            if (tempPandaChar[i] == '0')
                pandaLockIndex.Add(i);
        }

        //add locked moenkey in a list
        char[] tempMonkeyChar = monkeyBoughtString.ToCharArray();
        for (int i = 0; i < 10; i++)
        {
            if (tempMonkeyChar[i] == '0')
                monkeyLockIndex.Add(i);
        }
        #endregion

    }
    void AssignPetsSprite(int num)
    {
        if (PetsCount == 1)//cats 
        {
            progressbarbg.sprite = catSpr;
            progressBar.sprite = catSpr;
        }
        else if (PetsCount == 2)//dogs
        {
            progressbarbg.sprite = dogSpr;
            progressBar.sprite = dogSpr;
        }
        else if (PetsCount == 3)//rabbit
        {
            progressbarbg.sprite = rabbitSpr;
            progressBar.sprite = rabbitSpr;
        }
        else if (PetsCount == 4)//panda
        {
            progressbarbg.sprite = pandaSpr;
            progressBar.sprite = pandaSpr;
        }
        else if (PetsCount == 5)//monkey
        {
            progressbarbg.sprite = monkeySpr;
            progressBar.sprite = monkeySpr;
        }
    }

    public void GetPropsInfo()
    {
        hatsBoughtString = GameEnums.HatsUnlocked;
        spectsBoughtString = GameEnums.SpectsUnlocked;
        chainBoughtString = GameEnums.ChainsUnlocked;
        shirtBoughtString = GameEnums.ShirtsUnlocked;
        pantsBoughtString = GameEnums.PantsUnlocked;
        shoesBoughtString = GameEnums.ShoesUnlocked;

        //Debug.Log("hatboughtstring::" + hatsBoughtString);
        //Debug.Log("spectsBoughtString::" + spectsBoughtString);
        //Debug.Log("chainBoughtString::" + chainBoughtString);
        //Debug.Log("shirtBoughtString::" + shirtBoughtString);
        //Debug.Log("pantsBoughtString::" + pantsBoughtString);
        //Debug.Log("shoesBoughtString::" + shoesBoughtString);

        #region Adding locked upgrades in a list
        //add locked hats in a list
        char[] tempHatChar = hatsBoughtString.ToCharArray();
        for (int i = 0; i < 10; i++)
        {
            if (tempHatChar[i] == '0')
                hatLockIndex.Add(i);
        }

        //add locked spects in a list
        char[] tempSpectsChar = spectsBoughtString.ToCharArray();
        for (int i = 0; i < 10; i++)
        {
            if (tempSpectsChar[i] == '0')
                spectsLockIndex.Add(i);
        }

        //add locked chains in a list
        char[] tempChainsChar = chainBoughtString.ToCharArray();
        for (int i = 0; i < 10; i++)
        {
            if (tempChainsChar[i] == '0')
                chainLockIndex.Add(i);
        }

        //add locked shirts in a list
        char[] tempShirtsChar = shirtBoughtString.ToCharArray();
        for (int i = 0; i < 10; i++)
        {
            if (tempShirtsChar[i] == '0')
                shirtLockIndex.Add(i);
        }

        //add locked pants in a list
        char[] tempPantsChar = pantsBoughtString.ToCharArray();
        for (int i = 0; i < 10; i++)
        {
            if (tempPantsChar[i] == '0')
                pantsLockIndex.Add(i);
        }
        //add locked shoes in a list
        char[] tempShoesChar = shoesBoughtString.ToCharArray();
        for (int i = 0; i < 10; i++)
        {
            if (tempShoesChar[i] == '0')
                shoesLockIndex.Add(i);
        }
        #endregion
        
    }
    void AssignPropsSprite(int num)
    {
        if (PropsCount == 1)//hats 
        {
            progressbarbg.sprite = hatSpr;
            progressBar.sprite = hatSpr;
        }
        else if (PropsCount == 2)//spects
        {
            progressbarbg.sprite = SpectsSpr;
            progressBar.sprite = SpectsSpr;
        }
        else if (PropsCount == 3)//chains
        {
            progressbarbg.sprite = chainSpr;
            progressBar.sprite = chainSpr;
        }
        else if (PropsCount == 4)//shirts
        {
            progressbarbg.sprite = shirtSpr;
            progressBar.sprite = shirtSpr;
        }
        else if (PropsCount == 5)//pants
        {
            progressbarbg.sprite = pantsSpr;
            progressBar.sprite = pantsSpr;
        }
        else if (PropsCount == 6)//shoes
        {
            progressbarbg.sprite = shoesSpr;
            progressBar.sprite = shoesSpr;
        }
    }


    public void NextBtnClicked()
    {
        

        if (ProgressValue == 1)
        {
            ProgressValue = 0;
            unlockCharacterPopup.SetActive(true);
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
                if(!GameController.isBonusLevel)
                GameEnums.LevelsUnlocked += 1;
                SceneManager.LoadScene("Menu");
            }
            if (GameController.isBonusLevel)
                GameController.isBonusLevel = false;
        }
    }
    public void NoThanksBtnClicked()
    {
        GameEnums.LevelsUnlocked += 1;
        unlockCharacterPopup.SetActive(false);
        //if (UnlockPetsinLC == 0) UnlockPetsinLC = 1;
        //else UnlockPetsinLC = 0;
        UnlockPetsinLC = (UnlockPetsinLC == 0) ? 0 : 1;
        SceneManager.LoadScene("Menu");
    }
}
