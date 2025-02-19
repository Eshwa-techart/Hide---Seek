using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCoins : MonoBehaviour
{
    public int coinVal;
    [SerializeField] bool isKey;
    [SerializeField] bool isGiftBox;
    

    GameObject petFoodIns;

    private void OnEnable()
    {
        if (!isKey && !isGiftBox)
        {
            GetComponent<MeshRenderer>().enabled = false;
            EnableFood();
        }
    }
    void EnableFood()
    {
        if (GameController.instance.myPetType == GameController.PetType.cat)
        {
            petFoodIns = Instantiate(Resources.Load("PetFoodPrefabs/FishBone")) as GameObject;
        }
        else if (GameController.instance.myPetType == GameController.PetType.dog)
        {
            petFoodIns = Instantiate(Resources.Load("PetFoodPrefabs/DogBone")) as GameObject;
        }
        else if(GameController.instance.myPetType == GameController.PetType.monkey)
        {
            petFoodIns = Instantiate(Resources.Load("PetFoodPrefabs/Banana")) as GameObject;
        }
        else if(GameController.instance.myPetType == GameController.PetType.panda)
        {
            petFoodIns = Instantiate(Resources.Load("PetFoodPrefabs/Bamboo")) as GameObject;
        }
        else if(GameController.instance.myPetType == GameController.PetType.pig)
        {
            petFoodIns = Instantiate(Resources.Load("PetFoodPrefabs/Fishbone")) as GameObject;
        }
        else if(GameController.instance.myPetType == GameController.PetType.rabbit)
        {
            petFoodIns = Instantiate(Resources.Load("PetFoodPrefabs/Carrot")) as GameObject;
        }
        petFoodIns.transform.SetParent(gameObject.transform);
        petFoodIns.transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
        petFoodIns.transform.localScale = Vector3.one;
    }
   
}
