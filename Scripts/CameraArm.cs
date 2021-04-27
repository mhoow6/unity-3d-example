using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraArm : MonoBehaviour
{
    public Player player;
    public ResourceManager resourceManager;

    // Start is called before the first frame update
    private void Awake()
    {
        // Waiting for Resource
        if (player == null)
        {
            Debug.Log("[Camera Arm] Waiting for Resource Manager.. (" + Time.time + ")");

            // Get transform from Resource Manager
            Invoke("CatchPlayer", 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
        FollowPlayer();
    }

    void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = transform.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y;

        // Debug.Log($"x: {x}");

        if (x < 180f)
            x = Mathf.Clamp(x, -1f, 70f);
        if (x > 180f)
            x = Mathf.Clamp(x, 335f, 361f);

        transform.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }

    void FollowPlayer()
    {
        if (player != null)
            transform.position = player.gameObject.transform.position + new Vector3(0, 0.232f, 0);
    }

    void CatchPlayer()
    {
        player = resourceManager.player;
        player.isFreeze = false;
        Debug.Log("[Camera Arm] Player attached. (" + Time.time + ")");
    }
}
