using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaPage : MonoBehaviour
{
    //public List<GameObject> allCatsModelsList, allDogsModelsList, allRabbitModelsList, allPandasModelsList, allMonkeysModelsList;
    [SerializeField]
    List<GameObject> petsList;
    public List<GameObject> allHatsList, allSpectsList, allShirtsList, allPantsList, allShoesList;
    public List<Button> propsBtn;
    public List<GameObject> propsObj;
    public List<GameObject> unlockedPetsList;
    public Text petNameText;
    public Text noAvailablePropsText;
    public GameObject petGlowObj;
    public PetDressUp myPetToDressUp;
    // bool isPetsSpawned = false;

    GameObject SpaPetsObj;
    GameObject mypet;


    private void Awake()
    {
        SpaPetsObj = new GameObject();
        SpaPetsObj.name = "AllSpaPetsModels";
    }
    void OnEnable()
    {
        
        for (int i = 0; i < propsBtn.Count; i++)
        {
            propsBtn[i].GetComponent<Image>().color = Color.grey;
        }
        propsBtn[0].GetComponent<Image>().color = Color.white;

        //GetAllUnlockedPets();
        EnableCurrentPet();
        GetAllUnlockedProps();
        EnablePropsObj(0);
        checkHatsAvailable();
        myPetToDressUp = mypet.GetComponent<PetDressUp>();
    }

    void EnableCurrentPet()
    {
        mypet = petsList[CharacterSelection.petIndex];
        mypet.SetActive(true);
        mypet.transform.position = new Vector3(0, 2.35f, 0);
        petGlowObj.SetActive(true);
    }

    #region Get All Unlocked pets
    //void GetAllUnlockedPets()
    //{
    //    if (isPetsSpawned)
    //    {
    //        SpaPetsObj.SetActive(true);
    //        for (int i = 0; i < unlockedPetsList.Count; i++)
    //        {
    //            Destroy(unlockedPetsList[i]);
    //        }
    //        unlockedPetsList.Clear();
    //    }
    //    for (int i = 0; i < 10; i++)
    //    {
    //        //Get all unlocked cats into the list
    //        string s1 = GameEnums.CatsUnlocked;
    //        if (s1.Substring((i), 1) == "1")
    //        {
    //            GameObject go;
    //            go = Instantiate(allCatsModelsList[i]);
    //            go.transform.SetParent(SpaPetsObj.transform);
    //            unlockedPetsList.Add(go);
    //        }
    //        //Get all unlocked dogs into the list
    //        string s2 = GameEnums.DogsUnlocked;
    //        if (s2.Substring((i), 1) == "1")
    //        {
    //            GameObject go;
    //            go = Instantiate(allDogsModelsList[i]);
    //            go.transform.SetParent(SpaPetsObj.transform);
    //            unlockedPetsList.Add(go);
    //        }
    //        //Get all unlocked rabbits into the list
    //        string s3 = GameEnums.RabbitsUnlocked;
    //        if (s3.Substring((i), 1) == "1")
    //        {
    //            GameObject go;
    //            go = Instantiate(allRabbitModelsList[i]);
    //            go.transform.SetParent(SpaPetsObj.transform);
    //            unlockedPetsList.Add(go);
    //        }
    //        //Get all unlocked pandas into the list
    //        string s4 = GameEnums.PandasUnlocked;
    //        if (s4.Substring((i), 1) == "1")
    //        {
    //            GameObject go;
    //            go = Instantiate(allPandasModelsList[i]);
    //            go.transform.SetParent(SpaPetsObj.transform);
    //            unlockedPetsList.Add(go);
    //        }
    //        //Get all unlocked monkeys into the list
    //        string s5 = GameEnums.MonkeysUnlocked;
    //        if (s5.Substring((i), 1) == "1")
    //        {
    //            GameObject go;
    //            go = Instantiate(allMonkeysModelsList[i]);
    //            go.transform.SetParent(SpaPetsObj.transform);
    //            unlockedPetsList.Add(go);
    //        }
    //    }

    //    for (int i = 0; i < unlockedPetsList.Count; i++)
    //    {
    //        unlockedPetsList[i].SetActive(false);
    //        unlockedPetsList[i].transform.position = new Vector3(0, 2, 0);
    //    }
    //    unlockedPetsList[0].SetActive(true);
    //    isPetsSpawned = true;
    //}
    #endregion
    void GetAllUnlockedProps()
    {
        Debug.Log("Hats unlocked::" + GameEnums.HatsUnlocked);
        for (int i = 0; i < 10; i++)
        {
            //get all unlocked Hats into the list
            string s1 = GameEnums.HatsUnlocked;
            if (s1.Substring((i), 1) == "1")
            {
                allHatsList[i].SetActive(true);
            }
            else
            {
                allHatsList[i].SetActive(false);
            }

            //get all unlocked Spects into the list
            string s2 = GameEnums.SpectsUnlocked;
            if (s2.Substring((i), 1) == "1")
            {
                allSpectsList[i].SetActive(true);
            }
            else
            {
                allSpectsList[i].SetActive(false);
            }

            //get all unlocked Shirts into the list
            string s4 = GameEnums.ShirtsUnlocked;
            if (s4.Substring((i), 1) == "1")
            {
                allShirtsList[i].SetActive(true);
            }
            else
            {
                allShirtsList[i].SetActive(false);
            }

            //get all unlocked Pants into the list
            string s5 = GameEnums.PantsUnlocked;
            if (s5.Substring((i), 1) == "1")
            {
                allPantsList[i].SetActive(true);
            }
            else
            {
                allPantsList[i].SetActive(false);
            }

            //get all unlocked Shoes into the list
            string s6 = GameEnums.ShoesUnlocked;
            if (s6.Substring((i), 1) == "1")
            {
                allShoesList[i].SetActive(true);
            }
            else
            {
                allShoesList[i].SetActive(false);
            }
        }

    }

    public void EnablePropsObj(int ID)
    {
        for (int i = 0; i < propsObj.Count; i++)
        {
            propsObj[i].SetActive(false);
        }
        propsObj[ID].SetActive(true);
        //checkHatsAvailable();
        CheckOtherPropsAvailable(ID);
        //props buttons
        for (int i = 0; i < propsBtn.Count; i++)
        {
            //propsBtn[i].GetComponent<Image>().color = Color.grey;
            propsBtn[i].transform.GetChild(1).gameObject.SetActive(false);
        }
       // propsBtn[ID].GetComponent<Image>().color = Color.white;
        propsBtn[ID].transform.GetChild(1).gameObject.SetActive(true);

    }
    void checkHatsAvailable()
    {
        //if no hats are available
        if (GameEnums.HatsUnlocked == "0000000000")
        {
            noAvailablePropsText.gameObject.SetActive(true);
            noAvailablePropsText.text = "No Hats Available".ToUpper();
        }
        else
        {
            noAvailablePropsText.gameObject.SetActive(false);
        }
    }
    void CheckOtherPropsAvailable(int id)
    {
        //Hats
        if (id == 0)
        {
            if (GameEnums.HatsUnlocked == "0000000000")
            {
                noAvailablePropsText.gameObject.SetActive(true);
                noAvailablePropsText.text = "No Hats Available".ToUpper();
            }
            else
            {
                noAvailablePropsText.gameObject.SetActive(false);
            }
        }
        //Spects
        if (id == 1)
        {
            if (GameEnums.SpectsUnlocked == "0000000000")
            {
                noAvailablePropsText.gameObject.SetActive(true);
                noAvailablePropsText.text = "No Spects Available".ToUpper(); 
            }
            else
            {
                noAvailablePropsText.gameObject.SetActive(false);
            }
        }
      
        //Shirts
        else if (id == 2)
        {
            if (GameEnums.ShirtsUnlocked == "0000000000")
            {
                noAvailablePropsText.gameObject.SetActive(true);
                noAvailablePropsText.text = "No Shirts Available".ToUpper();
            }
            else
            {
                noAvailablePropsText.gameObject.SetActive(false);
            }
        }
        //Pants
        else if (id == 3)
        {
            if (GameEnums.PantsUnlocked == "0000000000")
            {
                noAvailablePropsText.gameObject.SetActive(true);
                noAvailablePropsText.text = "No Pants Available".ToUpper();
            }
            else
            {
                noAvailablePropsText.gameObject.SetActive(false);
            }
        }
        //Shoes
        else if (id == 4)
        {
            if (GameEnums.ShoesUnlocked == "0000000000")
            {
                noAvailablePropsText.gameObject.SetActive(true);
                noAvailablePropsText.text = "No Shoes Available".ToUpper();
            }
            else
            {
                noAvailablePropsText.gameObject.SetActive(false);
            }
        }
        

    }

    int count;
    public void NextBtnClicked()
    {
        if (count < unlockedPetsList.Count)
        {
            count++;
            unlockedPetsList[count].SetActive(true);
            unlockedPetsList[count - 1].SetActive(false);
        }
    }
    public void PrevBtnClicked()
    {
        if (count > 0)
        {
            count--;
            unlockedPetsList[count].SetActive(true);
            unlockedPetsList[count + 1].SetActive(false);
        }
    }
    public void BackBtnClicked()
    {
        mypet.SetActive(false);
        MenuScript.instance.EnablePages(0);
        SpaPetsObj.SetActive(false);
        petGlowObj.SetActive(false);

    }

   
   

    #region Select Props Click Events

    public void SelectHat(int index)
    {
        for (int i = 0; i < myPetToDressUp.hatsList.Count; i++)
        {
            myPetToDressUp.hatsList[i].SetActive(false);
        }
        myPetToDressUp.hatsList[index - 1].SetActive(true);
        GameEnums.SelectedHats = index - 1;
        PlayerPrefs.SetString("HatSelected", "true");
        MenuScript.hatvalueArray[CharacterSelection.petIndex] = GameEnums.SelectedHats;
        PlayerPrefsX.SetIntArray(MenuScript.hatPurchasedIndex, MenuScript.hatvalueArray);
    }

    public void SelectSpects(int index)
    {
        for (int i = 0; i < myPetToDressUp.spectsList.Count; i++)
        {
            myPetToDressUp.spectsList[i].SetActive(false);
        }
        myPetToDressUp.spectsList[index - 1].SetActive(true);
        GameEnums.SelectedSpects = index - 1;
        PlayerPrefs.SetString("SpectsSelected", "true");
        MenuScript.spectsvalueArray[CharacterSelection.petIndex] = GameEnums.SelectedSpects;
        PlayerPrefsX.SetIntArray(MenuScript.spectsPurchasedIndex, MenuScript.spectsvalueArray);
    }

    public void SelectShirts(int index)
    {
        for (int i = 0; i < myPetToDressUp.shirtsList.Count; i++)
        {
            myPetToDressUp.shirtsList[i].SetActive(false);
        }
        myPetToDressUp.shirtsList[index - 1].SetActive(true);
        GameEnums.SelectedShirts = index - 1;

        MenuScript.shirtsvalueArray[CharacterSelection.petIndex] = GameEnums.SelectedShirts;
        PlayerPrefsX.SetIntArray(MenuScript.shirtsPurchasedIndex, MenuScript.shirtsvalueArray);
    }
    public void SelectShorts(int index)
    {
        for (int i = 0; i < myPetToDressUp.pantsList.Count; i++)
        {
            myPetToDressUp.pantsList[i].SetActive(false);
        }
        myPetToDressUp.pantsList[index - 1].SetActive(true);
        GameEnums.SelectedPants = index - 1;

        MenuScript.pantsvalueArray[CharacterSelection.petIndex] = GameEnums.SelectedPants;
        PlayerPrefsX.SetIntArray(MenuScript.pantsPurchasedIndex, MenuScript.pantsvalueArray);
    }

    public void SelectShoes(int index)
    {
        for (int i = 0; i < myPetToDressUp.shoesList.Count; i++)
        {
            myPetToDressUp.shoesList[i].SetActive(false);
        }
        myPetToDressUp.shoesList[index - 1].SetActive(true);
        GameEnums.SelectedShoes = index - 1;

        MenuScript.shoesvalueArray[CharacterSelection.petIndex] = GameEnums.SelectedShoes;
        PlayerPrefsX.SetIntArray(MenuScript.shoesPurchasedIndex, MenuScript.shoesvalueArray);
    }
    #endregion


}
