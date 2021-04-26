using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
   // [HideInInspector]
    public int coin;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("BoostUp"))
            GetComponent<PlayerController>().speed = 12f;
        if (other.CompareTag("BoostDown"))
            GetComponent<PlayerController>().speed = 3f;
        if (other.CompareTag("PlayerObstacleZone") && !other.transform.GetComponentInParent<Obstacle>().State &&
          GetComponent<PlayerController>().CanMove && other.transform.GetComponentInParent<Obstacle>().stopPosActive && 
          other.transform.GetComponentInParent<Obstacle>().BallCheck)
        {
            GetComponent<PlayerController>().CanMove = false;
            StartCoroutine(Helpers.MoveToStopPos(other.transform.GetComponentInParent<Obstacle>().stopPos.position, transform));
            //StartCoroutine(GetComponent<AI>().MoveToStopPos(GetComponent<AI>().target.root.GetComponent<Obstacle>().stopPos.position));
           // StartCoroutine(GetComponent<AI>().KickBall());
        }
        if (other.CompareTag("PlayerObstacleZone") && other.transform.GetComponentInParent<Obstacle>().State && !GetComponent<PlayerController>().CanMove
            && other.transform.GetComponentInParent<Obstacle>().BallCheck)
            GetComponent<PlayerController>().CanMove = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BoostUp") || other.CompareTag("BoostDown"))
            GetComponent<PlayerController>().speed = 8f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Finish"))
        {
            GameManager.Instance.FinishGame("Player");
            StartCoroutine(CameraFollow.Instance.ChangeOffset());
         
            PlayerController.Instance.PointsParent.gameObject.SetActive(true);
          
            StartCoroutine(PlayerController.Instance.PickABall());
            //StartCoroutine(PlayerController.Instance.FinishMec());
            transform.GetChild(transform.childCount - 1).GetChild(0).gameObject.SetActive(false);
        }
        if(other.CompareTag("FinishPlace"))
        {
            GetComponent<Animator>().SetTrigger(Animator.StringToHash("Win"));
            PlayerController.Instance.finish = false;
            PlayerController.Instance.PointsParent.gameObject.SetActive(false);
            PlayerController.Instance.enabled = false;
            CanvasManager.Instance.EndOfLevelUpdateText(coin);

            int currentLevel = EncryptedPlayerPrefs.GetInt("Level", 1) + 1;
            EncryptedPlayerPrefs.SetInt("Level", currentLevel);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
      
        if(collision.transform.CompareTag("FinishWall"))
        {
            PlayerController.Instance.finish = false;
            PlayerController.Instance.PointsParent.gameObject.SetActive(false);
            PlayerController.Instance.enabled = false;

            StartCoroutine(Helpers.MoveToStopPos(collision.transform.GetChild(3).position, transform));

            GetComponent<Animator>().SetTrigger(Animator.StringToHash("Win"));
            CanvasManager.Instance.EndOfLevelUpdateText(coin);

            int currentLevel = EncryptedPlayerPrefs.GetInt("Level", 1) + 1;
            EncryptedPlayerPrefs.SetInt("Level", currentLevel);
        }

        if(!PlayerController.Instance.finish  && collision.transform.CompareTag("Obstacle") && !collision.transform.GetComponentInParent<Obstacle>().BallCheck)
        {
            GetComponent<PlayerController>().CanMove = false;
            StartCoroutine(Helpers.MoveToStopPos(collision.transform.GetComponentInParent<Obstacle>().stopPos.position, transform));
            //StartCoroutine(GetComponent<AI>().MoveToStopPos(GetComponent<AI>().target.root.GetComponent<Obstacle>().stopPos.position));
            // StartCoroutine(GetComponent<AI>().KickBall());
        }
        if(collision.transform.CompareTag("BucketBall")&&collision.transform.root.name=="BucketR")
        {
            StartCoroutine(Helpers.MoveToStopPos(collision.transform.GetComponentInParent<Obstacle>().stopPos.position, transform));
            collision.transform.root.name = "Bucket";
        }
    }
}
