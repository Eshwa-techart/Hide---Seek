using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{

    [SerializeField]
    GameObject buttonObj;
    [SerializeField]
    Material buttonMat;

    [SerializeField]
    GameObject[] gateObj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            buttonObj.GetComponent<Renderer>().material = buttonMat;
            for (int i = 0; i < gateObj.Length; i++)
            {
                gateObj[i].SetActive(false);
            }
        }
    }
}
