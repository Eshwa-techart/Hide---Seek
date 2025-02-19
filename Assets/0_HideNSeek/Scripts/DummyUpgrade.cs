using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DummyUpgrade : MonoBehaviour
{
    public static DummyUpgrade Instance;
    public static int carIndex = 0;
    public GameObject[] carArray;
    public Text totalCoinsText;
    public GameObject nextBtn, prevBtn;
    public int[] carCostAry;
    public Text carCostText;
    public GameObject selectBtn, buyBtn;
    private string vehicleBoughtString;
    public GameObject StorePage;
    public static string VehiclesUnlocked
    {
        get
        {
            return PlayerPrefs.GetString("vehicles", "10000000");
        }
        set
        {
            PlayerPrefs.SetString("vehicles", value);
        }
    }
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        vehicleBoughtString = VehiclesUnlocked;
        Debug.Log("Carindex::" + carIndex + " Cararray length::" + carArray.Length);
        UpdateCoinsText(100000);
        CarUnlockCheck(carIndex);

    }
    public void UpdateCoinsText(int coins)
    {
        GameEnums.TotalCoins += coins;
        totalCoinsText.text = "" + GameEnums.TotalCoins;
    }
    public void NextBtnClicked()
    {
        prevBtn.SetActive(true);
        carIndex = carIndex + 1;
        carArray[carIndex - 1].SetActive(false);
        carArray[carIndex - 1].transform.position = new Vector3(carArray[carIndex - 1].transform.position.x, 0.6f, carArray[carIndex - 1].transform.position.z);
        carArray[carIndex].SetActive(true);
        ShowCarPrice();
        CarUnlockCheck(carIndex);
        if (carIndex >= carArray.Length - 1) nextBtn.SetActive(false);
    }
    public void PrevBtnClicked()
    {
        carIndex = carIndex - 1;
        carArray[carIndex + 1].SetActive(false);
        carArray[carIndex + 1].transform.position = new Vector3(carArray[carIndex + 1].transform.position.x, 0.6f, carArray[carIndex + 1].transform.position.z);
        carArray[carIndex].SetActive(true);
        ShowCarPrice();
        CarUnlockCheck(carIndex);
        if (carIndex == 0) prevBtn.SetActive(false);
    }
    void ShowCarPrice()
    {
        carCostText.text = "" + carCostAry[carIndex];
    }
    public void BackBtnClicked()
    {
        SceneManager.LoadScene("0_LevelSelection");
    }
    public string sceneToLoad;
    public void SelectBtnClicked()
    {
        SceneManager.LoadScene(sceneToLoad);

    }
    public void BuyBtnClick(int index)
    {

        char[] tempChar = vehicleBoughtString.ToCharArray();        //divide strings to single character
        index = carIndex;
        if (carCostAry[index] <= GameEnums.TotalCoins)
        {
            Debug.Log("temop char::" + index);
            tempChar[index] = '1';
            VehiclesUnlocked = "";
            foreach (char ch in tempChar)
            {
                VehiclesUnlocked += ch.ToString();
            }
            Debug.Log("vehicle unlockd:::::" + VehiclesUnlocked);
            CarUnlockCheck(carIndex);

        }
        else
        {
            Debug.Log("show store here");
        }
    }
    public void CarUnlockCheck(int _index)
    {
        _index = carIndex;
        vehicleBoughtString = VehiclesUnlocked;
        if (vehicleBoughtString.Substring((_index), 1) == "1")
        {
            Debug.Log("array in carunlockcheck111111111111::" + VehiclesUnlocked);
            selectBtn.SetActive(true);
            buyBtn.SetActive(false);
        }

        else
        {
            Debug.Log("array in carunlockcheck2222222222222::" + VehiclesUnlocked);
            selectBtn.SetActive(false);
            buyBtn.SetActive(true);
        }


    }
    public void StoreBtnClicked()
    {
        StorePage.SetActive(true);
    }

    //to unlock cars
    // char[] _snipers = StaticVariables.strUnlockedSnipers.ToCharArray();
    // _snipers[1] = '1';
    //StaticVariables.strUnlockedSnipers = "";
    //foreach (char ch in _snipers)
    // StaticVariables.strUnlockedSnipers += ch.ToString();
    //     GameEnums.VehiclesUnlocked = GameEnums.VehicleUnlockedRef;
}
