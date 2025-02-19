using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetPetTransparent : MonoBehaviour
{
    [SerializeField]
    Material myMaterial;

    [SerializeField]
    Material transparentMat;

    [SerializeField]
    Material[] allmat_Body;
    Renderer rend_body;

    Outline outline;
    void OnEnable()
    {
        //outline = GetComponent<Outline>();
        rend_body = gameObject.GetComponent<Renderer>();
        myMaterial = rend_body.material;
        //allmat_Body = rend_body.materials;

        //Invoke(nameof(MakeMatTransparent), 3f);
    }
    public void MakeMatTransparent()
    {
        //for (int i = 0; i < allmat_Body.Length; i++)
        //{
        //    allmat_Body[i] = transparentMat;
        //}

        //rend_body.materials = allmat_Body;
        //outline.enabled = false;
        //outline.enabled = true;
        rend_body.material = transparentMat;
        Invoke(nameof(MakeMatNormal), 10);
    }
    void MakeMatNormal()
    {
        //for (int i = 0; i < allmat_Body.Length; i++)
        //{
        //    allmat_Body[i] = myMaterials[i];
        //}

        //rend_body.materials = allmat_Body;
        rend_body.material = myMaterial;
        GameController.instance.isPlayerInvicible = false;
    }
    
    
}
