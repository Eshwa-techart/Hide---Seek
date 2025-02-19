using UnityEngine;
using System.Collections;

public class RotateSample : MonoBehaviour
{	
	void Start(){
		//iTween.RotateBy(gameObject, iTween.Hash("x", .25, "easeType", "easeInOutBack", "loopType", "pingPong", "delay", .4));
	}
	Vector3 val;
	float diff = 0.1f;
	public void AddScale()
    {
		val = new Vector3(transform.localScale.x + diff, transform.localScale.y + diff, transform.localScale.z + diff);
		iTween.ScaleTo(gameObject, iTween.Hash("x", val.x + diff, "y", val.y + diff, "z", val.z + diff, "time", 0.1));
		iTween.ScaleTo(gameObject, iTween.Hash("x", val.x, "y", val.y, "z", val.z, "time", 0.1, "delay", 0.1f));
	}
	public void SubtractScale()
    {
		val = new Vector3(transform.localScale.x - diff, transform.localScale.y - diff, transform.localScale.z - diff);
		iTween.ScaleTo(gameObject, iTween.Hash("x", val.x + (diff + 0.1f), "y", val.y + (diff + 0.1f), "z", val.z + (diff + 0.1f), "time", 0.1));
		iTween.ScaleTo(gameObject, iTween.Hash("x", val.x, "y", val.y, "z", val.z, "time", 0.1, "delay", 0.1f));
	}
}

