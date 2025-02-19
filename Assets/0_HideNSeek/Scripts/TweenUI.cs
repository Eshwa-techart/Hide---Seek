using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TweenUI : MonoBehaviour
{

    [SerializeField]
    List<GameObject> ScaleObj;
    [SerializeField]
    List<GameObject> moveFromRight;
    [SerializeField]
    List<GameObject> moveFromLeft;
    [SerializeField]
    List<GameObject> moveFromBottom;
    [SerializeField]
    List<GameObject> moveFromTop;
    [SerializeField]
    List<GameObject> AlphaObjs;
    public static TweenUI _instance;

   
    void OnEnable()
    {
        _instance = this;
        for (int i = 0; i < AlphaObjs.Count; i++)
        {
            if (AlphaObjs[i] != null)
            {
                AlphaObjs[i].GetComponent<Image>().color = new Color(AlphaObjs[i].GetComponent<Image>().color.r, AlphaObjs[i].GetComponent<Image>().color.g, AlphaObjs[i].gameObject.GetComponent<Image>().color.b, 0);

            }
        }
        Invoke(nameof(Call_TweenIn), 0.001f);
    }

    public void Call_TweenIn()
    {
        gameObject.SetActive(true);

        for (int i = 0; i < ScaleObj.Count; i++)
        {
            ScaleObj[i].transform.localScale = Vector3.zero;
        }

        float _delay = 0.15f;
        for (int i = 0; i < AlphaObjs.Count; i++)
        {
            if (AlphaObjs[i] != null)
            {
                _delay += 0.01f;
            }
        }
        for (int i = 0; i < moveFromTop.Count; i++)
        {
            iTween.MoveFrom(moveFromTop[i], iTween.Hash("Y", 2500, "time", 0.25f, "delay", _delay, "easetype", iTween.EaseType.easeOutCirc,"islocal",true));
            _delay += 0.2f;
        }

        for (int i = 0; i < ScaleObj.Count; i++)
        {
            iTween.ScaleTo(ScaleObj[i], iTween.Hash("Scale", Vector3.one, "time", 0.25f, "delay", _delay, "easetype", iTween.EaseType.linear, "islocal", true));

            _delay += 0.1f;
        }
        for (int i = 0; i < moveFromBottom.Count; i++)
        {
            iTween.MoveFrom(moveFromBottom[i], iTween.Hash("Y", -1000, "time", 0.25f, "delay", _delay, "easetype", iTween.EaseType.easeOutCirc, "islocal", true));
            _delay += 0.1f;
        }
        for (int i = 0; i < moveFromLeft.Count; i++)
        {
            iTween.MoveFrom(moveFromLeft[i], iTween.Hash("X", -1000, "time", 0.25f, "delay", _delay, "easetype", iTween.EaseType.easeOutCirc, "islocal", true));
            _delay += 0.1f;
        }
        for (int i = 0; i < moveFromRight.Count; i++)
        {
            iTween.MoveFrom(moveFromRight[i], iTween.Hash("X", 1000, "time", 0.25f, "delay", _delay, "easetype", iTween.EaseType.easeOutCirc, "islocal", true));
            _delay += 0.05f;
        }

    }
    private void OnDisable()
    {
        Call_TweenOut();
    }

    public void Call_TweenOut()
    {
        gameObject.SetActive(false);
    }
   
    

   
}
