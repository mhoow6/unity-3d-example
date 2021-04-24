using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int stage; // Scene 별로 직접 설정
    public Text stageText;

    public int stageItemCount; // Scene 별로 직접 설정
    public Text stageItemText;

    private int playerItemCount;
    public Text playerItemText;

    private int playerScore;
    public Text playerScoreText;

    public GameObject menuSet;
    public ResourceManager resourceManager;
    private Player player;

    private float freezeTimer;

    private void Awake()
    {
        // freeze Timer init
        freezeTimer = 0;

        // Component Check
        if (menuSet == null)
            Debug.Log("[Game Manager] You need to attach the UI");

        if (resourceManager == null)
            Debug.Log("[Game Manager] You need to attach the Resource Manager");

        // Waiting for Resource
        if (player == null)
            Debug.Log("[Game Manager] Waiting for Resource Manager.. (" + Time.time + ")");

        // UI Init
        stageText.text = "STAGE " + stage.ToString();
        stageItemText.text = "/ " + stageItemCount.ToString(); // Stage Item, Scene 별로 다름
        playerItemText.text = playerItemCount.ToString();
        playerScoreText.text = playerScore.ToString();

        // Get Player
        Invoke("GetPlayerFromResource", 1);
    }

    private void Update()
    {
        // Sub Menu
        if (Input.GetButtonDown("Cancel"))
            SubMenuActive();

        // Freeze Timer On
        if (freezeTimer <= 2.0f)
            freezeTimer += Time.deltaTime;

        // Freeze Off
        if (player != null && freezeTimer > 2.0f)
            player.isFreeze = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
            SceneManager.LoadScene(stage);
    }

    public void GetItemCount(int itemCount)
    {
        playerItemCount = itemCount;
        playerItemText.text = playerItemCount.ToString();
    }

    public void GetScore(int score)
    {
        playerScore = score;
        playerScoreText.text = playerScore.ToString();
    }

    public void SubMenuActive()
    {
        if (menuSet.activeSelf)
        {
            menuSet.SetActive(false);
            Time.timeScale = 1.0f;
        }
            
        else
        {
            menuSet.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void GetPlayerFromResource()
    {
        player = resourceManager.player;
        Debug.Log("[GameManager] Player attached. (" + Time.time + ")");
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
