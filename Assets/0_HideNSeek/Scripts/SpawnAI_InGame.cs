using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAI_InGame : MonoBehaviour
{
    public GameObject aiCharacterPrefab;
    public GameObject aiSkeerPrefab;
    [SerializeField]
    List<GameObject> aiSpawnPos;
    [SerializeField]
    GameObject seekerAiPos;
    void Start()
    {
        SpawnAi();
    }

    void SpawnAi()
    {
        for (int i = 0; i < aiSpawnPos.Count; i++)
        {
            GameObject go;
            go = Instantiate(aiCharacterPrefab, aiSpawnPos[i].transform.position, aiSpawnPos[i].transform.rotation);
            GameController.instance.aiPlayersList.Add(go);
        }

        if (MenuScript.isHideGame && !GameController.isBonusLevel)
        {
            GameObject seekerObj;
            seekerObj = Instantiate(aiSkeerPrefab, seekerAiPos.transform.position, seekerAiPos.transform.rotation);
            GameController.instance.aiPlayersList.Add(seekerObj);
            GameController.instance.aiSeeker = seekerObj.GetComponent<AIController>();
        }
        
    }
}
