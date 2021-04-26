using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private Transform obj;


    [SerializeField]
    private Transform target;

    
    public Transform stopPos;

    public bool BallCheck;

    public string doWhat;
    [SerializeField]
    private string SetTagOff;
    [SerializeField]
    private string SetTagOn;

    public bool stopPosActive;

    public bool State;
    public void ObstacleStuff()
    {
        Color color;
        switch (State)
        {
            case true:
                switch (doWhat)
                {
                    case "Anim":
                        obj.GetComponent<Animation>().Play("Off");
                        break;
                    case "Active":
                        obj.gameObject.SetActive(false);
                        break;
                    case "Tag":
                        obj.tag = SetTagOff;
                        break;
                    default:
                        break;
                }
                if (ColorUtility.TryParseHtmlString("#FF0012", out color))
                { target.GetComponent<MeshRenderer>().materials[1].color = color; }

                State = false;
                break;
            case false:
                switch (doWhat)
                {
                    case "Anim":
                        obj.GetComponent<Animation>().Play("On");
                        break;
                    case "Active":
                        obj.gameObject.SetActive(true);
                        break;
                    case "Tag":
                        obj.tag = SetTagOn;
                        break;
                    default:
                        break;
                }
               
            
                if (ColorUtility.TryParseHtmlString("#3DFF17", out color))
                { target.GetComponent<MeshRenderer>().materials[1].color = color; }
              
                State = true;
                break;     
            default:
                break;
        }
      
    }

}
