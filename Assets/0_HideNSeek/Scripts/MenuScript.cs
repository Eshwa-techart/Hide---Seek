using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public static MenuScript instance;

    public List<GameObject> menuPages;
    public static bool isSeekGame, isHideGame;
    public static bool isPetEscapeLevels;

    public GameObject loadingPage;

    [Space(10)]
    public Camera menuLevelCamera;
    public Camera characterCamera;
    public GameObject background;
    public Canvas menuCanvas;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
   

    void Start()
    {
        EnablePages(0);

        SetHatsPlayerprefs();
        SetSpectsPlayerprefs();
        SetShirtsPlayerprefs();
        SetPantsPlayerprefs();
        SetShoesPlayerprefs();
    }

    public void ButtonClick(string btnName)
    {
        switch (btnName)
        {
            case "HideBtn":
                isHideGame = true;
                isSeekGame = false;
                isPetEscapeLevels = false;
                ShowLoadingPage();
                SceneManager.LoadScene("Ingame");
                break;
            case "SeekBtn":
                isHideGame = false;
                isSeekGame = true;
                isPetEscapeLevels = false;
                ShowLoadingPage();
                SceneManager.LoadScene("Ingame");
                break;
            
            case "PropsBtn":
                EnablePages(1);
                break;

            case "SpaBtn":
                EnablePages(2);  
                break;

            case "CharacterBtn":
                EnablePages(4);
                break;

            case "NoAdsBtn":
                break;

            case "WatchVideoBtn":
                break;

            case "SettingsBtn":
                EnablePages(6);
                break;

            case "StoreBtn":
                EnablePages(3);
                break;


            case "SpinWheelBtn":
                EnablePages(5);
                break;
        }       
    }

    public void EnablePages(int ID)
    {
        for (int i = 0; i < menuPages.Count; i++)
        {
            menuPages[i].SetActive(false);
        }
        menuPages[ID].SetActive(true);
        if (ID == 0)
        {
            ShowMenuPage();
        }
        else
        {
            ShowOtherPages();
        }
    }

    public void ShowLoadingPage()
    {
        loadingPage.SetActive(true);
    }

    void ShowMenuPage()
    {
        menuLevelCamera.enabled = true;
        characterCamera.enabled = false;
        background.SetActive(false);
        menuCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
    }
    void ShowOtherPages()
    {
        menuLevelCamera.enabled = false;
        characterCamera.enabled = true;
        background.SetActive(true);
        menuCanvas.renderMode = RenderMode.ScreenSpaceCamera;
    }


    #region Props PlayerprefsX
    public static int[] hatvalueArray;
    public static int[] spectsvalueArray;
    public static int[] shirtsvalueArray;
    public static int[] pantsvalueArray;
    public static int[] shoesvalueArray;
    public static string hatPurchasedIndex = "purchasedHatIndex";
    public static string spectsPurchasedIndex = "purchasedSpetcsIndex";
    public static string shirtsPurchasedIndex = "purchasedShirtsIndex";
    public static string pantsPurchasedIndex = "purchasedPantsIndex";
    public static string shoesPurchasedIndex = "purchasedShoesIndex";
    public void SetHatsPlayerprefs()
    {
        hatvalueArray = PlayerPrefsX.GetIntArray(hatPurchasedIndex, 0, 6);
        PlayerPrefsX.SetIntArray(hatPurchasedIndex, hatvalueArray);
        //for (int i = 0; i < 6; i++)
        //{
        //    Debug.LogError("hat values " + i + " " + hatvalueArray[i]);
        //}
    }
    public void SetSpectsPlayerprefs()
    {
        spectsvalueArray = PlayerPrefsX.GetIntArray(spectsPurchasedIndex, 0, 6);
        PlayerPrefsX.SetIntArray(spectsPurchasedIndex, spectsvalueArray);
        //for (int i = 0; i < 6; i++)
        //{
        //    Debug.LogError("spectsvalueArray " + i + " " + spectsvalueArray[i]);
        //}
    }

    public void SetShirtsPlayerprefs()
    {
        shirtsvalueArray = PlayerPrefsX.GetIntArray(shirtsPurchasedIndex, 0, 6);
        PlayerPrefsX.SetIntArray(shirtsPurchasedIndex, shirtsvalueArray);
        //for (int i = 0; i < 6; i++)
        //{
        //    Debug.LogError("shirtsvalueArray " + i + " " + shirtsvalueArray[i]);
        //}
    }

    public void SetPantsPlayerprefs()
    {
        pantsvalueArray = PlayerPrefsX.GetIntArray(pantsPurchasedIndex, 0, 6);
        PlayerPrefsX.SetIntArray(pantsPurchasedIndex, pantsvalueArray);
        //for (int i = 0; i < 6; i++)
        //{
        //    Debug.LogError("pantsvalueArray " + i + " " + pantsvalueArray[i]);
        //}
    }

    public void SetShoesPlayerprefs()
    {
        shoesvalueArray = PlayerPrefsX.GetIntArray(shoesPurchasedIndex, 0, 6);
        PlayerPrefsX.SetIntArray(shoesPurchasedIndex, shoesvalueArray);
        //for (int i = 0; i < 6; i++)
        //{
        //    Debug.LogError("shoesvalueArray " + i + " " + shoesvalueArray[i]);
        //}
    }
    #endregion

  
}
