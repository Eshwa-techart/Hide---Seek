using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.AI;

public class LevelData : MonoBehaviour
{

    public GameObject playerSpawnPos, OtherPlayersSpawnPos;
    

    [SerializeField]
    Color ambientColor;

    [Header("Fog Variables")]
    [SerializeField]
    Color fogColor;
    [SerializeField]
    int fogStartVal;
    [SerializeField]
    int fogEndVal;
    [SerializeField]
    Material verticalFogMat;
    [SerializeField]
    GameObject verticalFogObj;
    [SerializeField]
    Vector3 verticalFogPosition;
    [SerializeField]
    int fogYScaleValue;

    [Space(20)]

    [SerializeField]
    Color cameraBgSolidColor;

    [Space(20)]

    [Header("Environment Variables")]
    public GameObject levelEnvironment;
    [SerializeField] int environmentNum;
    [SerializeField]
    Vector3 levelEnvironmentPosition;
    [SerializeField]
    Material environmentMaterial;
    [SerializeField]
    Color environmentMatColor;


    public Material baseMat, wallsMat, base2Mat;
    public Color xRayColor;

    public List<BoxCollider> WallsColliderList;


    private void Awake()
    {
        if(!GameController.isBonusLevel)
        ApplyLevelOrgColors();
    }
    private void Start()
    {
        //Invoke(nameof(ChangeXRayMaterial), 8f); 
    }
    public void ApplyLevelEnvironmentColors()
    {
        GameController.instance.playerCamera.backgroundColor = cameraBgSolidColor;

        SpawnVerticalFog();
       
        RenderSettings.ambientSkyColor = ambientColor;
        SpawnLevelEnvironment();
    }

    void SpawnVerticalFog()
    {
        GameObject verticalFog = Instantiate(verticalFogObj);
        verticalFog.transform.position = verticalFogPosition;
        verticalFogMat.SetColor("_FogColor", fogColor);
        verticalFog.transform.localScale = new Vector3(verticalFog.transform.localScale.x, fogYScaleValue, verticalFog.transform.localScale.z);
    }


    void SpawnLevelEnvironment()
    {
        levelEnvironment = Resources.Load("EnvironmentPrefabs/Environment_" + environmentNum)as GameObject;

        GameObject myEnv = Instantiate(levelEnvironment);
        myEnv.transform.position = levelEnvironmentPosition;
        myEnv.SetActive(true);

        if(environmentMaterial != null)
        environmentMaterial.color = environmentMatColor;
       
    }

    [SerializeField]
    Color baseOrgMatColor;
    [SerializeField]
    Color base2OrgMatColor;
    [SerializeField]
    Color wallsOrgMatColor;
    [SerializeField]
    Color environmentOrgMatColor;
    [SerializeField]
    Color forOrgMatColor;

   
    
    public void EnableXRayVision()
    {
        if (GameController.instance.myGameType != GameController.GameType.Hide || !MenuScript.isHideGame)
        {
            baseMat.color = Color.grey;
            base2Mat.color = Color.grey;
            wallsMat.color = Color.grey;
            wallsMat.DisableKeyword("_EMISSION");
            wallsMat.SetColor("_EmissionColor", Color.grey);

            environmentMaterial.color = Color.grey;
            environmentMaterial.DisableKeyword("_EMISSION");
            environmentMaterial.SetColor("_EmissionColor", Color.grey);

            verticalFogMat.SetColor("_FogColor", xRayColor);
            GameController.instance.xRayImage.SetActive(true);
            GameController.instance.enableXRayVision = true;
            XrayModeOn();
            Invoke(nameof(ApplyLevelOrgColors), 10f);
        }

    }


    public void ApplyLevelOrgColors()
    {
        //Debug.Log("apply level original colors");
        baseMat.color = baseOrgMatColor;
        base2Mat.color = base2OrgMatColor;
        wallsMat.color = wallsOrgMatColor;
        environmentMaterial.color = environmentOrgMatColor;
        verticalFogMat.SetColor("_FogColor", forOrgMatColor);
        GameController.instance.xRayImage.SetActive(false);
        GameController.instance.enableXRayVision = false;
        for (int i = 0; i < GameController.instance.aiPlayersList.Count; i++)
        {

            if (!GameController.instance.aiPlayersList[i].GetComponent<AIController>().isObjFound)
            {
                GameController.instance.aiPlayersList[i].GetComponent<AIController>().gameobjectMesh.SetActive(false);
                GameController.instance.aiPlayersList[i].GetComponent<AIController>().petAnimCntrl.SetRunAnim();
            }
        }
    }

    void XrayModeOn()
    {
        //.Log("XmodeON Noww");
        for (int i = 0; i < GameController.instance.aiPlayersList.Count; i++)
        {
            GameController.instance.aiPlayersList[i].GetComponent<AIController>().gameobjectMesh.SetActive(true);

            if (!GameController.instance.aiPlayersList[i].GetComponent<AIController>().isObjFound)
            {
                GameController.instance.aiPlayersList[i].GetComponent<AIController>().petAnimCntrl.SetBodyTransparent();
                GameController.instance.aiPlayersList[i].GetComponent<AIController>().petAnimCntrl.SetRunAnim();
            }
        }
    }

    public void DisableGoThroughWalls()
    {
        for (int i = 0; i < WallsColliderList.Count; i++)
        {
            WallsColliderList[i].enabled = true;
        }
    }

    public void EnbleGoThroughWalls()
    {
        for (int i = 0; i < WallsColliderList.Count; i++)
        {
            WallsColliderList[i].enabled = false;
        }

        Invoke(nameof(DisableGoThroughWalls), 10f);
    }
}
