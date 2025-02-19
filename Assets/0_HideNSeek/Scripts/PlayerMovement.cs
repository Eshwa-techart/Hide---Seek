using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 8.0f;
    public float rotationSpeed = 10.0f;

    private Rigidbody m_Rb;
    [HideInInspector]
    public DynamicJoystick joystick;

    public GameObject[] objectsToActivate;
    public PetAnimController petAnimCntrl;
    public bool isTeleported;
    public List<GameObject> playerPetsList;
    public GameObject cageModel;
    public FieldOfView playerFov;


    void Awake()
    {
        m_Rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        LoadRandomPet();

        joystick = GameController.instance.joystick;
        if (!GameController.instance.isShowJoystick)
        {
            joystick.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
            joystick.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
        }
        Invoke(nameof(EnablePlayerFOV), 5f);

        //if (MenuScript.isPlayerInvicible)
        //{
        //    SetPlayerInvicible();
        //}
    }

    void LoadRandomPet()
    {
        int randNum;
        randNum = CharacterSelection.petIndex;
        playerPetsList[randNum].SetActive(true);
        petAnimCntrl = playerPetsList[randNum].GetComponent<PetAnimController>();
        
        if (randNum == 0) GameController.instance.myPetType = GameController.PetType.cat;
        if (randNum == 1) GameController.instance.myPetType = GameController.PetType.dog;
        if (randNum == 2) GameController.instance.myPetType = GameController.PetType.monkey;
        if (randNum == 3) GameController.instance.myPetType = GameController.PetType.panda;
        if (randNum == 4) GameController.instance.myPetType = GameController.PetType.pig;
        if (randNum == 5) GameController.instance.myPetType = GameController.PetType.rabbit;
    }
    void EnablePlayerFOV()
    {
        playerFov.enabled = true;
        if (MenuScript.isHideGame)
        {
            playerFov.enabled = false;
            this.gameObject.layer = 9;
        }
        else if (MenuScript.isSeekGame)
        {
            playerFov.enabled = true;
            gameObject.layer = 0;
        }
    }
    public void ActivatePlayerObjects()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }
    float horizontalInput;
    float verticalInput;

    void FixedUpdate()
    {
        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");
       
        horizontalInput = joystick.Horizontal;
        verticalInput = joystick.Vertical;
        //Debug.Log("horizontalInput::" + horizontalInput + " VerticalInput::" + verticalInput);
        if ((horizontalInput == 0 || verticalInput == 0) && !GameController.instance.isStartTimer)
        {
            petAnimCntrl.SetIdleAnim();
        }
        else
        {
            petAnimCntrl.SetRunAnim();
        }

        Vector3 movement = new Vector3(-horizontalInput, 0, -verticalInput).normalized;

        if (movement == Vector3.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(movement);

        targetRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * rotationSpeed * Time.deltaTime);

        m_Rb.MovePosition(m_Rb.position + moveSpeed * Time.fixedDeltaTime * movement);
        
        m_Rb.MoveRotation(targetRotation);
    }
    public void SetInputToZero()
    {
        horizontalInput = 0;
        verticalInput = 0;
    }

    bool b;
    public void RunFaster()
    {
        if (!b)
        {
            b = true;
            moveSpeed *= 1.3f;
            Invoke(nameof(RunNormalSpeed), 10f);
        }
    }
    public void RunNormalSpeed()
    {
        moveSpeed = 8;
        b = false;
    }

    public void SetPlayerInvicible()
    {
        GameController.instance.player.petAnimCntrl.GetComponent<PetAnimController>().SetBodyTransparent();
    }

}