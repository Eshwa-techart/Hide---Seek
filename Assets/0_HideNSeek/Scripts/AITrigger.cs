using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITrigger : MonoBehaviour
{

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Opponent"))
        {
            if (other.gameObject.GetComponent<AIController>().isObjFound)
            {
                //Debug.Log("Set Free the AI");
            }
        }
    }
}
