using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetRescueAI : MonoBehaviour
{
    
    public bool isRotate;


    public bool onlyRotateChar;
    
    public float angle1, angle2;
   // bool rotateNow;
    
    [SerializeField]
    List<GameObject> aiMovePoints;

    
    public  bool moveAi;

    [SerializeField]
    float moveSpeed;


    public bool is2Points;

    [SerializeField]
    GameObject twoPointsObj,fourPointsObj;

    [SerializeField] Animator humanAnim;


    void Start()
    {
        if (is2Points)
        {
            twoPointsObj.SetActive(true);
            fourPointsObj.SetActive(false);
        }
        else
        {
            twoPointsObj.SetActive(false);
            fourPointsObj.SetActive(true);
        }
        Invoke(nameof(EnableCollider), 1f);

        if (onlyRotateChar)
        {
            Invoke(nameof(RotateCharNow), 1f);
            moveAi = false;
            isRotate = false;
        }
    }

    void EnableCollider()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    void Update()
    {
        if (moveAi)
        {
            transform.position += moveSpeed * Time.deltaTime * transform.forward;
            SetRunAnim();
        }
        else
        {
            SetIdleAnim();
        }
        //if (isRotate && !moveAi)
        //{
        //    SetIdleAnim();
        //    if (is2Points)
        //    {
        //        if (twoPointscount == 1)
        //        {
        //            RotateAI(Quaternion.Euler(0, 180, 0));
        //            Invoke(nameof(MakeAiMove), 0.5f);
        //        }
        //        else if (twoPointscount == 2)
        //        {
        //            RotateAI(Quaternion.Euler(0, 0, 0));
        //            Invoke(nameof(MakeAiMove), 0.5f);
        //        }
        //    }
        //    else
        //    {
        //        Debug.Log("fourpointscount:" + fourPointsCount);
        //        if (fourPointsCount == 0)
        //        {
        //            RotateAI(Quaternion.Euler(0, 0, 0));
        //            Invoke(nameof(MakeAiMove), 0.5f);
        //        }
        //        else if (fourPointsCount == 1)
        //        {
        //            RotateAI(Quaternion.Euler(0, 90, 0));
        //            Invoke(nameof(MakeAiMove), 0.5f);
        //        }
        //        else if (fourPointsCount == 2)
        //        {
        //            RotateAI(Quaternion.Euler(0, 180, 0));
        //            Invoke(nameof(MakeAiMove), 0.5f);
        //        }
        //        else if (fourPointsCount == 3)
        //        {
        //            RotateAI(Quaternion.Euler(0, 270, 0));
        //            Invoke(nameof(MakeAiMove), 0.5f);
        //        }
        //        else if (fourPointsCount == 4)
        //        {
        //            RotateAI(Quaternion.Euler(0, 0, 0));
        //            fourPointsCount = 1;
        //            Invoke(nameof(MakeAiMove), 0.5f);
        //        }
        //    }
        //}
        //if (!onlyRotateChar)
        //{

        //}
        //else
        //{

        //    if (rotateNow)
        //        RotateAI(Quaternion.Euler(0, angle1, 0));
        //    else
        //        RotateAI(Quaternion.Euler(0, angle2, 0));
        //}

    }
    int twoPointscount, fourPointsCount;
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("triggered to ::" + other.gameObject.name);
        if (other.gameObject.name == "Pos_1")
        {
            Invoke(nameof(MoveToPos2), 0.2f);
        }
        if (other.gameObject.name == "Pos_2")
        {
            Invoke(nameof(MoveToPos3), 0.2f);
        }
        if (other.gameObject.name == "Pos_3")
        {
            Invoke(nameof(MoveToPos4), 0.2f);
        }
        if (other.gameObject.name == "Pos_4")
        {
            Invoke(nameof(MoveToPos1), 0.2f);
        }

    }
    void RotateAI(float yAngle)
    {
        //transform.localRotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 0.5f);
        iTween.RotateTo(gameObject, iTween.Hash("y", yAngle, "time", 0.5f, "easetype", iTween.EaseType.linear, "islocal", true));
        Debug.Log("rotation::" + yAngle);
        
        
    }
    void MakeAiMove()
    {
        if (!onlyRotateChar)
        {
            moveAi = true;
            isRotate = false;
        }
        
    }
    void MoveToPos1()
    {
        moveAi = false;
        //isRotate = true;
        fourPointsCount = 3;
        WaitForMoveAi();
    }
    void MoveToPos2()
    {
        moveAi = false;
        //isRotate = true;
        if (is2Points)
        {
            twoPointscount = 2;
        }
        else
        {
            fourPointsCount = 0;
        }
        WaitForMoveAi();
    }
    void MoveToPos3()
    {
        moveAi = false;
        //isRotate = true;
        if (is2Points)
        {
            twoPointscount = 1;
        }
        else
        {
            fourPointsCount = 1;
        }
        WaitForMoveAi();
    }

    void MoveToPos4()
    {
        moveAi = false;
        //isRotate = true;
        fourPointsCount = 2;
        WaitForMoveAi();
    }


    #region Human Charaacter animation

    public void SetRunAnim()
    {
        humanAnim.SetTrigger("run");
    }
    public void SetIdleAnim()
    {
        humanAnim.SetTrigger("idle");
    }
    #endregion


    void RotateCharNow()
    {
        RotateAI(angle1);
        Invoke(nameof(RotateCharNow1), 2f);
    }
    void RotateCharNow1()
    {
        RotateAI(angle2);
        Invoke(nameof(RotateCharNow), 2f);
    }

    float _delay = 0.7f;
    void WaitForMoveAi()
    {
        SetIdleAnim();
        if (is2Points)
        {
            if (twoPointscount == 1)
            {
                RotateAI(180);
                Invoke(nameof(MakeAiMove), _delay);
            }
            else if (twoPointscount == 2)
            {
                RotateAI(0);
                Invoke(nameof(MakeAiMove), _delay);
            }
        }
        else
        {
            Debug.Log("fourpointscount:" + fourPointsCount);
            if (fourPointsCount == 0)
            {
                RotateAI(0);
                Invoke(nameof(MakeAiMove), _delay);
            }
            else if (fourPointsCount == 1)
            {
                RotateAI(90);
                Invoke(nameof(MakeAiMove), _delay);
            }
            else if (fourPointsCount == 2)
            {
                RotateAI(180);
                Invoke(nameof(MakeAiMove), _delay);
            }
            else if (fourPointsCount == 3)
            {
                RotateAI(270);
                Invoke(nameof(MakeAiMove), _delay);
            }
            else if (fourPointsCount == 4)
            {
                RotateAI(0);
                fourPointsCount = 1;
                Invoke(nameof(MakeAiMove), _delay);
            }
        }
    }
}
