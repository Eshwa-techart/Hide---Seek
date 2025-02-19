using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToastMsg : MonoBehaviour
{
    public static ToastMsg instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    public void ShowToast(string msg)
    {
        //toastText.text = "" + msg;
        gameObject.GetComponentInChildren<Text>().text = "" + msg;
        iTween.MoveTo(gameObject, iTween.Hash("y", 650, "time", 0.6, "easetype", iTween.EaseType.easeInBack, "islocal", true));
        iTween.MoveTo(gameObject, iTween.Hash("y", 1500, "time", 0.6, "easetype", iTween.EaseType.easeOutBack, "delay", 2, "islocal", true));
    }
}
