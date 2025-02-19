using System.Collections;
using System.Collections.Generic;
using MenteBacata.ScivoloCharacterControllerDemo;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public enum GameType
    {
        None,
        Seek,
        Hide
    }
    public GameType myGameType;

    public enum PetType
    {
        none,
        cat,
        dog,
        monkey,
        panda,
        pig,
        rabbit
    }
    public PetType myPetType;

    [SerializeField] float timerVal = 20;


    [Header("All GameObjects")]
    GameObject seekGameplayPlayers;
    GameObject hideGameplayPlayers;
    [SerializeField] GameObject prelc, prelf;
    public GameObject levelCompletePage, levelFailPage, petRescue_lcPage;
    public GameObject xRayBtn, goThroughWallsBtn;
    public List<GameObject> aiPlayersList;
    [SerializeField] GameObject saveOtherPetsEffect;
    [SerializeField] GameObject coinsCollectingEffect;
    [SerializeField] GameObject keyCollectingEffect;
    public GameObject pausePage;
    public GameObject startInvisiblePopup;
    public GameObject xRayImage;
    GameObject playerPos;
    public GameObject powerupsHolder;


    [Header("All Boolean")]
    public bool isTesting;
    public static bool isBonusLevel;
    public bool isEnableJoystick;
    public bool isStartTimer;
    [SerializeField]
    public bool isShowJoystick;
    bool cameraZoomIn, cameraZoomOut;
    bool isLevelFailed;
    public bool footPrintsOn;
    public bool isPlayerInvicible;
    public bool enableXRayVision;
    [SerializeField]
    bool bLevel;


    [Header("All Integers")]
    [SerializeField] int testLevelNum;
    public int LevelTarget;
    public int CoinsVal;
    public int coinsCollectedInLevel;
    public int savedLifeCount;
    public int petArrestedcount;
    [SerializeField]
    int bonusTestLevelNum;
    [SerializeField]
    int levelsLoop;


    [Header("All UI Texts")]
    public Text timerText;
    public Text countDownText;
    public Text timeToHide;
    public Text coinsText;
    public Text levelNumText;
    [SerializeField] Text moveSpeedText;
    public Text speedRunningCount, invisibleCount, xRayCount, goThroughWallsCount;

    public Sprite yellowKeySpr;

    [Header("All References")]
    
    public AIController aiSeeker;
    public Camera playerCamera;
    public PlayerController player;
    public GameObject playerObj;
    public DynamicJoystick joystick;
    LevelData myLevelData;


    
    public List<Image> savedPetLifeObj;
    public List<Image> keysList;
    
    public List<string> petnamesList;

    
    
   
   


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void LoadPlayer()
    {
        Vector3 playerPos;
        playerPos= new Vector3(myLevelData.playerSpawnPos.transform.position.x,
            myLevelData.playerSpawnPos.transform.position.y, myLevelData.playerSpawnPos.transform.position.z);

        GameObject playerIns = Instantiate(playerObj, playerPos, myLevelData.playerSpawnPos.transform.rotation);
        player = playerIns.GetComponent<PlayerController>();
    }
    void Start()
    {
        isEnableJoystick = true;

        LoadLevel();
        // player.gameObject.transform.position = new Vector3(myLevelData.playerSpawnPos.transform.position.x,
        //  myLevelData.playerSpawnPos.transform.position.y, myLevelData.playerSpawnPos.transform.position.z);
        LoadPlayer();
        StartCoroutine(StartCountdown(1f));
        if (isBonusLevel)
        {
            StartCoroutine(StartBonusLevelGameplay(4f));
            powerupsHolder.SetActive(false);
            return;
        }


        else if (MenuScript.isSeekGame && !isBonusLevel)
        {
            StartCoroutine(CameraZoom(0.5f));

            GameObject seekGameObj;
            seekGameplayPlayers = Resources.Load("SeekGameplayPlayers") as GameObject;
            seekGameObj = Instantiate(seekGameplayPlayers, myLevelData.OtherPlayersSpawnPos.transform.position, Quaternion.identity);
            seekGameObj.SetActive(true);

            AIController[] allChildrens = seekGameObj.GetComponentsInChildren<AIController>();
            foreach (AIController child in allChildrens)
            {
                //child.gameObject.SetActive(false);
                aiPlayersList.Add(child.gameObject);
            }
            InvokeRepeating(nameof(PlayAiSmokeEffect), 5, 5);
            goThroughWallsBtn.SetActive(false);
        }
        else if (MenuScript.isHideGame && !isBonusLevel)
        {
            StartCoroutine(StartHideGameplayNow(0f));
            joystick.gameObject.SetActive(true);

            GameObject hideGameObj;
            hideGameplayPlayers = Resources.Load("HideGameplayPlayers") as GameObject;
            hideGameObj = Instantiate(hideGameplayPlayers, myLevelData.OtherPlayersSpawnPos.transform.position, Quaternion.identity);
            hideGameObj.SetActive(true);

            player.objectsToActivate[0].SetActive(true);
            //AIController[] allChildrens = hideGameObj.GetComponentsInChildren<AIController>();
            //foreach (AIController child in allChildrens)
            //{
            //    aiPlayersList.Add(child.gameObject);
            //}
            //aiSeeker = aiPlayersList[aiPlayersList.Count - 1].GetComponent<AIController>();
            

            xRayBtn.SetActive(false);
        }
        for (int i = 0; i < GameEnums.KeysCount; i++)
        {
            keysList[i].color = Color.white;
        }

        Invoke(nameof(GetAiPetsNames), 1f);

        ShowPowerUpCounts();

        ShowKeysUI();
    }

    int count;
    void GetAiPetsNames()
    {
        if (MenuScript.isHideGame) count = aiPlayersList.Count - 1;
        else if (MenuScript.isSeekGame) count = 6;
        for (int i = 0; i < count; i++)//-1 because seeker doesnt have a name (to avoid null reference)
        {
            petnamesList.Add(aiPlayersList[i].GetComponent<AIController>().PetName);
        }
    }
    void PlayAiSmokeEffect()
    {
        aiPlayersList[Random.Range(0, aiPlayersList.Count)].GetComponent<AIController>().PlaySmoke();
    }

    int levelLoopCount;
    int levelLoopNum;
    void LoadLevel()
    {
       // Debug.Log("levels unlocked::" + GameEnums.LevelsUnlocked);

        GameObject go;
        if (isTesting)
        {
            if (myGameType == GameType.Hide)
            {
                MenuScript.isSeekGame = false;
                MenuScript.isHideGame = true;
            }
            else if (myGameType == GameType.Seek)
            {
                MenuScript.isHideGame = false;
                MenuScript.isSeekGame = true;
            }
            
            if (testLevelNum > 20)
            {
                int num;
                num = testLevelNum % 20;
                //Debug.Log("num::" + num);
            }
            go = Resources.Load("HideNSeekLevels/Level_" + testLevelNum)as GameObject;
            levelNumText.text = testLevelNum.ToString();

        }
        else
        {
            if (isBonusLevel)
            {
                //GameEnums.BonusLevelsUnlocked = bonusTestLevelNum;

                if (GameEnums.BonusLevelsUnlocked > 4)
                    GameEnums.BonusLevelsUnlocked = 1;
                go = Resources.Load("BonusLevels/BonusLevel_" + GameEnums.BonusLevelsUnlocked) as GameObject;
                levelNumText.text = "Bonus";

                timerVal = 20;
            }
            else
            {
                if (GameEnums.LevelsUnlocked > 20)//this condition enters when level number is greater than 20 and this means its in loop of level,
                {
                    levelLoopCount = GameEnums.LevelsUnlocked % 20;
                    levelLoopNum = GameEnums.LevelsUnlocked / 20;
                    
                    if(levelLoopCount == 0)//this condition is for level numbers like 40,60,80 etc.., 
                    {
                        //Debug.Log("level 20 load");
                        go = Resources.Load("HideNSeekLevels/Level_" + (levelLoopCount + 20)) as GameObject;
                    }
                    else//this condition is to load levels greater than 20 and not 40,60,80 etc.,
                    {
                        go = Resources.Load("HideNSeekLevels/Level_" + levelLoopCount) as GameObject;
                    }
                    levelNumText.text = ((levelLoopNum * 20) + levelLoopCount).ToString();
                }
                else//this condition is load levels below 20
                {
                    go = Resources.Load("HideNSeekLevels/Level_" + GameEnums.LevelsUnlocked) as GameObject;
                    //go = Resources.Load("HideNSeekLevels/Level_" + LevelSelection.hideNseekLevelNum) as GameObject;
                    go.transform.rotation = Quaternion.Euler(0, Random.Range(90, 180), 0);

                    levelNumText.text = GameEnums.LevelsUnlocked.ToString();
                   //levelNumText.text = "Level " + LevelSelection.hideNseekLevelNum;
                }
                
            }
        }
        GameObject myLevel;
        myLevel = Instantiate(go);
        myLevel.SetActive(true);
        playerCamera.transform.position = new Vector3(myLevel.transform.position.x, myLevel.transform.position.y + 35, myLevel.transform.position.z);
        //int[] angle = new int[] { 45, 90, 180 };
        //myLevel.transform.rotation = Quaternion.Euler(0, Random.Range(0,angle.Length), 0);
        myLevelData = myLevel.GetComponent<LevelData>();
        //if(!isBonusLevel)
        myLevelData.ApplyLevelEnvironmentColors();

        
    }
   
    //public void LoadPetEscapeLevels()
    //{
    //    GameObject go;
    //    Debug.Log("petrescuelevels::" + GameEnums.PetRescueLevels);
    //    //go = Instantiate(Resources.Load("PetRescueLevels/PetRescueLevel_" + GameEnums.PetRescueLevels)) as GameObject;
    //    //go = petEscapeLevels[GameEnums.PetRescueLevels - 1];
    //    go = petEscapeLevels[LevelSelection.PetEscapeLevelNum - 1];

    //    go.SetActive(true);
        
    //    playerPos = GameObject.Find("PlayerSpawnPos");
    //    Debug.Log("playerpos name::" + playerPos.name);
    //    levelNumText.text = "Level " + LevelSelection.PetEscapeLevelNum;
    //}

    IEnumerator StartBonusLevelGameplay(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        joystick.gameObject.SetActive(true);
        player.moveSpeed = 12;
        isStartTimer = true;
        MenuScript.isSeekGame = false;
        MenuScript.isHideGame = false;
        myGameType = GameType.None;
    }

    IEnumerator StartHideGameplayNow(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        player.moveSpeed = 8;
        timeToHide.text = "Time to Hide " + 3;
        yield return new WaitForSeconds(1);
        timeToHide.text = "Time to Hide " + 2;
        yield return new WaitForSeconds(1);
        timeToHide.text = "Time to Hide " + 1;
        yield return new WaitForSeconds(1);
        timeToHide.text = "GO";
        isStartTimer = true;
        //player.objectsToActivate[2].SetActive(true);
    }

    IEnumerator CameraZoom(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        cameraZoomIn = true;
        yield return new WaitForSeconds(3f);
        cameraZoomOut = true;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    private void Update()
    {
       // moveSpeedText.text = "MoveSpeed:" + player.moveSpeed + "Rotval::" + player.rotationSpeed + " horizontal::" + joystick.Horizontal + "  Vertical::" + joystick.Vertical;
        if (cameraZoomIn)
        {
            playerCamera.fieldOfView -= 30f * Time.deltaTime;
            if (playerCamera.fieldOfView <= 10)
            {
                playerCamera.fieldOfView = 10;
                cameraZoomIn = false;
            }
        }
        if (cameraZoomOut)
        {
            playerCamera.fieldOfView += 30f * Time.deltaTime;
            if (playerCamera.fieldOfView >= 60)
            {
                playerCamera.fieldOfView = 60;
                cameraZoomOut = false;
                //player.ActivatePlayerObjects();
                isStartTimer = true;
                isEnableJoystick = true;
                player.moveSpeed = 8;
            }
        }
        if (isStartTimer)
        {
            timerVal -= Time.deltaTime;
            timerText.text = timerVal.ToString("F0");
            if (timerVal <= 0)
            {
                isStartTimer = false;
                isEnableJoystick = false;
                timerVal = 0;

                if (MenuScript.isSeekGame)
                {
                    if (LevelTarget >= 4)
                        LoadLevelComplete();
                    else
                        LoadLevelFail();
                }
                else
                    LoadLevelComplete();
            }
        }
    }

    public void CheckLevelTarget()
    {
        LevelTarget++;
        SetLifeUI(LevelTarget - 1);
        //Debug.Log("check level target::" + LevelTarget);
        if (LevelTarget == 6 && !isLevelFailed)
        {
           // Debug.Log("level Complete");
            isStartTimer = false;
            isEnableJoystick = false;
            Invoke(nameof(LoadLevelComplete), 0.2f);
        }
    }
    public void LoadLevelComplete()
    {
        prelc.SetActive(true);
        player.moveSpeed = 0;
        player.SetInputToZero();
        
            
        joystick.gameObject.SetActive(false);
        isStartTimer = false;
        isEnableJoystick = false;
        player.petAnimCntrl.SetIdleAnim();
        for (int i = 0; i < aiPlayersList.Count; i++)
        {
            aiPlayersList[i].GetComponent<AIController>().StopAI();
        }

        Invoke(nameof(LoadLcWithDelay), 2); 
    }
    void LoadLcWithDelay()
    {
        prelc.SetActive(false);
        levelCompletePage.SetActive(true);
    }
    public void LoadLevelFail()
    {
        prelf.SetActive(true);
        if (timerVal <= 0)
        {
            prelf.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            prelf.transform.GetChild(1).gameObject.SetActive(true);
        }
        for (int i = 0; i < aiPlayersList.Count; i++)
        {
            aiPlayersList[i].GetComponent<AIController>().StopAI();
        }
        isLevelFailed = true;
        player.moveSpeed = 0;
        joystick.gameObject.SetActive(false);
        player.petAnimCntrl.SetIdleAnim();
        isStartTimer = false;
        isEnableJoystick = false;
       // Invoke(nameof(LoadlFWithDelay), 2f);
        StartCoroutine(LoadlFWithDelay(2f));
    }
    IEnumerator LoadlFWithDelay(float waittime)
    {
        // Debug.Log("Load level fail");
        yield return new WaitForSeconds(waittime);
        prelf.SetActive(false);
        levelFailPage.SetActive(true);
    }
    
    public void LF_RetryBtnClicked()
    {
        SceneManager.LoadScene("Menu");
    }
    IEnumerator StartCountdown(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        countDownText.gameObject.SetActive(true);
        countDownText.text = "3";
        yield return new WaitForSeconds(1);
        countDownText.text = "2";
        yield return new WaitForSeconds(1);
        countDownText.text = "1";
        yield return new WaitForSeconds(1);
        countDownText.text = "GO!";
        yield return new WaitForSeconds(1);
        countDownText.gameObject.SetActive(false);
        joystick.gameObject.SetActive(true);
        
        player.objectsToActivate[0].SetActive(true);
    }

    public void SetLifeUI(int count)
    {
        if (MenuScript.isSeekGame)
        {
            savedPetLifeObj[count].GetComponent<Image>().color = Color.green;
        }
        else if (MenuScript.isHideGame)
        {
            savedPetLifeObj[count].GetComponent<Image>().color = Color.red;
        }
    }
    public void ShowKeysUI()
    {
        for (int i = 0; i < GameEnums.KeysCount; i++)
        {
            // keysList[i].color = Color.white;
            keysList[i].GetComponent<Image>().sprite = yellowKeySpr;
        }
    }

    public void EnableFootPrints(Color clr)
    {
        player.petAnimCntrl.GetComponent<SetFootPrints>().enabled = true;
        player.petAnimCntrl.GetComponent<SetFootPrints>().footPrints.GetComponent<SpriteRenderer>().color = clr;
        Invoke(nameof(DisableFootPrints), 5f);
    }
    void DisableFootPrints()
    {
        player.petAnimCntrl.GetComponent<SetFootPrints>().enabled = false;
        footPrintsOn = false;
    }

    public void EnableCoinsEffect(GameObject obj)
    {
        GameObject go = Instantiate(coinsCollectingEffect);
        go.transform.position = obj.transform.position;
        go.transform.rotation = obj.transform.rotation;
    }
    public void EnableKeysEffect(GameObject obj)
    {
        GameObject go = Instantiate(keyCollectingEffect);
        go.transform.position = obj.transform.position;
        go.transform.rotation = obj.transform.rotation;
    }
    public void EnableSaveOtherPetsEffect(GameObject obj)
    {
        GameObject go = Instantiate(saveOtherPetsEffect);
        go.transform.position = obj.transform.position;
        go.transform.rotation = obj.transform.rotation;
    }

    public void GetExtraTimeSuccessEvent()
    {
        for (int i = 0; i < aiPlayersList.Count; i++)
        {
            if(!aiPlayersList[i].GetComponent<AIController>().isObjFound)
            aiPlayersList[i].GetComponent<AIController>().MoveAI();
        }
        timerVal = 15;
        player.moveSpeed = 8;
        joystick.gameObject.SetActive(true);
        isStartTimer = true;
        isEnableJoystick = true;
        if (isLevelFailed)
        {
            levelFailPage.SetActive(false);
            isLevelFailed = false;
        }
        else
        {
            levelCompletePage.SetActive(false);
        }
        
    }
    public void GetOutSuccessEvent()
    {
        StartCoroutine(StartCountdown(0.5f));
        player.moveSpeed = 8;
        player.cageModel.SetActive(false);
        aiSeeker.seekerFov.foundPlayer = false;
        aiSeeker.seekerFov.viewRadius = 0;
        aiSeeker.seekerFov.viewAngle = 0;
        joystick.gameObject.SetActive(true);
        isEnableJoystick = true;
        levelFailPage.SetActive(false);
        isLevelFailed = false;
        Invoke(nameof(GetOutDelay), 3.5f);
    }

    void GetOutDelay()
    {
        for (int i = 0; i < aiPlayersList.Count; i++)
        {
            if (!aiPlayersList[i].GetComponent<AIController>().isObjFound)
                aiPlayersList[i].GetComponent<AIController>().MoveAI();
        }
        isStartTimer = true;
        aiSeeker.seekerFov.viewRadius = 6;
        aiSeeker.seekerFov.viewAngle = 90;
    }

    public void ShowPetRescueLC()
    {
        petRescue_lcPage.SetActive(true);
        joystick.gameObject.SetActive(false);
        isStartTimer = false;
        isEnableJoystick = false;
        player.moveSpeed = 0;
    }

    public void PauseBtnClicked()
    {
        Time.timeScale = 0;
        pausePage.SetActive(true);
    }

    public void Pause_HomeBtnClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    #region Start Invisible Popup

    public void ShowStartInvisiblePopup()
    {
        startInvisiblePopup.SetActive(true);
        Time.timeScale = 0;
    }
    public void StartInvisibleVideoBtn()
    {
        startInvisiblePopup.SetActive(false);
        isPlayerInvicible = true;
        Time.timeScale = 1;
        //  player.petAnimCntrl.GetComponent<PetAnimController>().SetBodyTransparent();
        for (int i = 0; i < player.petAnimCntrl.petBody.Count; i++)
        {
            if (player.petAnimCntrl.petBody[i].activeInHierarchy)
                player.petAnimCntrl.petBody[i].GetComponent<SetPetTransparent>().MakeMatTransparent();
        }

    }

    public void StartInvisible_NoThanksBtn()
    {
        startInvisiblePopup.SetActive(false);
        isPlayerInvicible = false;
        Time.timeScale = 1;
    }

    #endregion


    #region Powerup Button Click Events
    GameObject selectedPowerupBtn;
    void DisablePowerupFillImg()
    {
        selectedPowerupBtn.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void RunFasterPowerup()
    {
        if(GameEnums.RunFasterPowerup > 0)
        {
            selectedPowerupBtn = EventSystem.current.currentSelectedGameObject;
            selectedPowerupBtn.transform.GetChild(0).gameObject.SetActive(true);
            Invoke(nameof(DisablePowerupFillImg), 10f);

            player.RunFaster();
            GameEnums.RunFasterPowerup -= 1;
            speedRunningCount.text = "" + GameEnums.RunFasterPowerup;
        }
    }

    public void GoThroughWallsPowerup()
    {
        if(GameEnums.GoThroughWallsPowerup > 0)
        {
            selectedPowerupBtn = EventSystem.current.currentSelectedGameObject;
            selectedPowerupBtn.transform.GetChild(0).gameObject.SetActive(true);
            Invoke(nameof(DisablePowerupFillImg), 10f);

            myLevelData.EnbleGoThroughWalls();
            GameEnums.GoThroughWallsPowerup -= 1;
            goThroughWallsCount.text = "" + GameEnums.GoThroughWallsPowerup;
        }
    }
    public void XRayPowerup()
    {
        if(GameEnums.XRayPowerup > 0)
        {
            selectedPowerupBtn = EventSystem.current.currentSelectedGameObject;
            selectedPowerupBtn.transform.GetChild(0).gameObject.SetActive(true);
            Invoke(nameof(DisablePowerupFillImg), 10f);

            myLevelData.EnableXRayVision();
            GameEnums.XRayPowerup -= 1;
            xRayCount.text = "" + GameEnums.XRayPowerup;
        }
    }
    public void GoInvisiblePowerup()
    {
        if(GameEnums.GoInvisiblePowerup > 0)
        {
            selectedPowerupBtn = EventSystem.current.currentSelectedGameObject;
            selectedPowerupBtn.transform.GetChild(0).gameObject.SetActive(true);
            Invoke(nameof(DisablePowerupFillImg), 10f);

            isPlayerInvicible = true;
            GameEnums.GoInvisiblePowerup -= 1;
            invisibleCount.text = "" + GameEnums.GoInvisiblePowerup;
            //player.petAnimCntrl.GetComponent<PetAnimController>().SetBodyTransparent();
            for (int i = 0; i < player.petAnimCntrl.petBody.Count; i++)
            {
                if(player.petAnimCntrl.petBody[i].activeInHierarchy)
                player.petAnimCntrl.petBody[i].GetComponent<SetPetTransparent>().MakeMatTransparent();
            }
            
        }
    }

    #endregion

    void ShowPowerUpCounts()
    {
        speedRunningCount.text = "" + GameEnums.RunFasterPowerup;
        invisibleCount.text = "" + GameEnums.GoInvisiblePowerup;
        xRayCount.text = "" + GameEnums.XRayPowerup;
        goThroughWallsCount.text = "" + GameEnums.GoThroughWallsPowerup;
    }
}
