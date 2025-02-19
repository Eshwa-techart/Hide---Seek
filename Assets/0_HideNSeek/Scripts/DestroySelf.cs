using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{

    [SerializeField] float delay;
    void Start()
    {
        DestroyObj();
    }
    private void DestroyObj()
    {
        Destroy(gameObject, delay);
    }

}
