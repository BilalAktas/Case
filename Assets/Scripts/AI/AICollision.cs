using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            GetComponent<AI>().target = other.transform.parent.GetChild(0);
            StartCoroutine(GetComponent<AI>().KickBall());
        }
        if (other.CompareTag("PlayerTarget"))
            GetComponent<AI>().playerTarget = other.transform.root.GetChild(0);
        if(other.CompareTag("Finish"))
        { GameManager.Instance.FinishGame("AI"); }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("BoostUp"))
            GetComponent<AI>().speed = 12f;
        if (other.CompareTag("BoostDown"))
            GetComponent<AI>().speed = 3f;
        if (other.CompareTag("ObstacleZone") && !GetComponent<AI>().target.GetComponentInParent<Obstacle>().State && 
            GetComponent<AI>().CanMove && GetComponent<AI>().target.GetComponentInParent<Obstacle>().stopPosActive
             && other.transform.GetComponentInParent<Obstacle>().BallCheck)
        {
            GetComponent<AI>().CanMove = false;
            StartCoroutine(Helpers.MoveToStopPos(GetComponent<AI>().target.GetComponentInParent<Obstacle>().stopPos.position, transform));
            //StartCoroutine(GetComponent<AI>().MoveToStopPos(GetComponent<AI>().target.root.GetComponent<Obstacle>().stopPos.position));
            StartCoroutine(GetComponent<AI>().KickBall());
        }
        if(other.CompareTag("ObstacleZone")&& GetComponent<AI>().target.GetComponentInParent<Obstacle>().State
             && other.transform.GetComponentInParent<Obstacle>().BallCheck)
            GetComponent<AI>().CanMove = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BoostUp") || other.CompareTag("BoostDown"))
            GetComponent<AI>().speed = 8f;
        if (other.CompareTag("ObstacleZone"))
            GetComponent<AI>().target = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Obstacle") && !collision.transform.GetComponentInParent<Obstacle>().BallCheck)
        {
            GetComponent<AI>().CanMove = false;
            StartCoroutine(Helpers.MoveToStopPos(collision.transform.GetComponentInParent<Obstacle>().stopPos.position, transform));
            //StartCoroutine(GetComponent<AI>().MoveToStopPos(GetComponent<AI>().target.root.GetComponent<Obstacle>().stopPos.position));
            // StartCoroutine(GetComponent<AI>().KickBall());
        }
        if (collision.transform.CompareTag("BucketBall") && collision.transform.root.name == "BucketL")
        {
            StartCoroutine(Helpers.MoveToStopPos(collision.transform.GetComponentInParent<Obstacle>().stopPos.position, transform));
            collision.transform.root.name = "Bucket";
        }
    }
}
