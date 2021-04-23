using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody rigid;
    public AudioSource scoreAudio;
    public GameManager manager;

    public int itemCount;
    public int score;

    bool isJump;
    private float jumpPower;

    private void Awake()
    {
        // Component Check
        if (rigid == null)
            Debug.Log("You need to attach the rigidbody component.");

        if (scoreAudio == null)
            Debug.Log("You need to attach the audio component.");

        // Jump
        jumpPower = 25f;
        isJump = false;

        // Item
        itemCount = 0;

        // Score
        score = 0;
    }

    void Update()
    {
        // Jump
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
            isJump = true;
        }

    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Move
        rigid.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);

    }

    private void OnCollisionEnter(Collision collision)
    {
        // Jump
        if (collision.gameObject.name == "Terrain")
            isJump = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Get Coin
        if (other.name == "Coin")
        {
            // Player gets score and itemCount
            itemCount++;
            score += 100;
            manager.GetItemCount(itemCount);
            manager.GetScore(score);

            // Sound Play
            GetComponent<AudioSource>().Play();

            // DeActive
            other.gameObject.SetActive(false);
        }
        
        // Get Cube
        if (other.name == "Cube")
        {
            // Player gets score
            score += 500;
            manager.GetScore(score);

            // Sound Play
            GetComponent<AudioSource>().Play();

            // DeActive
            other.gameObject.SetActive(false);
        }

        if (other.name == "Finish" && itemCount == manager.stageItemCount)
            SceneManager.LoadScene(++manager.stage);
    }
}
