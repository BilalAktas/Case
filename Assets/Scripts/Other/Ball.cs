using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        //gameObject.SetActive(false);
        if(collision.transform.CompareTag("Obstacle"))
        {
            if (!GameManager.Instance.finish)
            { 
               
                if (!collision.transform.GetComponentInParent<Obstacle>().BallCheck)
                {
                    if(transform.root.GetComponent<PlayerController>())
                        PlayerController.Instance.CanMove = true; 
                    if(transform.root.GetComponent<AI>())
                        AI.Instance.CanMove = true;
                }
                if (collision.transform.name.Contains("Glass"))
                {                   
                    StartCoroutine(Helpers.BreakOBJ(collision.transform.gameObject, 0, 1));
                  
                        //
                        }
                else if (collision.transform.name.Contains("Bucket"))
                { StartCoroutine(Helpers.BreakOBJ(collision.transform.gameObject, 0, 1));
                    StartCoroutine(Helpers.OpenRBWithDelay(collision.transform.GetChild(2), .002f));
                }
                else
                collision.transform.GetComponentInParent<Obstacle>().ObstacleStuff(); 
            }
            else
            {
             
                GameManager.Instance.player.GetComponent<PlayerCollision>().coin += 20;
                collision.transform.tag = "Untagged";
                StartCoroutine(Helpers.BreakOBJ(collision.transform.parent.gameObject, 0, 1));
                //collision.transform.parent.gameObject.SetActive(false);
                PlayerController.Instance.speed += 3;
            }
        }
        Invoke("Disable", 2f);
    }

    void Disable()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
