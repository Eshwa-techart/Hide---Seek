using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCoins : MonoBehaviour
{
    [SerializeField]
    Text txt;
    private void Update()
    {
        txt.text = GameEnums.TotalCoins.ToString();
    }
}
