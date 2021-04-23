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

    private int playerItemCount; // private
    public Text playerItemText;

    private int playerScore; // private, 누적되게할려면 특별한 방법이 필요하다..
    public Text playerScoreText;

    public GameObject menuSet;
    public ResourceManager resourceManager;
    private Player player;

    private void Awake()
    {
        stageText.text = "STAGE " + stage.ToString();

        // Stage Item, Scene 별로 다름
        stageItemText.text = "/ " + stageItemCount.ToString();

        // Player Item Count
        playerItemText.text = playerItemCount.ToString();

        // Player Score
        playerScoreText.text = playerScore.ToString();

        // Waiting for Resource
        if (player == null)
            Debug.Log("[GameManager] Waiting for Resource Manager..");

        // Get Player with Loading
        Invoke("GetPlayer", 1);

    }

    private void Update()
    {
        // Sub Menu
        if (Input.GetButtonDown("Cancel"))
            SubMenuActive();
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

    public void GameSave()
    {
        // Set Player Position
        PlayerPrefs.SetFloat("playerX", player.gameObject.transform.position.x);
        PlayerPrefs.SetFloat("playerY", player.gameObject.transform.position.y);
        PlayerPrefs.SetFloat("playerZ", player.gameObject.transform.position.z);

        // Set Player Item Count & Score
        PlayerPrefs.SetInt("playerItemCount", player.itemCount);
        PlayerPrefs.SetInt("playerScore", player.score);

        // menuSet deactive
        menuSet.SetActive(false);
    }

    public void GameLoad()
    {
        // Save Fail
        if (!PlayerPrefs.HasKey("playerX"))
        {
            Debug.Log("[GameManager] Save Failed.");
            return;
        }
            

        // Player Pos
        float x = PlayerPrefs.GetFloat("playerX");
        float y = PlayerPrefs.GetFloat("playerY");
        float z = PlayerPrefs.GetFloat("playerZ");
        player.transform.position = new Vector3(x, y, z);

        // Get Player Item Count & Score
        playerItemCount = PlayerPrefs.GetInt("playerItemCount");
        playerScore = PlayerPrefs.GetInt("playerScore");
    }

    public void GameExit()
    {
        Application.Quit();
    }

    void GetPlayer()
    {
        player = resourceManager.player;
        Debug.Log("[GameManager] Player attached.");

        // Load Game
        GameLoad();
    }
}
