using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsMove : MonoBehaviour
{
    public List<GameObject> coinsList = new List<GameObject>();
    public GameObject coinsDestinationPos;
    public List<Vector3> coinsInitialPos = new List<Vector3>();
    private void OnEnable()
    {
        StartCoroutine(SpawnCoins(0));

        StartCoroutine(MoveCoins(0.1f));
        Invoke("DestroyAllCoins", 3.0f);
    }

    IEnumerator SpawnCoins(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
        for (int i = 0; i < coinsList.Count; i++)
        {
            coinsList[i].SetActive(true);
            coinsInitialPos.Add(coinsList[i].transform.position);
            yield return new WaitForSeconds(0.02f);

        }
    }
    public Transform[] waypoints;
    public IEnumerator MoveCoins(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        for (int i = 0; i < coinsList.Count; i++)
        {
            iTween.MoveTo(coinsList[i].gameObject,iTween.Hash("path",waypoints,"time",1.5f,"delay", (0.01f + 0.05f) * i, "easetype", iTween.EaseType.easeInBack));
        }
       // StartCoroutine(EnableCoinSound(1f));
        
    }
    IEnumerator EnableCoinSound(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        for (int i = 0; i < coinsList.Count; i++)
        {
            coinsList[i].GetComponent<AudioSource>().enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }
    void DestroyAllCoins()
    {
        for (int i = 0; i < coinsList.Count; i++)
        {
            coinsList[i].transform.position = coinsInitialPos[i];
            coinsList[i].GetComponent<AudioSource>().enabled = false;
            coinsList[i].SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
