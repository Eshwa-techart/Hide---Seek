using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChestBoxHandler : MonoBehaviour
{
    public static ChestBoxHandler instance;
    public int[] chestValue;
    public Text[] chestAmountText;
    public Text totalCoinsText;
    public GameObject key1, key2, key3;
    public GameObject keysObj;
    public GameObject watchVideoBtn;
   
    private void OnEnable()
    {
        instance = this;
        totalCoinsText.text = "" + GameEnums.TotalCoins;
        key1.SetActive(true); key2.SetActive(true); key3.SetActive(true);
        Debug.Log("Keycount::" + GameEnums.KeysCount); 
        AssignChestValues();
    }
    
    void AssignChestValues()
    {
        for(int i = 0; i < chestAmountText.Length; i++)
        {
            int rand;
            rand = chestValue[Random.Range(0, chestValue.Length)];
            chestAmountText[i].text = "" + rand;
        }
    }
    GameObject selectedButton;
    int bonus1, bonus2, bonus3, totalBonus;
    int fromValue;
    public void ChestBoxBtnClicked()
    {
        
        selectedButton = EventSystem.current.currentSelectedGameObject;

        if (selectedButton.GetComponent<Image>().raycastTarget)
        {
            //keyCount++;
            GameEnums.KeysCount -= 1;
            //if (SoundManager.instance)
            //{
            //    SoundManager.instance.StopAll();
            //    SoundManager.instance.PlaySound(SoundManager.instance.Sound_ChestCollect);
            //}
            if (GameEnums.KeysCount >= 0)
            {
                Debug.Log("open chest now");
                selectedButton.GetComponent<Image>().raycastTarget = false;
                selectedButton.transform.GetChild(1).gameObject.SetActive(false);
                selectedButton.transform.GetChild(0).gameObject.SetActive(true);
                //Destroy(selectedButton.GetComponent<ButtonAnim>());
                Destroy(selectedButton.GetComponent<iTween>());
                selectedButton.transform.localScale = Vector3.one;
            }
            else
            {
                Debug.Log("No keys Left");
            }
            if (GameEnums.KeysCount == 2)
            {
                string s;
                s = selectedButton.transform.GetChild(0).transform.GetChild(2).gameObject.GetComponent<Text>().text;
                bonus3 = int.Parse(s);
                key3.SetActive(false);
                Debug.Log("Bonus3::" + bonus3);
                GameEnums.TotalCoins += bonus3;
                iTween.ValueTo(gameObject, iTween.Hash(
                "from", fromValue,
                 "to", GameEnums.TotalCoins,
                 "time", 0.3f, "delay", 0.5,
                 "onupdatetarget", gameObject,
                 "onupdate", "tweenOnUpdateCallBack"));
            }
            else if (GameEnums.KeysCount == 1)
            {
                string s;
                s = selectedButton.transform.GetChild(0).transform.GetChild(2).gameObject.GetComponent<Text>().text;
                bonus2 = int.Parse(s);
                key2.SetActive(false);
                Debug.Log("Bonus32::" + bonus2);

                GameEnums.TotalCoins += bonus2;

                iTween.ValueTo(gameObject, iTween.Hash(
                "from", fromValue,
                 "to", GameEnums.TotalCoins,
                "time", 0.3f, "delay", 0.5,
                 "onupdatetarget", gameObject,
                 "onupdate", "tweenOnUpdateCallBack"));

            }
            else if (GameEnums.KeysCount == 0)
            {
                string s;
                s = selectedButton.transform.GetChild(0).transform.GetChild(2).gameObject.GetComponent<Text>().text;
                bonus1 = int.Parse(s);
                key1.SetActive(false);
                Debug.Log("Bonus1::" + bonus1);

                GameEnums.TotalCoins += bonus1;

                iTween.ValueTo(gameObject, iTween.Hash(
                "from", fromValue,
                 "to", GameEnums.TotalCoins,
                 "time", 0.3f, "delay", 0.5,
                 "onupdatetarget", gameObject,
                 "onupdate", "tweenOnUpdateCallBack", "onComplete", "EnableWatchVideoBtn"));
            }
           
        }
    }

    int num;
    void EnableWatchVideoBtn()
    {
        num++;
        if (num > 2)
        {

        }
        else
        {
            keysObj.SetActive(false);
            watchVideoBtn.SetActive(true);
        }
    }
    public void WatchVideoSuccessEvent()
    {
        GameEnums.KeysCount += 3;
        keysObj.SetActive(true);
        key1.SetActive(true);
        key2.SetActive(true);
        key3.SetActive(true);
        watchVideoBtn.SetActive(false);
    }
    void tweenOnUpdateCallBack(int newValue)
    {
        fromValue = newValue;
        totalCoinsText.text = "" + newValue;
    }
    public void BackBtnClicked()
    {
        //if (SoundManager.instance)
        //{
        //    SoundManager.instance.PlaySound(SoundManager.instance.Sound_Button);
        //}
      
        gameObject.SetActive(false);
        SceneManager.LoadScene("Menu");
    }
   
}
