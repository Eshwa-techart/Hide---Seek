using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{ 
    [Header("Sound")]
    public GameObject soundToggle;
    public GameObject soundOnPos, soundOffPos;
    [Space(10)] 

    [Header("Vibration")]
    public GameObject vibrationToggle;
    public GameObject vibrationOnPos, vibrationOffPos;
    [Space(10)]

    public InputField petNameField;
    [Space(10)]

    [Header("Game Controls")]
    public Button touchBtn;
    public Button joystickBtn;
    public Sprite greenBtnSpr;
    public Sprite greyBtnSpr;
    void OnEnable()
    {
        Debug.Log("Gameenums.sound::" + GameEnums.Sound);
        if (GameEnums.Sound == 0)
            soundToggle.transform.position = soundOffPos.transform.position;
        else if (GameEnums.Sound == 1)
            soundToggle.transform.position = soundOnPos.transform.position;


        if (GameEnums.isVibration == 0)
            vibrationToggle.transform.position = vibrationOffPos.transform.position;
        else if (GameEnums.isVibration == 1)
            vibrationToggle.transform.position = vibrationOnPos.transform.position;

        petNameField.text = GameEnums.PetName;

        if (GameEnums.GameControlsType == "Touch")
        {
            touchBtn.GetComponent<Image>().sprite = greenBtnSpr;
            joystickBtn.GetComponent<Image>().sprite = greyBtnSpr;
        }
        else if (GameEnums.GameControlsType == "Joystick")
        {
            touchBtn.GetComponent<Image>().sprite = greyBtnSpr;
            joystickBtn.GetComponent<Image>().sprite = greenBtnSpr;
        }
    }

    public void BackbtnClicked()
    {
        if (petNameField.text == string.Empty)
        {
            GameEnums.PetName = "YOU";
        }
        else
        {
            GameEnums.PetName = petNameField.text;
        }
        MenuScript.instance.EnablePages(0);
    }
    public void SoundBtnClicked()
    {
        if (GameEnums.Sound == 0)
        {
            GameEnums.Sound = 1;
            AudioListener.volume = 1;
            iTween.MoveTo(soundToggle, iTween.Hash("x", soundOnPos.transform.position.x, "time", 0.5));
        }
        else if(GameEnums.Sound == 1)
        {
            AudioListener.volume = 0;
            GameEnums.Sound = 0;
            iTween.MoveTo(soundToggle, iTween.Hash("x", soundOffPos.transform.position.x, "time", 0.5));
        }
    }
    public void VibrationBtnClicked()
    {
        if (GameEnums.isVibration == 0)
        {
            GameEnums.isVibration = 1;
            iTween.MoveTo(vibrationToggle, iTween.Hash("x", vibrationOnPos.transform.position.x, "time", 0.5));
        }
        else if (GameEnums.isVibration == 1)
        {
            GameEnums.isVibration = 0;
            iTween.MoveTo(vibrationToggle, iTween.Hash("x", vibrationOffPos.transform.position.x, "time", 0.5));
        }
    }

    public void EnableTouchControls()
    {
        GameEnums.GameControlsType = "Touch";
        joystickBtn.GetComponent<Image>().sprite = greyBtnSpr;
        touchBtn.GetComponent<Image>().sprite = greenBtnSpr;
    }
    public void EnableJoystickControls()
    {
        GameEnums.GameControlsType = "Joystick";
        joystickBtn.GetComponent<Image>().sprite = greenBtnSpr;
        touchBtn.GetComponent<Image>().sprite = greyBtnSpr;
    }
    public void PPClicked()
    {

    }
    
}
