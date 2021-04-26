using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviorHelper<AI>
{
    GameObject Player;
    GameObject ball;
    [SerializeField]
    private Transform BallsParent;
    [SerializeField]
    private Transform aimTransform;
    [SerializeField]
    float force;
    
    public float speed=7f;
    //[HideInInspector]
    public Transform target;
    public Transform playerTarget;

    [HideInInspector]
    public bool CanMove;

    private void Start()
    {
        //GameManager.Instance.ai = gameObject;
        Player = PlayerController.Instance.gameObject;
        StartCoroutine(PickABall());
        //StartCoroutine(KickBall());
    }
    private void Update()
    {
        if(CanMove)
        transform.position += transform.forward * speed * Time.deltaTime;
    }
 
  

    public IEnumerator PickABall()
    {
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < BallsParent.childCount; i++)
        {
            ball = BallsParent.GetChild(i).gameObject;
            if (!ball.activeInHierarchy)
            { ball.transform.localPosition = new Vector3(0, 3.48f, 0); ball.SetActive(true); break; }
        }
    }

   
    public IEnumerator KickBall()
    {
        //bool kick_playerTarget=false;
        Vector3 dir=Vector3.zero;
        if (Player.transform.position.z > transform.position.z && playerTarget && Player.transform.position.z < playerTarget.position.z)
        {

            if (playerTarget.root.GetComponent<Obstacle>().BallCheck && playerTarget.root.GetComponent<Obstacle>().State)
            dir = TargetDir(playerTarget);
        }
        else
        {
            if (target && !target.GetComponentInParent<Obstacle>().State)
                dir = TargetDir(target);
        }

        if (dir != Vector3.zero)
        {
            float time = 0.0f;
        while (time < 1)
        {
            aimTransform.rotation = Quaternion.Lerp(aimTransform.rotation, Quaternion.LookRotation(dir), time);

            time += Time.deltaTime * .9f;

            yield return new WaitForEndOfFrame();
        }

       
            GetComponent<Animator>().SetTrigger(Animator.StringToHash("KickBall"));
            ball.GetComponent<Rigidbody>().velocity = aimTransform.forward * force;
            ball.GetComponent<Rigidbody>().isKinematic = false;
         

            yield return new WaitForSeconds(.6f);

           
        }
        StartCoroutine(PickABall());
        if(ball && target && !target.GetComponentInParent<Obstacle>().State)
        StartCoroutine(KickBall());
    }

    Vector3 TargetDir(Transform _target)
    {
        Vector3 dir = Vector3.zero;
        int r = Random.Range(1, 11);
        if (r <= 9)
        dir = (_target.position - transform.position) + Vector3.down * 1.6f + -Vector3.right * .3f; 
        else // miss 
         dir = (_target.position - transform.position) + /*Random.insideUnitSphere*/ Vector3.up * 2.3f; 
        return dir;
    }
}
