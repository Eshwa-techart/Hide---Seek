using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinWheelController : MonoBehaviour
{
    private bool _isStarted;
    private float[] _sectorsAngles;
    private float _finalAngle;
    private float _startAngle = 0;
    private float _currentLerpRotationTime;
   
    public Button spinBtn;
    public GameObject spinWheelObj;           // Rotatable Object with rewards
    public GameObject claimPopup, spinWheelParent;


    public Image claimPageicon;
    public Sprite coinsSpr, xraySpr, invisibleSpr, goThroughWallsSpr, runFasterSpr;
    public Text claimDescriptionText;

    public GameObject coinsMoveObj;
    public static SpinWheelController instance;
   
    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        spinBtn.gameObject.SetActive(true);
        spinWheelParent.SetActive(true);
        claimPopup.SetActive(false);

        claimPopup.transform.localScale = Vector3.one;

    }

    public void SpinBtnClicked()
    {

        //spinBtn.GetComponent<iTween>().enabled = false;
        _currentLerpRotationTime = 0f;

        // Fill the necessary angles (for example if you want to have 12 sectors you need to fill the angles with 30 degrees step)
        // _sectorsAngles = new float[] { 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330, 360 };
        _sectorsAngles = new float[] { 45, 90, 135, 180, 225, 270, 315, 360 };

        int fullCircles = 10;
        float randomFinalAngle = _sectorsAngles[UnityEngine.Random.Range(0, _sectorsAngles.Length)];

        // Here we set up how many circles our wheel should rotate before stop
        _finalAngle = -(fullCircles * 360 + randomFinalAngle);
        _isStarted = true;
    }
    void EnableClaimPopup()
    {
        Debug.Log("start angle::" + _startAngle);
        spinBtn.gameObject.SetActive(false);
        spinWheelParent.SetActive(false);
        claimPopup.SetActive(true);
        clickCount = 0;
       


        switch ((int)_startAngle)
        {
            case 0:
                Debug.Log("500 coins");
                claimPageicon.sprite = coinsSpr;
                claimDescriptionText.text = "YOU HAVE EARNED 500 COINS.";
                break;
            case -315:
                Debug.Log("3 Go through Walls");
                claimDescriptionText.text = "YOU HAVE EARNED 3 GO THROUGH WALLS POWERUP.";
                claimPageicon.sprite = goThroughWallsSpr;
                break;
            case -270:
                Debug.Log("100 coins");
                claimDescriptionText.text = "YOU HAVE EARNED 100 COINS.";
                claimPageicon.sprite = coinsSpr;
                break;
            case -225:
                Debug.Log("3 Invisible");
                claimDescriptionText.text = "YOU HAVE EARNED 3 INVISIBLE POWERUP.";
                claimPageicon.sprite = invisibleSpr;
                break;
            case -180:
                Debug.Log("150 coins");
                claimDescriptionText.text = "YOU HAVE EARNED 150 COINS.";
                claimPageicon.sprite = coinsSpr;
                break;
            case -135:
                Debug.Log("3 Xray vision");
                claimDescriptionText.text = "YOU HAVE EARNED 3 X-RAY VISION POWERUP.";
                claimPageicon.sprite = xraySpr;
                break;
            case -90:
                Debug.Log("300 coins");
                claimDescriptionText.text = "YOU HAVE EARNED 300 COINS.";
                claimPageicon.sprite = coinsSpr;
                break;
            case -45:
                Debug.Log("3 Run Faster");
                claimDescriptionText.text = "YOU HAVE EARNED 3 2X RUN POWERUP.";
                claimPageicon.sprite = runFasterSpr;
                break;
            default:
                break;
        }
        claimPageicon.SetNativeSize();

    }
    float fromValue;
    void CoinsTweenOnUpdateCallBack(int newValue)
    {
        //fromValue = newValue;
        fromValue = prevCoinsVal;
    }
    void KeysTweenOnUpdateCallBack(int newValue)
    {
        //fromValue = newValue;
        fromValue = prevKeysVal;
    }
    int clickCount;
    int prevKeysVal, prevCoinsVal;
    public void ClaimBtnClicked()
    {
        if (clickCount == 0)
        {
            clickCount++;

            switch ((int)_startAngle)
            {
                case 0:
                    GameEnums.TotalCoins += 500;
                    coinsMoveObj.SetActive(true);
                    ToastMsg.instance.ShowToast("500 Coins Added Successfully");

                    break;
                case -315:
                    GameEnums.GoThroughWallsPowerup += 3;
                    ToastMsg.instance.ShowToast("3 Go through Walls Powerup Added");


                    break;
                case -270:
                    GameEnums.TotalCoins += 100;
                    coinsMoveObj.SetActive(true);
                    ToastMsg.instance.ShowToast("100 Coins Added Successfully");

                    break;
                case -225:
                    GameEnums.GoInvisiblePowerup += 3;
                    ToastMsg.instance.ShowToast("3 Invisible Powerup Added");

                    break;
                case -180:
                    GameEnums.TotalCoins += 150;
                    coinsMoveObj.SetActive(true);
                    ToastMsg.instance.ShowToast("150 Coins Added Successfully");

                    break;
                case -135:
                    GameEnums.XRayPowerup += 3;
                    ToastMsg.instance.ShowToast("3 X-Ray Vision Powerup Added");

                    break;
                case -90:
                    GameEnums.TotalCoins += 300;
                    coinsMoveObj.SetActive(true);
                    ToastMsg.instance.ShowToast("300 Coins Added Successfully");

                    break;
                case -45:
                    GameEnums.RunFasterPowerup += 3;
                    ToastMsg.instance.ShowToast("3 Run Faster Powerup Added");

                    break;
                default:

                    break;
            }
            StartCoroutine(CloseSpinWheelObj(3.2f));
        }
        // Here you can set up rewards for every sector of wheel
    }
    IEnumerator CloseSpinWheelObj(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        iTween.ScaleTo(claimPopup.gameObject, iTween.Hash("x", 0, "y", 0, "time", 0.35f, "easetype", iTween.EaseType.easeInBack));
        yield return new WaitForSeconds(0.5f);

       
        gameObject.SetActive(false);
        MenuScript.instance.EnablePages(0);

    }

    public void CoinsBtnClicked()
    {
        gameObject.SetActive(false);
    }
    public void RingsBtnClicked()
    {

    }
    void Update()
    {
        // Make turn button non interactable if user has not enough money for the turn
        if (_isStarted)
        {
            spinBtn.enabled = false;
        }
        else
        {
            spinBtn.enabled = true;
        }

        if (!_isStarted)
            return;

        float maxLerpRotationTime = 3f;

        // increment timer once per frame
        _currentLerpRotationTime += Time.deltaTime;
        if (_currentLerpRotationTime > maxLerpRotationTime || spinWheelObj.transform.eulerAngles.z == _finalAngle)
        {
            _currentLerpRotationTime = maxLerpRotationTime;
            _isStarted = false;
            _startAngle = _finalAngle % 360;

            Invoke(nameof(EnableClaimPopup), 1);

        }

        // Calculate current position using linear interpolation
        float t = _currentLerpRotationTime / maxLerpRotationTime;

        // This formulae allows to speed up at start and speed down at the end of rotation.
        // Try to change this values to customize the speed
        t = t * t * t * (t * (6f * t - 15f) + 10f);

        float angle = Mathf.Lerp(_startAngle, _finalAngle, t);
        spinWheelObj.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public void BackBtnClicked()
    {

        gameObject.SetActive(false);
        MenuScript.instance.EnablePages(0);

    }
}

   