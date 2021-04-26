using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviorHelper<PlayerController>
{
    [SerializeField]
    private GameObject PointPrefab;
    [SerializeField]
    private GameObject[] Points;
    [SerializeField]
    private int PointAmount;
    [HideInInspector]
    public Transform PointsParent;
    [SerializeField]
    private float sensivity;

    bool aiming;
    [HideInInspector]
    public bool finish;

    //[HideInInspector]
    public bool CanMove;

    public float speed;

    private void Start()
    {
        //GameManager.Instance.player = gameObject;
        Points = new GameObject[PointAmount];
        for (int i = 0; i < PointAmount; i++)
        {
            Points[i] = Instantiate(PointPrefab, PointsParent);
        }
        StartCoroutine(PickABall());
    }

    private void Update()
    {
        if(CanMove)
        transform.position += transform.forward * speed * Time.deltaTime;

        if (ball || finish)
        {
          //  if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
                AimControl();

            if (aiming || finish)
            {
                for (int i = 0; i < PointAmount; i++)
                {
                   
                    Points[i].transform.position = PointsPosition(i * 0.1f);
                }
            }

        }
    }



    void AimControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            aiming = true;
            PointsParent.gameObject.SetActive(true);
        }
        if(Input.GetMouseButton(0))
        {
            float x = Mathf.Clamp(FixedTouchField.instance.TouchDist.x, -1, 1);
            float y = Mathf.Clamp(FixedTouchField.instance.TouchDist.y, -1, 1);
            //if((x>.3f||x<-.3f)&&y>.3f||y<-.3f)
            aimTransform.Rotate(-y * sensivity, x * sensivity, 0);
        }
        if (Input.GetMouseButtonUp(0))
        {
            aiming = false;
            if (!finish)
            {
                PointsParent.gameObject.SetActive(false);

                ForceBall();
                StartCoroutine(PickABall());
            }
        }
        //Touch t = Input.GetTouch(0);
        //switch (t.phase)
        //{
        //    case TouchPhase.Began:
        //        aiming = true;
        //        PointsParent.gameObject.SetActive(true);
        //        break;
        //    case TouchPhase.Moved:
        //        //float x = Mathf.Clamp(t.deltaPosition.x, -1, 1);
        //        //float y = Mathf.Clamp(t.deltaPosition.y, -1, 1);
        //        ////if((x>.3f||x<-.3f)&&y>.3f||y<-.3f)
        //        //aimTransform.Rotate(-y * sensivity, x * sensivity, 0);
        //        break;
        //    case TouchPhase.Stationary:
        //        break;
        //    case TouchPhase.Ended:
        //        //aiming = false;
        //        //if (!finish)
        //        //{
        //        //    PointsParent.gameObject.SetActive(false);

        //        //    ForceBall();
        //        //    StartCoroutine(PickABall());
        //        //}

        //        break;
        //    case TouchPhase.Canceled:
        //        break;
        //    default:
        //        break;
        //}
    }
    [HideInInspector]
    public GameObject ball;

    [SerializeField]
    private Transform BallsParent;
    public IEnumerator PickABall()
    {
        ball = null;
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < BallsParent.childCount; i++)
        {
            ball = BallsParent.GetChild(i).gameObject;

            if (!ball.activeInHierarchy)
            { ball.transform.localPosition = new Vector3(0, 3.55f, 0); ball.SetActive(true); break; }            
        }
        if(finish)
        ForceBall();
    } 
    void ForceBall()
    {
        GetComponent<Animator>().SetTrigger(Animator.StringToHash("KickBall"));
        ball.GetComponent<Rigidbody>().isKinematic = false;
        //ball.GetComponent<Rigidbody>().AddForce(aimTransform.forward * force * );
        ball.GetComponent<Rigidbody>().velocity = aimTransform.forward * force;
        if (finish)
            StartCoroutine(PickABall());
    }

    [SerializeField]
    private Transform aimTransform;
    [SerializeField]
    float force;
   Vector3 PointsPosition(float t)
    {
        Vector3 pointPos = transform.position + new Vector3(0,3f,5f)+ (aimTransform.forward * force * t) + .5f * Physics.gravity * (t * t);

        return pointPos;
    }
}
