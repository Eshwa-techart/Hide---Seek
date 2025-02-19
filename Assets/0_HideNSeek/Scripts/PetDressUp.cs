using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetDressUp : MonoBehaviour
{

    
    public List<GameObject> hatsList;
    public List<GameObject> dogHatsList;

    public List<GameObject> spectsList;
    public List<GameObject> dogSpectsList;

    public List<GameObject> shirtsList;

    public List<GameObject> pantsList;

    public List<GameObject> shoesList;

    public AnimationClip idleAnim, otherAnim;
    public Animation charAnimCntrl;
    public PetAnimController myPetAnimCntrl;
    void OnEnable()
    {
        
        myPetAnimCntrl = GetComponent<PetAnimController>();
        Debug.Log("petindex::" + CharacterSelection.petIndex);


        Invoke(nameof(EnableSelectedDress), 1f);
        //EnableSelectedDress();

        //if(!GameController.instance)
        //SetIdleAnim();
    }

    void EnableSelectedDress()
    {

        for (int i = 0; i < hatsList.Count; i++)
        {
            hatsList[i].SetActive(false);
        }
        if(PlayerPrefs.GetString("HatSelected") == "true")
        {
            if(hatsList.Count > 0)
            hatsList[MenuScript.hatvalueArray[CharacterSelection.petIndex]].SetActive(true);
            if(dogHatsList.Count > 0)
            dogHatsList[MenuScript.hatvalueArray[CharacterSelection.petIndex]].SetActive(true);
        }

        for (int i = 0; i < spectsList.Count; i++)
        {
            spectsList[i].SetActive(false);
        }
        if (PlayerPrefs.GetString("SpectsSelected") == "true")
        {
            if(spectsList.Count > 0)
            spectsList[MenuScript.spectsvalueArray[CharacterSelection.petIndex]].SetActive(true);
            if(dogSpectsList.Count > 0)
            dogSpectsList[MenuScript.spectsvalueArray[CharacterSelection.petIndex]].SetActive(true);
        }

        for (int i = 0; i < shirtsList.Count; i++)
        {
            shirtsList[i].SetActive(false);
        }
        shirtsList[MenuScript.shirtsvalueArray[CharacterSelection.petIndex]].SetActive(true);

        for (int i = 0; i < pantsList.Count; i++)
        {
            pantsList[i].SetActive(false);
        }
        pantsList[MenuScript.pantsvalueArray[CharacterSelection.petIndex]].SetActive(true);

        for (int i = 0; i < shoesList.Count; i++)
        {
            shoesList[i].SetActive(false);
        }
        shoesList[MenuScript.shoesvalueArray[CharacterSelection.petIndex]].SetActive(true);

        if (GameController.instance)
        {
            //playerPetsList[CharacterSelection.petIndex]
            GameObject go;
            go = GameController.instance.player.playerPetsList[CharacterSelection.petIndex];
            int childCount;
            childCount = go.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                myPetAnimCntrl.petBody.Add(go.transform.GetChild(i).gameObject);
            }

            myPetAnimCntrl.petBody.Add(hatsList[GameEnums.SelectedHats]);
            myPetAnimCntrl.petBody.Add(dogHatsList[GameEnums.SelectedHats]);
            myPetAnimCntrl.petBody.Add(spectsList[GameEnums.SelectedSpects]);
            myPetAnimCntrl.petBody.Add(dogSpectsList[GameEnums.SelectedSpects]);
            myPetAnimCntrl.petBody.Add(shirtsList[GameEnums.SelectedShirts]);
            myPetAnimCntrl.petBody.Add(pantsList[GameEnums.SelectedPants]);
            myPetAnimCntrl.petBody.Add(shoesList[GameEnums.SelectedShoes]);
        }

    }

    public void SetOtherAnim()
    {
        charAnimCntrl.Play(otherAnim.name);
        Invoke(nameof(SetIdleAnim), 2.2f);
    }
    public void SetIdleAnim()
    {
        charAnimCntrl.Play(idleAnim.name);
        Invoke(nameof(SetOtherAnim), 3f);
    }



    public static int[] wheelvalueArray;
    string wheelPurchasedIndex;
    public void SetWheelsPlayerprefs()
    {
        wheelvalueArray = PlayerPrefsX.GetIntArray(wheelPurchasedIndex, 0, 20);
        PlayerPrefsX.SetIntArray(wheelPurchasedIndex, wheelvalueArray);

        for (int i = 0; i <= 19; i++)
        {
             Debug.LogError("window tint values mohith " + i + " " + wheelvalueArray[i]);
        }
    }
}
