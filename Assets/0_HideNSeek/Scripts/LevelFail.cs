using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFail : MonoBehaviour
{
    [SerializeField] List<Text> petNamesList;

    [SerializeField]
    GameObject Hide_LF, Seek_LF, petEscape_LF;

    public Sprite greenBtn, redBtn;
    void OnEnable()
    {
        if (MenuScript.isHideGame)
        {
            Hide_LF.SetActive(true);
            Seek_LF.SetActive(false);
        }
        else if (MenuScript.isSeekGame)
        {
            Hide_LF.SetActive(false);
            Seek_LF.SetActive(true);

            for (int i = 0; i < GameController.instance.petnamesList.Count; i++)
            {
                petNamesList[i].text = "" + GameController.instance.petnamesList[i];

                if (GameController.instance.aiPlayersList[i].GetComponent<AIController>().isObjFound)
                {
                    petNamesList[i].transform.parent.GetComponent<Button>().image.sprite = greenBtn;
                }
                else
                {
                    petNamesList[i].transform.parent.GetComponent<Button>().image.sprite = redBtn;
                }
            }
        }
    }
    
    public void RetryBtnClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Ingame");
    }
   

}
