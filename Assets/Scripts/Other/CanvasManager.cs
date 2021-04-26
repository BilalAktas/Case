using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviorHelper<CanvasManager>
{
    
    public Transform TapToStartMenu;    
    public Transform FinishCollectMenu;
    public Transform FailedMenu;
    [SerializeField]
    private TextMeshProUGUI saved_coinText;
    [SerializeField]
    private TextMeshProUGUI levelEndCoinText;

    public void TapToStart()
    {
        TapToStartMenu.gameObject.SetActive(false);
        GameManager.Instance.player.GetComponent<PlayerController>().enabled = true;
        GameManager.Instance.ai.GetComponent<AI>().enabled = true;
    }

    public void EndOfLevelUpdateText(float endcoin)
    {
        FinishCollectMenu.gameObject.SetActive(true);
        levelEndCoinText.text = endcoin.ToString();
        saved_coinText.text = EncryptedPlayerPrefs.GetInt("Coin", 0).ToString();
    }

    public void CollectButton()
    {
        int coin = int.Parse(levelEndCoinText.text);
        int totalcoin = EncryptedPlayerPrefs.GetInt("Coin", 0) + coin;
        EncryptedPlayerPrefs.SetInt("Coin", totalcoin);
        saved_coinText.text = totalcoin.ToString();

        SceneManager.LoadSceneAsync(0);
    }

    public void Retry()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
