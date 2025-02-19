using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    public float speed;
    public float rotationSpeed;

    public FloatingJoystick joystick;
    float horizontalInput;
    float verticalInput;
    Vector3 movementDirection;
    Quaternion toRotation;


    private void Start()
    {
        joystick.gameObject.SetActive(true);
        if (!GameController.instance.isShowJoystick)
        {
            joystick.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
            joystick.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
        }
    }

    void Update()
    {
        MovePlayer();
    }
    /// <summary>
    /// Player Movement 
    /// </summary>
    void MovePlayer()
    {

#if UNITY_EDITOR
        //horizontalInput = Input.GetAxis("Horizontal");
        //  verticalInput = Input.GetAxis("Vertical");

//#elif UNITY_ANDROID
        horizontalInput = joystick.Horizontal;
        verticalInput = joystick.Vertical;
#endif

        movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        //Player Movement
        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        //Player rotation
        toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * 100 * Time.deltaTime);
        
    }
}