using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class AIController : MonoBehaviour
{
    
    NavMeshAgent myNavAgent;
    public string PetName;
    public List<string> petnameList;
    public bool isAiSeeker;
    float timer;
    public float timetoChangePos;
    public float aiMoveSpeed;
    Vector3 newPos;
    bool moveAI;
    public bool isObjFound;
    GameController gameCntrl;
    bool seek_SetNewPos;
    public GameObject gameobjectMesh, cageObj, helpMeObj;
    public List<GameObject> lights;
    [SerializeField] GameObject hideSmoke;
    [SerializeField] List<GameObject> allAIPetsList;
    public PetAnimController petAnimCntrl;

    [SerializeField] Animator humanAnim;
    public FieldOfView seekerFov;
    [SerializeField] GameObject petNameText;
    bool isHumanAiSeeker;

    void Start()
    {
        gameCntrl = GameObject.Find("GameController").GetComponent<GameController>();
        GameObject go;
        go = allAIPetsList[Random.Range(0, allAIPetsList.Count)];
        go.SetActive(true);
        
        petAnimCntrl = go.GetComponent<PetAnimController>();
        if (!go.GetComponent<PetAnimController>())
            isHumanAiSeeker = true;
        
        if (isAiSeeker)
        {
            
            Invoke(nameof(MoveAI), 5f);
            Invoke(nameof(EnableFOV), 5f);
        }
        else
        {
            Invoke(nameof(MoveAI), 0f);
            PetName = petnameList[Random.Range(0, petnameList.Count)];
            petNameText.GetComponent<TextMeshPro>().text = "" + PetName;
        }

        myNavAgent = GetComponent<NavMeshAgent>();
        timer = timetoChangePos;
        if (MenuScript.isSeekGame)
        {
            Invoke(nameof(DisableMesh), 2f);
        }
    }
    void EnableFOV()
    {
        seekerFov.enabled = true;
        //for (int i = 0; i < lights.Count; i++)
        //{
        //    lights[i].SetActive(true);
        //}
    }
    void Update()
    {
        if (MenuScript.isHideGame)
        {
            if (moveAI)
            {
                timer += Time.deltaTime;
                if (isAiSeeker)
                {
                    myNavAgent.SetDestination(gameCntrl.player.transform.position);
                   // Debug.Log("seek ai speed::" + aiMoveSpeed + "       level unlocked::" + LevelSelection.hideNseekLevelNum);
                    if (GameEnums.LevelsUnlocked % 3 == 0)
                    {
                        myNavAgent.speed = 7;
                    }
                    //CheckThePets();
                }
                else
                {
                    timetoChangePos = 1;
                    if (timer >= timetoChangePos)
                    {
                        newPos = NewRandomNavSphere(transform.position, aiMoveSpeed * 3, -1);
                        myNavAgent.SetDestination(newPos);
                        timer = 0;
                    }
                }
                

            }
        }

        if (MenuScript.isSeekGame)
        {
            if (isObjFound)
                return;
            if (moveAI)
            {
                timer += Time.deltaTime;
                timetoChangePos = 1;
                if (timer >= timetoChangePos)
                {
                    newPos = NewRandomNavSphere(transform.position, aiMoveSpeed * 3, -1);
                    myNavAgent.SetDestination(newPos);
                    timer = 0;
                }

            }
        }
        
    }
    

    public void PlaySmoke()
    {
        if(!isObjFound)
        StartCoroutine(PlaySmokeEffect(0f));
    }

    IEnumerator PlaySmokeEffect(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        hideSmoke.SetActive(true);
        yield return new WaitForSeconds(1);
        hideSmoke.SetActive(false);
    }


    #region code for AI Seeker to catch the pets
    [SerializeField] GameObject rayPoint;
    //float angle = 40;
    //float distance = 04;


    int wideRayLength = 5;
    int tightRayLength = 5;
    int sideRayLength = 5;
    bool raycasting;
    Vector3 forward;
    Vector3 pivotPos;
    float radius = 1f;
    RaycastHit hit;

    void CheckThePets()
    {
        forward = transform.TransformDirection(new Vector3(0, 0, 1));
        pivotPos = rayPoint.transform.position;
      


        // New bools effected by fixed raycasts.
        bool tightTurn = false;
        bool wideTurn = false;
        bool sideTurn = false;
        bool tightTurn1 = false;
        bool wideTurn1 = false;
        bool sideTurn1 = false;

        // Drawing Rays.
        /*
        Debug.DrawRay(pivotPos, Quaternion.AngleAxis(25, transform.up) * forward * wideRayLength, Color.white);
        Debug.DrawRay(pivotPos, Quaternion.AngleAxis(-25, transform.up) * forward * wideRayLength, Color.white);

        Debug.DrawRay(pivotPos, Quaternion.AngleAxis(7, transform.up) * forward * tightRayLength, Color.white);
        Debug.DrawRay(pivotPos, Quaternion.AngleAxis(-7, transform.up) * forward * tightRayLength, Color.white);

        Debug.DrawRay(pivotPos, Quaternion.AngleAxis(40, transform.up) * forward * sideRayLength, Color.white);
        Debug.DrawRay(pivotPos, Quaternion.AngleAxis(-40, transform.up) * forward * sideRayLength, Color.white);
        */
        // Wide Raycasts.
        if (Physics.SphereCast(pivotPos,radius,Quaternion.AngleAxis(25, transform.up) * forward, out hit, wideRayLength) && !hit.collider.isTrigger && hit.transform.root != transform)//&& !hit.collider.isTrigger && hit.transform.root != transform
        {
           // Debug.DrawRay(pivotPos, Quaternion.AngleAxis(25, transform.up) * forward * wideRayLength, Color.red);
            wideTurn = true;
        }

        else
        {
            wideTurn = false;
        }

        if (Physics.SphereCast(pivotPos,radius, Quaternion.AngleAxis(-25, transform.up) * forward, out hit, wideRayLength) && !hit.collider.isTrigger && hit.transform.root != transform)
        {
           // Debug.DrawRay(pivotPos, Quaternion.AngleAxis(-25, transform.up) * forward * wideRayLength, Color.red);
            wideTurn1 = true;
        }
        else
        {
            wideTurn1 = false;
        }

        // Tight Raycasts.
        if (Physics.SphereCast(pivotPos, radius,Quaternion.AngleAxis(7, transform.up) * forward, out hit, tightRayLength) && !hit.collider.isTrigger && hit.transform.root != transform)
        {
           // Debug.DrawRay(pivotPos, Quaternion.AngleAxis(7, transform.up) * forward * tightRayLength, Color.red);
            tightTurn = true;
        }
        else
        {
            tightTurn = false;
        }

        if (Physics.SphereCast(pivotPos, radius, Quaternion.AngleAxis(-7, transform.up) * forward, out hit, tightRayLength) && !hit.collider.isTrigger && hit.transform.root != transform)
        {
            //Debug.DrawRay(pivotPos, Quaternion.AngleAxis(-7, transform.up) * forward * tightRayLength, Color.red);
            tightTurn1 = true;
        }
        else
        {
            tightTurn1 = false;
        }

        // Side Raycasts.
        if (Physics.SphereCast(pivotPos,radius, Quaternion.AngleAxis(40, transform.up) * forward, out hit, sideRayLength)&& !hit.collider.isTrigger && hit.transform.root != transform)
        {
            //Debug.DrawRay(pivotPos, Quaternion.AngleAxis(40, transform.up) * forward * sideRayLength, Color.red);
            sideTurn = true;
        }
        else
        {
            sideTurn = false;

        }

        if (Physics.SphereCast(pivotPos,radius, Quaternion.AngleAxis(-40, transform.up) * forward, out hit, sideRayLength) && !hit.collider.isTrigger && hit.transform.root != transform)
        {
            //Debug.DrawRay(pivotPos, Quaternion.AngleAxis(-40, transform.up) * forward * sideRayLength, Color.red);
            sideTurn1 = true;
        }
        else
        {
            sideTurn1 = false;
        }

        // Raycasts hits an obstacle now?
        if (wideTurn || wideTurn1 || tightTurn || tightTurn1 || sideTurn || sideTurn1)
            raycasting = true;
        else
            raycasting = false;

        // If raycast hits a collider, feed rayInput.
        if (raycasting)
        {
            //Debug.Log("player found: " + hit.collider.gameObject.name);

            if (hit.collider.gameObject.CompareTag("Player"))
            {
                //Debug.Log("found the player pet::" + hit.collider.gameObject.name);
                GameController.instance.player.petAnimCntrl.SetIdleAnim();
                GameController.instance.player.moveSpeed = 0;
                GameController.instance.player.cageModel.SetActive(true);
                GameController.instance.LoadLevelFail();
            }

            else if (hit.collider.gameObject.CompareTag("Opponent"))
            {
               // Debug.Log("found the AI pet::" + hit.collider.gameObject.name);
                GameController.instance.petArrestedcount++;
                hit.collider.gameObject.GetComponent<AIController>().isObjFound = true;
                hit.collider.gameObject.GetComponent<AIController>().StopAI();
                hit.collider.gameObject.GetComponent<AIController>().DisableMesh();
                GameController.instance.SetLifeUI(GameController.instance.petArrestedcount - 1);
            }

        }
      

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (Physics.SphereCast(pivotPos, radius, Quaternion.AngleAxis(25, transform.up) * forward, out hit, wideRayLength)&& !hit.collider.isTrigger && hit.transform.root != transform)
        {
            Debug.DrawLine(pivotPos, Quaternion.AngleAxis(25, transform.up) * forward * wideRayLength, Color.green);
            Gizmos.DrawWireSphere(pivotPos + Quaternion.AngleAxis(25, transform.up) * forward  * hit.distance, radius);
        }
        if (Physics.SphereCast(pivotPos, radius, Quaternion.AngleAxis(-25, transform.up) * forward, out hit, wideRayLength) && !hit.collider.isTrigger && hit.transform.root != transform)
        {
            Debug.DrawLine(pivotPos, Quaternion.AngleAxis(-25, transform.up) * forward * wideRayLength, Color.green);
            Gizmos.DrawWireSphere(pivotPos + Quaternion.AngleAxis(-5, transform.up) * forward  * hit.distance, radius);
        }
        // Tight Raycasts.
        if (Physics.SphereCast(pivotPos, radius, Quaternion.AngleAxis(7, transform.up) * forward, out hit, tightRayLength) && !hit.collider.isTrigger && hit.transform.root != transform)
        {
            Debug.DrawLine(pivotPos, Quaternion.AngleAxis(7, transform.up) * forward * wideRayLength, Color.green);
            Gizmos.DrawWireSphere(pivotPos + Quaternion.AngleAxis(7, transform.up) * forward  * hit.distance, radius);
        }
        if (Physics.SphereCast(pivotPos, radius, Quaternion.AngleAxis(-7, transform.up) * forward, out hit, tightRayLength) && !hit.collider.isTrigger && hit.transform.root != transform)
        {
            Debug.DrawLine(pivotPos, Quaternion.AngleAxis(-7, transform.up) * forward * wideRayLength, Color.green);
            Gizmos.DrawWireSphere(pivotPos + Quaternion.AngleAxis(-7, transform.up) * forward  * hit.distance, radius);
        }
        // Side Raycasts.
        if (Physics.SphereCast(pivotPos, radius, Quaternion.AngleAxis(40, transform.up) * forward, out hit, sideRayLength) && !hit.collider.isTrigger && hit.transform.root != transform)
        {
            Debug.DrawLine(pivotPos, Quaternion.AngleAxis(40, transform.up) * forward * wideRayLength, Color.green);
            Gizmos.DrawWireSphere(pivotPos + Quaternion.AngleAxis(40, transform.up) * forward  * hit.distance, radius);
        }
        if (Physics.SphereCast(pivotPos, radius, Quaternion.AngleAxis(-40, transform.up) * forward, out hit, sideRayLength) && !hit.collider.isTrigger && hit.transform.root != transform)
        {
            Debug.DrawLine(pivotPos, Quaternion.AngleAxis(-40, transform.up) * forward * wideRayLength, Color.green);
            Gizmos.DrawWireSphere(pivotPos + Quaternion.AngleAxis(-40, transform.up) * forward  * hit.distance, radius);
        }
    }

    #endregion


    /// <summary>
    /// Get Random points for ai to move in some intervals
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="dist"></param>
    /// <param name="layermask"></param>
    /// <returns></returns>

    Vector3 NewRandomNavSphere(Vector3 origin,float dist,int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection,out navHit, dist, layermask);
        return navHit.position;
    }

    /// <summary>
    /// This method is to stop ai and make it idle
    /// </summary>
    public void  StopAI()
    {
        moveAI = false;
       
        myNavAgent.speed = 0;
        myNavAgent.enabled = false;
        if (isAiSeeker)
        {
            if (isHumanAiSeeker)
                SetHumanIdleAnim();
            else
                petAnimCntrl.SetIdleAnim();
        }
        else
        {
            petAnimCntrl.SetIdleAnim();
        }
    }

    /// <summary>
    /// This method is to move the AI 
    /// </summary>
    public void MoveAI()
    {
        moveAI = true;
        myNavAgent.enabled = true;
        myNavAgent.speed = aiMoveSpeed;
        if (isAiSeeker)
        {
            if (isHumanAiSeeker)
                SetHumanRunAnim();
            else
                petAnimCntrl.SetRunAnim();
        }
        else
        {
            petAnimCntrl.SetRunAnim();
        }
    }

    public void DisableMesh()
    {
        if (MenuScript.isSeekGame)
        {
            cageObj.SetActive(false);
            helpMeObj.SetActive(false);
            gameobjectMesh.SetActive(false);
            petNameText.gameObject.SetActive(false);
        }
        else if (MenuScript.isHideGame && !isAiSeeker)
        {
            cageObj.SetActive(true);
            helpMeObj.SetActive(true);
        }
    }
    public void EnableMesh()
    {
        if (MenuScript.isSeekGame)
        {
            cageObj.SetActive(true);
            //helpMeObj.SetActive(true);
            gameobjectMesh.SetActive(true);
            petNameText.gameObject.SetActive(true);

            if (!isObjFound)
                gameCntrl.CheckLevelTarget();
        }
        else if (MenuScript.isHideGame)
        {

        }
    }
    


    #region Human Charaacter animation

    public void SetHumanRunAnim()
    {
        humanAnim.SetTrigger("run");
    }
    public void SetHumanIdleAnim()
    {
        humanAnim.SetTrigger("idle");
    }
    #endregion
}
