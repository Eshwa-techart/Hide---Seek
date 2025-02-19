using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSeekerTrigger : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Opponent"))
        {
            //Debug.Log("ai triggered");
            GameController.instance.petArrestedcount++;
            other.gameObject.GetComponent<AIController>().isObjFound = true;
            other.gameObject.GetComponent<AIController>().StopAI();
            other.gameObject.GetComponent<AIController>().DisableMesh();
            GameController.instance.SetLifeUI(GameController.instance.petArrestedcount - 1);
        }
        if (other.gameObject.CompareTag("Player"))
        {
           // Debug.Log("Player triggered, level fail");
            GameController.instance.player.petAnimCntrl.SetIdleAnim();
            GameController.instance.player.moveSpeed = 0;
            GameController.instance.player.cageModel.SetActive(true);
            GameController.instance.LoadLevelFail();
        }
    }
}
