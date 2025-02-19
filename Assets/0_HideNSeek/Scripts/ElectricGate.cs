using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricGate : MonoBehaviour
{
    [SerializeField]
    GameObject[] electricGateObj;

    [SerializeField]
    Material mat;

    [SerializeField]
    GameObject buttonObj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            buttonObj.GetComponent<Renderer>().material = mat;
            for (int i = 0; i < electricGateObj.Length; i++)
            {
                electricGateObj[i].SetActive(false);
            }
        }
    }
}
