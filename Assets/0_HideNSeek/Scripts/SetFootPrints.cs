using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFootPrints : MonoBehaviour
{
    public GameObject footPrints;
    float totalTime;
    private void Update()
    {
        if (GameController.instance.player.moveSpeed != 0)
        {
            totalTime += Time.deltaTime;
            if (totalTime >= 0.2f)
            {
                Instantiate(footPrints, new Vector3(transform.position.x, transform.position.y + 0.85f, transform.position.z), Quaternion.Euler(90, 0, 0));
                totalTime = 0;
            }
        }
    }
}
