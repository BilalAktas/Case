using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviorHelper<GameManager>
{
//    [HideInInspector]
    public GameObject player;
//    [HideInInspector]
    public GameObject ai;

    [SerializeField]
    private Transform Levels;

   // [HideInInspector]
    public bool finish;

    public Image AIIMG;
    public Image PlayerIMG;
    public Transform PlayerFinish;
    public Transform AIFinish;
    [SerializeField]
    private TextMeshProUGUI PLevel;
    [SerializeField]
    private TextMeshProUGUI NLevel;

    private void Awake()
    {
        StartLevel();
    }

    private void Update()
    {
        WhoIsKing();
    }

    void WhoIsKing()
    {
        if (!finish)
        {
            if (player.transform.position.z > ai.transform.position.z)
            {
                player.transform.GetChild(player.transform.childCount - 1).GetChild(0).gameObject.SetActive(true);
                ai.transform.GetChild(ai.transform.childCount - 1).GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                player.transform.GetChild(player.transform.childCount - 1).GetChild(0).gameObject.SetActive(false);
                ai.transform.GetChild(ai.transform.childCount - 1).GetChild(0).gameObject.SetActive(true);
            }

           // Debug.Log(Vector3.Distance(player.transform.position, PlayerFinish.position));
            TopList();
        }
     
    }

    void TopList()
    {
        #region PLAYER

        float dist = Vector3.Distance(player.transform.position, PlayerFinish.position);
        float a = 203 - dist;
        float s = (-194 + a)+87;
        PlayerIMG.rectTransform.localPosition = new Vector3(s, 995, 0);

        #endregion

        #region AI

        float dist2 = Vector3.Distance(ai.transform.position, AIFinish.position);
        float a2 = 219 - dist2;
        float s2 = (-194 + a2)+71;
        AIIMG.rectTransform.localPosition = new Vector3(s2, 995, 0);

        #endregion
    }



    public void StartLevel()
    {
        int currentLevel =  EncryptedPlayerPrefs.GetInt("Level", 1);   
        Levels.GetChild(currentLevel - 1).gameObject.SetActive(true);
        Transform levelParent = Levels.GetChild(currentLevel - 1);
        Vector3 p_StartPos = levelParent.GetChild(0).position;
        Vector3 a_StartPos = levelParent.GetChild(1).position;
        player.transform.position = p_StartPos;
        ai.transform.position = a_StartPos;
        player.GetComponent<PlayerController>().enabled = false;
        ai.GetComponent<AI>().enabled = false;
        CanvasManager.Instance.TapToStartMenu.gameObject.SetActive(true);
        CanvasManager.Instance.FinishCollectMenu.gameObject.SetActive(false);
        PLevel.text = EncryptedPlayerPrefs.GetInt("Level", 1).ToString();
        NLevel.text = (EncryptedPlayerPrefs.GetInt("Level", 1) + 1).ToString();
        PlayerFinish = Levels.GetChild(currentLevel - 1).GetChild(3).GetChild(1);
        AIFinish = Levels.GetChild(currentLevel - 1).GetChild(3).GetChild(0);
    }

    public void FinishGame(string winner)
    {        
       
        PlayerController.Instance.finish = true;
       finish = true;
     
      
      
        if (PlayerController.Instance.ball)
            PlayerController.Instance.ball.SetActive(false);

        if (winner == "Player")
        {
            AI.Instance.enabled = false;
        }
        else
        {
            AI.Instance.enabled = false;
            CanvasManager.Instance.FailedMenu.gameObject.SetActive(true);
            PlayerController.Instance.enabled = false;
            PlayerController.Instance.PointsParent.gameObject.SetActive(false);
        }
    }
}
