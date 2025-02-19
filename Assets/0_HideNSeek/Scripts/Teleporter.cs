using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject teleportToObj;

    public GameObject teleportInnerRing;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameController.instance.player.isTeleported = false;
            gameObject.tag = "Teleporter";
        }
    }

    private void Update()
    {
        teleportInnerRing.transform.Rotate(0,0, 60 * Time.deltaTime);
    }

}
