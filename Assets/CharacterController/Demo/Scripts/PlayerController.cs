//#define MB_DEBUG

using MenteBacata.ScivoloCharacterController;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

namespace MenteBacata.ScivoloCharacterControllerDemo
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 8f;

        public float jumpSpeed = 8f;
        
        public float rotationSpeed = 720f;

        public float gravity = -25f;

        public CharacterMover mover;

        public GroundDetector groundDetector;

        public MeshRenderer groundedIndicator;

        private const float minVerticalSpeed = -12f;

        // Allowed time before the character is set to ungrounded from the last time he was safely grounded.
        private const float timeBeforeUngrounded = 0.02f;

        // Speed along the character local up direction.
        private float verticalSpeed = 0f;

        // Time after which the character should be considered ungrounded.
        private float nextUngroundedTime = -1f;

        private Transform cameraTransform;
        
        private MoveContact[] moveContacts = CharacterMover.NewMoveContactArray;

        private int contactCount;

        private bool isOnMovingPlatform = false;

        private MovingPlatform movingPlatform;
        public DynamicJoystick joystick;
        public GameObject[] objectsToActivate;
        public PetAnimController petAnimCntrl;
        public bool isTeleported;
        public GameObject seekPlayerTrigger, hidePlayerTrigger;
        public List<GameObject> playerPetsList;
        public List<GameObject> otherPetsObjectsList;
        public List<GameObject> dogObjectsList;

        public GameObject cageModel;
        public FieldOfView playerFov;



        private void Start()
        {
            cameraTransform = Camera.main.transform;
            mover.canClimbSteepSlope = true;

            
            joystick = GameController.instance.joystick;

            if (GameEnums.GameControlsType=="Touch")
            {
                joystick.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
                joystick.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
            }
            else if(GameEnums.GameControlsType=="Joystick")
            {
                joystick.transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
                joystick.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
            }

            if (!GameController.isBonusLevel)
                Invoke(nameof(EnablePlayerFOV), 5f);

            ActivatePlayerObjects();

            //if (!MenuScript.isPetEscapeLevels)
            //{
            //    moveSpeed = 0;
            //    if (!GameController.isBonusLevel)
            //        Invoke(nameof(EnablePlayerFOV), 5f);

            //    //Debug.Log("isplayer invicible::" + MenuScript.isPlayerInvicible);
            //    //if (MenuScript.isPlayerInvicible)
            //    //{
            //    //    SetPlayerInvicible();
            //    //}
            //}
            //if (MenuScript.isPetEscapeLevels)
            //{
            //    ActivatePlayerObjects();
            //    gameObject.layer = 9;
            //}

            LoadRandomPet();
            //joystick.gameObject.SetActive(true);
           
        }
       
        void LoadRandomPet()
        {
            int randNum;
            //randNum = Random.Range(0, playerPetsList.Count);
           // Debug.Log("pet index11111111111::" + CharacterSelection.petIndex);
            randNum = CharacterSelection.petIndex;
            Debug.Log("petindex::111111" + randNum);
            playerPetsList[randNum].SetActive(true);
           // petAnimCntrl = playerPetsList[randNum].GetComponent<PetAnimController>();
            if (randNum == 0) GameController.instance.myPetType = GameController.PetType.cat;
            if (randNum == 1) GameController.instance.myPetType = GameController.PetType.dog;
            if (randNum == 2) GameController.instance.myPetType = GameController.PetType.monkey;
            if (randNum == 3) GameController.instance.myPetType = GameController.PetType.panda;
            if (randNum == 4) GameController.instance.myPetType = GameController.PetType.pig;
            if (randNum == 5) GameController.instance.myPetType = GameController.PetType.rabbit;


            if (randNum == 1)//dog
            {
                for (int i = 0; i < dogObjectsList.Count; i++)
                {
                    dogObjectsList[i].SetActive(true);
                }

                for (int i = 0; i < otherPetsObjectsList.Count; i++)
                {
                    otherPetsObjectsList[i].SetActive(false);
                }
            }
            else
            {
                for (int i = 0; i < otherPetsObjectsList.Count; i++)
                {
                    otherPetsObjectsList[i].SetActive(true);
                }

                for (int i = 0; i < dogObjectsList.Count; i++)
                {
                    dogObjectsList[i].SetActive(false);
                }
            }
           

        }
        void EnablePlayerFOV()
        {

            playerFov.enabled = true;
            if (MenuScript.isHideGame)
            {
                playerFov.enabled = false;
                gameObject.layer = 9;
            }
            else if (MenuScript.isSeekGame)
            {
                playerFov.enabled = true;
                gameObject.layer = 0;
            }
            //if (MenuScript.isHideGame)
            //{
            //    hidePlayerTrigger.SetActive(true);
            //    seekPlayerTrigger.SetActive(false);
            //}
            //else if (MenuScript.isSeekGame)
            //{
            //    hidePlayerTrigger.SetActive(false);
            //    seekPlayerTrigger.SetActive(true);
            //}
        }

        public void SetInputToZero()
        {
            horizontalInput = 0;
            verticalInput = 0;
        }


        public void ActivatePlayerObjects()
        {
            for (int i = 0; i < objectsToActivate.Length; i++)
            {
                objectsToActivate[i].SetActive(true);
            }
        }

        Vector3 velocity;
        float deltaTime;
        Vector3 movementInput;
        bool groundDetected;
        private void FixedUpdate()
        {
            deltaTime = Time.deltaTime;
            movementInput = GetMovementInput();

            velocity = moveSpeed * movementInput;

            groundDetected = DetectGroundAndCheckIfGrounded(out bool isGrounded, out GroundInfo groundInfo);

            SetGroundedIndicatorColor(isGrounded);

            isOnMovingPlatform = false;

            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                verticalSpeed = jumpSpeed;
                nextUngroundedTime = -1f;
                isGrounded = false;
            }

            if (isGrounded)
            {
                mover.isInWalkMode = true;
                verticalSpeed = 0f;

                if (groundDetected)
                    isOnMovingPlatform = groundInfo.collider.TryGetComponent(out movingPlatform);
            }
            else
            {
                mover.isInWalkMode = false;

                BounceDownIfTouchedCeiling();

                verticalSpeed += gravity * deltaTime;

                if (verticalSpeed < minVerticalSpeed)
                    verticalSpeed = minVerticalSpeed;

                velocity += verticalSpeed * transform.up;
            }

            RotateTowards(velocity);
            mover.Move(velocity * deltaTime, moveContacts, out contactCount);
           
        }
        //private void FixedUpdate()
        //{
        //    RotateTowards(velocity);
        //    mover.Move(velocity * deltaTime, moveContacts, out contactCount);
        //}
        private void LateUpdate()
        {
            if (isOnMovingPlatform)
                ApplyPlatformMovement(movingPlatform);
        }

        float horizontalInput, verticalInput;

        private Vector3 GetMovementInput()
        {
            ///float x = Input.GetAxis("Horizontal");
            //float y = Input.GetAxis("Vertical");
            

            horizontalInput = joystick.Horizontal;
            verticalInput = joystick.Vertical;
//            Debug.Log("joystick X::" + x + " joystick Y:" + y);
            if (horizontalInput == 0 || verticalInput == 0)
            {
                petAnimCntrl.SetIdleAnim();
            }
            else
            {

                if (GameController.instance.isEnableJoystick)
                    petAnimCntrl.SetRunAnim();
                else
                    petAnimCntrl.SetIdleAnim();
            }
            Vector3 forward = Vector3.ProjectOnPlane(cameraTransform.forward, transform.up).normalized;
            Vector3 right = Vector3.Cross(transform.up, forward);

            return horizontalInput * right + verticalInput * forward;
        }

        private bool DetectGroundAndCheckIfGrounded(out bool isGrounded, out GroundInfo groundInfo)
        {
            bool groundDetected = groundDetector.DetectGround(out groundInfo);

            if (groundDetected)
            {
                if (groundInfo.isOnFloor && verticalSpeed < 0.1f)
                    nextUngroundedTime = Time.time + timeBeforeUngrounded;
            }
            else
                nextUngroundedTime = -1f;

            isGrounded = Time.time < nextUngroundedTime;
            return groundDetected;
        }

        private void SetGroundedIndicatorColor(bool isGrounded)
        {
            if (groundedIndicator != null)
                groundedIndicator.material.color = isGrounded ? Color.green : Color.blue;
        }

        private void RotateTowards(in Vector3 direction)
        {
            Vector3 flatDirection = Vector3.ProjectOnPlane(direction, transform.up);

            if (flatDirection.sqrMagnitude < 1E-06f)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(flatDirection, transform.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        private void ApplyPlatformMovement(MovingPlatform movingPlatform)
        {
            GetMovementFromMovingPlatform(movingPlatform, out Vector3 movement, out float upRotation);

            transform.Translate(movement, Space.World);
            transform.Rotate(0f, upRotation, 0f, Space.Self);
        }

        private void GetMovementFromMovingPlatform(MovingPlatform movingPlatform, out Vector3 movement, out float deltaAngleUp)
        {
            movingPlatform.GetDeltaPositionAndRotation(out Vector3 platformDeltaPosition, out Quaternion platformDeltaRotation);
            Vector3 localPosition = transform.position - movingPlatform.transform.position;
            movement = platformDeltaPosition + platformDeltaRotation * localPosition - localPosition;

            platformDeltaRotation.ToAngleAxis(out float platformDeltaAngle, out Vector3 axis);
            float axisDotUp = Vector3.Dot(axis, transform.up);

            if (-0.1f < axisDotUp && axisDotUp < 0.1f)
                deltaAngleUp = 0f;
            else
                deltaAngleUp = platformDeltaAngle * Mathf.Sign(axisDotUp);
        }
        
        private void BounceDownIfTouchedCeiling()
        {
            for (int i = 0; i < contactCount; i++)
            {
                if (Vector3.Dot(moveContacts[i].normal, transform.up) < -0.7f)
                {
                    verticalSpeed = -0.25f * verticalSpeed;
                    break;
                }
            }
        }
        bool b;
        public void RunFaster()
        {
            if (!b)
            {
                b = true;
                moveSpeed = 12f;
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

       

        ////MO
        
        //Collider[] listOfObjects;
        //[SerializeField]
        //List<GameObject> objNearToPlayer;
        //GameObject GetthisGameobject()
        //{
        //    listOfObjects = Physics.OverlapSphere(this.gameObject.transform.position, 5);
            
        //    for (var i = 0; i < listOfObjects.Length; i++)
        //    {
        //        if (listOfObjects[i].GetComponent<NavMeshAgent>())
        //        {
        //            var angle = Vector3.Angle(listOfObjects[i].gameObject.transform.position, gameObject.transform.position);
        //            if (angle > 1 && angle < 25)
        //            {
        //                objNearToPlayer.Add(listOfObjects[i].gameObject);
        //                return listOfObjects[i].gameObject;
        //            }
        //        }
        //    }
        //    return null;
        //}


        ////MO


    }
}
