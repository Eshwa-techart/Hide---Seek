using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MenteBacata.ScivoloCharacterControllerDemo;


public class PlayerTrigger : MonoBehaviour
{

    bool keyCollected;
    [SerializeField]
    PlayerController player;
    
    

    void Start()
    {

    }

    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log("player trigger works fine"+other .gameObject.tag);
        if (other.gameObject.CompareTag("Opponent"))
        {
           // Debug.Log("Triggered with Opponent::" + other.gameObject.name);
            if (MenuScript.isSeekGame && GameController.instance.isStartTimer)
            {
                other.gameObject.GetComponent<AIController>().EnableMesh();
                other.gameObject.GetComponent<AIController>().isObjFound = true;
                other.gameObject.GetComponent<AIController>().StopAI();
            }
            else if (MenuScript.isHideGame)
            {
                if (other.gameObject.GetComponent<AIController>().isObjFound)
                {
                    Vibration.Vibrate(50);
                    GameController.instance.savedLifeCount++;
                    //Debug.Log("player released other player::" + GameController.instance.savedLifeCount);
                    GameController.instance.petArrestedcount--;
                    GameController.instance.savedPetLifeObj[GameController.instance.petArrestedcount].GetComponent<Image>().color = Color.white;
                    other.gameObject.GetComponent<AIController>().cageObj.SetActive(false);
                    other.gameObject.GetComponent<AIController>().helpMeObj.SetActive(false);
                    GameController.instance.EnableSaveOtherPetsEffect(other.gameObject);

                }
                other.gameObject.GetComponent<AIController>().isObjFound = false;
                other.gameObject.GetComponent<AIController>().MoveAI();
            }
        }
        if (other.gameObject.CompareTag("Coins"))
        {
            //  Debug.Log("coins::" + GameController.instance.CoinsVal);
            Vibration.Vibrate(50);

            GameController.instance.CoinsVal = other.gameObject.GetComponent<RotateCoins>().coinVal;
            GameController.instance.coinsCollectedInLevel += GameController.instance.CoinsVal;

            GameController.instance.EnableCoinsEffect(other.gameObject);
//            Debug.Log("coins::" + GameController.instance.coinsCollectedInLevel);
            GameEnums.TotalCoins += GameController.instance.CoinsVal;
            GameController.instance.coinsText.text = "" + GameController.instance.coinsCollectedInLevel;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Teleporter"))
        {
            if (!GameController.instance.player.isTeleported)
            {
                GameController.instance.player.isTeleported = true;
                if (other.gameObject.name == "Teleport_1")
                {
                    other.gameObject.GetComponent<Teleporter>().teleportToObj.tag = "Untagged";
                    GameController.instance.player.gameObject.transform.SetPositionAndRotation(other.gameObject.GetComponent<Teleporter>().teleportToObj.transform.GetChild(0).transform.position,
                        other.gameObject.GetComponent<Teleporter>().teleportToObj.transform.rotation);
                   
                }
                else if (other.gameObject.name == "Teleport_2")
                {
                    other.gameObject.GetComponent<Teleporter>().teleportToObj.tag = "Untagged";
                    GameController.instance.player.gameObject.transform.SetPositionAndRotation(other.gameObject.GetComponent<Teleporter>().teleportToObj.transform.GetChild(0).transform.position,
                        other.gameObject.GetComponent<Teleporter>().teleportToObj.transform.rotation);
                }
                else
                {
                    Debug.LogError("Teleporter name is not correct. Please check the spelling of teleporter");
                }
            }
        }
        if (other.gameObject.CompareTag("2xRun"))
        {
            Vibration.Vibrate(50);
            Destroy(other.gameObject);
            GameController.instance.player.RunFaster();
        }
        if (other.gameObject.CompareTag("Key"))
        {
            Vibration.Vibrate(50);
            if (!keyCollected && GameEnums.KeysCount < 3)
            {
              //  Debug.Log("Key collected");
                Destroy(other.gameObject);
                GameEnums.KeysCount += 1;
                GameController.instance.EnableKeysEffect(other.gameObject);
                GameController.instance.ShowKeysUI();
                keyCollected = true;
            }
           
        }
        if (other.gameObject.CompareTag("Water"))
        {
            if(!GameController.instance.footPrintsOn)
            GameController.instance.footPrintsOn = true;
            GameController.instance.EnableFootPrints(other.gameObject.GetComponent<Renderer>().material.color);
        }
        if (other.gameObject.CompareTag("LevelCompleteTrigger"))
        {
            Invoke(nameof(ShowPetRescueLCWithDelay), 0.5f);
        }

        if (other.gameObject.name == "ViewVisualization")
        {
            //Debug.Log("11111111111player found");
            player.moveSpeed = 0;
            player.petAnimCntrl.SetIdleAnim();
        }
        
    }

    void ShowPetRescueLCWithDelay()
    {
        GameController.instance.ShowPetRescueLC();
    }
}