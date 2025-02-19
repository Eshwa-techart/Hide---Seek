using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LookAtCamera : MonoBehaviour
{
    Transform target;
    float textScaleVal = 0.15f;

    [SerializeField]
    bool isHelpMeObj;
    float helpMeSize = 0.3f;

    [SerializeField]
    List<string> petNameList;
    string randName;
    [SerializeField] bool isPlayer;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("MainCamera").transform;

        if (isHelpMeObj)
        {
            gameObject.transform.GetChild(0).transform.localScale = new Vector3(-helpMeSize, helpMeSize, helpMeSize);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-textScaleVal, textScaleVal, textScaleVal);
        }


        if (isPlayer)
        {
            gameObject.transform.localScale = new Vector3(-textScaleVal, textScaleVal, textScaleVal);
            //randName = petNameList[Random.Range(0, petNameList.Count)];
            gameObject.GetComponent<TextMeshPro>().text = "" + GameEnums.PetName;
        }
    }

   
    void Update()
    {
        transform.LookAt(target);
    }
}
