using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnums : MonoBehaviour
{
   
    public static int LevelsUnlocked
    {
        get
        {
            return PlayerPrefs.GetInt("levelsunlocked", 1);
        }
        set
        {
            PlayerPrefs.SetInt("levelsunlocked", value);
        }
    }
    
    public static int BonusLevelsUnlocked
    {
        get
        {
            return PlayerPrefs.GetInt("bonuslevelsunlocked", 0);
        }
        set
        {
            PlayerPrefs.SetInt("bonuslevelsunlocked", value);
        }
    }
    public static string PetsUnlocked
    {
        get
        {
            return PlayerPrefs.GetString("petsUnlocked", "100000");
            // return PlayerPrefs.GetString("petsUnlocked", "11111111111111111111");
        }
        set
        {
            PlayerPrefs.SetString("petsUnlocked", value);
        }
    }
    public static int TotalCoins
    {
        get
        {
            return PlayerPrefs.GetInt("totalcoins", 1000000);
        }
        set
        {
            PlayerPrefs.SetInt("totalcoins", value);
        }
    }
    public static int KeysCount
    {
        get
        {
            return PlayerPrefs.GetInt("keys", 0);
        }
        set
        {
            PlayerPrefs.SetInt("keys", value);
        }
    }

    public static int isVibration
    {
        get
        {
            return PlayerPrefs.GetInt("vibration", 1);
        }
        set
        {
            PlayerPrefs.SetInt("vibration", value);
        }
    }
    public static int Sound
    {
        get
        {
            return PlayerPrefs.GetInt("sound", 1);
        }
        set
        {
            PlayerPrefs.SetInt("sound", value);
        }
    }

    public static string PetName
    {
        get
        {
            return PlayerPrefs.GetString("petname", "YOU");
        }
        set
        {
            PlayerPrefs.SetString("petname", value);
        }
    }
    public static string GameControlsType
    {
        get
        {
            return PlayerPrefs.GetString("controlsType", "Touch");
        }
        set
        {
            PlayerPrefs.SetString("controlsType", value);
        }
    }

    #region Powerups Playerprefs
    public static int RunFasterPowerup
    {
        get
        {
            return PlayerPrefs.GetInt("runfasterPowerup", 3);
        }
        set
        {
            PlayerPrefs.SetInt("runfasterPowerup", value);
        }
    }

    public static int GoThroughWallsPowerup
    {
        get
        {
            return PlayerPrefs.GetInt("gothroughwallspowerup", 3);
        }
        set
        {
            PlayerPrefs.SetInt("gothroughwallspowerup", value);
        }
    }

    public static int XRayPowerup
    {
        get
        {
            return PlayerPrefs.GetInt("xraypowerup", 5);
        }
        set
        {
            PlayerPrefs.SetInt("xraypowerup", value);
        }
    }
    public static int GoInvisiblePowerup
    {
        get
        {
            return PlayerPrefs.GetInt("goinvisiblepowerup", 4);
        }
        set
        {
            PlayerPrefs.SetInt("goinvisiblepowerup", value);
        }
    }
    #endregion


    #region Saving Data of Pets

    //public static string CatsUnlocked
    //{
    //    get
    //    {
    //        return PlayerPrefs.GetString("unlockallcats", "1000000000");
    //       // return PlayerPrefs.GetString("unlockallcats", "1111111111");
    //    }
    //    set
    //    {
    //        PlayerPrefs.SetString("unlockallcats", value);
    //    }
    //}
    //public static string DogsUnlocked
    //{
    //    get
    //    {
    //        return PlayerPrefs.GetString("unlockalldogs", "1000000000");
    //        //return PlayerPrefs.GetString("unlockalldogs", "1111111111");
    //    }
    //    set
    //    {
    //        PlayerPrefs.SetString("unlockalldogs", value);
    //    }
    //}
    //public static string RabbitsUnlocked
    //{
    //    get
    //    {
    //        return PlayerPrefs.GetString("unlockallrabbits", "1000000000");
    //    }
    //    set
    //    {
    //        PlayerPrefs.SetString("unlockallrabbits", value);
    //    }
    //}
    //public static string PandasUnlocked
    //{
    //    get
    //    {
    //        return PlayerPrefs.GetString("unlockallpandas", "1000000000");
    //    }
    //    set
    //    {
    //        PlayerPrefs.SetString("unlockallpandas", value);
    //    }
    //}
    //public static string MonkeysUnlocked
    //{
    //    get
    //    {
    //        return PlayerPrefs.GetString("unlockallmonkeys", "1000000000");
    //    }
    //    set
    //    {
    //        PlayerPrefs.SetString("unlockallmonkeys", value);
    //    }
    //}
   
    #endregion

    #region Saving Data of Pets Props

    public static string HatsUnlocked
    {
        get
        {
            return PlayerPrefs.GetString("unlockallhats", "0000000000");
        }
        set
        {
            PlayerPrefs.SetString("unlockallhats", value);
        }
    }
    public static string SpectsUnlocked
    {
        get
        {
            return PlayerPrefs.GetString("unlockallspects", "0000000000");
        }
        set
        {
            PlayerPrefs.SetString("unlockallspects", value);
        }
    }
    public static string ChainsUnlocked
    {
        get
        {
            return PlayerPrefs.GetString("unlockallchains", "0000000000");
        }
        set
        {
            PlayerPrefs.SetString("unlockallchains", value);
        }
    }
    public static string ShirtsUnlocked
    {
        get
        {
            return PlayerPrefs.GetString("unlockallshirts", "0000000000");
        }
        set
        {
            PlayerPrefs.SetString("unlockallshirts", value);
        }
    }
    public static string PantsUnlocked
    {
        get
        {
            return PlayerPrefs.GetString("unlockallpants", "0000000000");
        }
        set
        {
            PlayerPrefs.SetString("unlockallpants", value);
        }
    }
    public static string ShoesUnlocked
    {
        get
        {
            return PlayerPrefs.GetString("unlockallshoes", "0000000000");
        }
        set
        {
            PlayerPrefs.SetString("unlockallshoes", value);
        }
    }

    #endregion

    #region Selected Pet props playerprefs

    public static int SelectedHats
    {
        get
        {
            return PlayerPrefs.GetInt("selectedhats", 0);
        }
        set
        {
            PlayerPrefs.SetInt("selectedhats", value);
        }
    }
    public static int SelectedSpects
    {
        get
        {
            return PlayerPrefs.GetInt("selectedspects", 0);
        }
        set
        {
            PlayerPrefs.SetInt("selectedspects", value);
        }
    }

    public static int SelectedShirts
    {
        get
        {
            return PlayerPrefs.GetInt("selectedshirts", 0);
        }
        set
        {
            PlayerPrefs.SetInt("selectedshirts", value);
        }
    }

    public static int SelectedPants
    {
        get
        {
            return PlayerPrefs.GetInt("selectedpants", 0);
        }
        set
        {
            PlayerPrefs.SetInt("selectedpants", value);
        }
    }

    public static int SelectedShoes
    {
        get
        {
            return PlayerPrefs.GetInt("selectedshoes", 0);
        }
        set
        {
            PlayerPrefs.SetInt("selectedshoes", value);
        }
    }
    #endregion
}
