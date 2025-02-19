using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetRescueFood : MonoBehaviour
{
    public List<Rigidbody> bricksList;
    public GameObject particleEffect;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < bricksList.Count; i++)
            {
                bricksList[i].isKinematic = false;
            }
            GameObject pe = Instantiate(particleEffect);
            //pe.transform.SetParent(gameObject.transform);
            pe.transform.position = gameObject.transform.position;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            //pe.transform.parent = null;
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
