using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] public GameObject Panel;
    [SerializeField] public GameObject buttonHide;
    bool isActive = true;

    void Start() 
    {
        
    }
    
    public void OpenShopPanel()
    {
        if(Panel !=null)
        {
            
            Vector3 outFramePanel = new Vector3(1061.5f,0f,0f);
            Vector3 inFramePanel = new Vector3(832.5f,0f,0f);
            Vector3 outFrameButton = new Vector3(918f,500f,0f);
            Vector3 inFrameButton = new Vector3(690f,500f,0f);


            if (isActive)
            {
                Debug.Log("moving to outframe");
                LeanTween.moveLocal(Panel,outFramePanel,0.4f).setEase(LeanTweenType.easeInOutBack);
                LeanTween.moveLocal(buttonHide,outFrameButton,0.4f).setEase(LeanTweenType.easeInOutBack);
                isActive = false;
                
            }
            else
            {
                Debug.Log("moving to inframe");
                LeanTween.moveLocal(Panel,inFramePanel,0.4f).setEase(LeanTweenType.easeInOutBack);
                LeanTween.moveLocal(buttonHide,inFrameButton,0.4f).setEase(LeanTweenType.easeInOutBack);
                isActive = true;
            }
            return;
        }
        
    }

    public void OpenInfoPanel(GameObject Ally)
    {
        //set icon to matching ally
        //
    }

    public void CloseInfoPanel(GameObject Ally)
    {
        Debug.Log("do thing");
    }

    public void ChangeIcon()
    {
        //when called, change icon to current target.
    }
}
