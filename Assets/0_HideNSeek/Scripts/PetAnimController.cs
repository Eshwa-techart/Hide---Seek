using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetAnimController : MonoBehaviour
{

    public AnimationClip runAnim, idleAnim;
    public Animation charAnimCntrl;

    public List<GameObject> petBody;
    public Material normalMat, transparentMat;
    
    void Start()
    {

       
    }
    
    public void SetRunAnim()
    {
        charAnimCntrl.Play(runAnim.name);
    }
    public void SetIdleAnim()
    {
        charAnimCntrl.Play(idleAnim.name);
    }
    public void SetBodyTransparent()
    {
        for (int i = 0; i < petBody.Count; i++)
        {
            petBody[i].GetComponent<SkinnedMeshRenderer>().material = transparentMat;

        }
        if (GameController.instance.enableXRayVision)
        {
            Invoke(nameof(EnablePetBodyMesh), 10f);
        }
        else
        {
            StartCoroutine(ChangeBodyMatToNormal(10f));
        }

    }

    public void DisablePetBodyMesh()
    {
        for (int i = 0; i < petBody.Count; i++)
        {
            petBody[i].GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
    }
    public void EnablePetBodyMesh()
    {
        for(int i = 0; i < petBody.Count; i++)
        {
            petBody[i].GetComponent<SkinnedMeshRenderer>().enabled = true;
        }
    }
    public void SetBodyNormal()
    {
        for (int i = 0; i < petBody.Count; i++)
        {
            petBody[i].GetComponent<SkinnedMeshRenderer>().material = normalMat;
        }
    }
    

    IEnumerator ChangeBodyMatToNormal(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        SetBodyNormal();
        yield return new WaitForSeconds(0.3f);
        SetBodyTransparent();
        yield return new WaitForSeconds(0.3f);
        SetBodyNormal();
        yield return new WaitForSeconds(0.3f);
        SetBodyTransparent();
        yield return new WaitForSeconds(0.3f);
        SetBodyNormal();
        yield return new WaitForSeconds(0.3f);
        SetBodyTransparent();
        yield return new WaitForSeconds(0.3f);
        SetBodyNormal();
        //MenuScript.isPlayerInvicible = false;
        GameController.instance.isPlayerInvicible = false;
        StopCoroutine(nameof(ChangeBodyMatToNormal));
    }
}
