using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody rigid;
    public AudioSource scoreAudio;
    public GameManager manager;
    public Transform cameraArm;

    public int itemCount;
    public int score;

    public bool isJump;
    public float jumpPower;

    public bool isFreeze;

    private void Awake()
    {
        // Component Check
        if (rigid == null)
            Debug.Log("You need to attach the rigidbody component.");

        if (scoreAudio == null)
            Debug.Log("You need to attach the audio component.");

        if (manager == null)
            Debug.Log("You need to attach the Game Manager.");

        if (cameraArm == null)
            Debug.Log("You need to attach the Camera Arm.");

        // Jump
        jumpPower = 25f;
        isJump = false;

        // Item
        itemCount = 0;

        // Score
        score = 0;

        // Freeze
        isFreeze = true;
    }

    private void Update()
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
        if (!isFreeze)
        {
            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector3 lookForawrd = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForawrd * moveInput.y + lookRight * moveInput.x;

            // 바라보는 방향으로 이동
            rigid.AddForce(moveDir, ForceMode.Impulse);
        }  
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
            scoreAudio.Play();

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
            scoreAudio.Play();

            // DeActive
            other.gameObject.SetActive(false);
        }

        if (other.name == "Finish" && itemCount == manager.stageItemCount)
        {
            // Next Stage
            SceneManager.LoadScene(++manager.stage);
        }
            
    }

}
