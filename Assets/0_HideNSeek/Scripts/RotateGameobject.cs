using UnityEngine;

public class RotateGameobject : MonoBehaviour
{
	[SerializeField]
	float rotationSpeed = 5f;

	void OnMouseDrag()
	{
		float XaxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
		//float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSpeed;
		
		transform.Rotate(Vector3.down, XaxisRotation);
		//transform.RotateAround(Vector3.right, YaxisRotation);
	}
}