using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetRescueLevelData : MonoBehaviour
{
    //public GameObject[] aiCharacter;

    //public List<PetRescueAiList> petRescueAi;

    public Material floorMat, wallsMat, wallDepthMat, outerFloorMat;
    public Color floorMatColor, wallsMatColor, wallDepthMatColor, outerFloorMatColor;

    void Start()
    {
        //SpawnAI();
        AssignColorsToMaterials();
    }
    void AssignColorsToMaterials()
    {
        floorMat.color = floorMatColor;
        wallsMat.color = wallsMatColor;
        wallDepthMat.color = wallDepthMatColor;
        outerFloorMat.color = outerFloorMatColor;
    }



    //void SpawnAI()
    //{
    //    for (int i = 0; i < petRescueAi.Count; i++)
    //    {
    //        GameObject go = Instantiate(aiCharacter[Random.Range(0, aiCharacter.Length)]);
    //        go.transform.localPosition = petRescueAi[i].aiSpawnPos.transform.position;
    //        go.transform.rotation = petRescueAi[i].aiSpawnPos.transform.rotation;

    //        if (petRescueAi[i].isMove)
    //        {
               
    //            go.transform.GetChild(0).transform.GetComponent<PetRescueAI>().onlyRotateChar = false;
    //            go.transform.GetChild(0).transform.GetComponent<PetRescueAI>().moveAi = true;

    //            if (petRescueAi[i].is2Points)
    //            {
    //                go.transform.GetChild(0).transform.GetComponent<PetRescueAI>().is2Points = true;
    //            }

    //        }
    //        else
    //        {
    //            go.transform.GetChild(0).transform.GetComponent<PetRescueAI>().onlyRotateChar = true;
               
    //            go.transform.GetChild(0).transform.GetComponent<PetRescueAI>().is2Points = false;
    //            go.transform.GetChild(0).transform.GetComponent<PetRescueAI>().moveAi = false;
    //            go.transform.GetChild(0).transform.GetComponent<PetRescueAI>().angle1 = petRescueAi[i].angle1;
    //            go.transform.GetChild(0).transform.GetComponent<PetRescueAI>().angle2 = petRescueAi[i].angle2;
    //        }
    //    }
        
    //}

   
}


//[System.Serializable]
//public struct PetRescueAiList
//{
//    public GameObject aiSpawnPos;
   
//    public bool isMove;
//    public bool is2Points;
//    [Header("Rotate AI")]
//    public bool isRotate;
//    public float angle1, angle2;
//}



