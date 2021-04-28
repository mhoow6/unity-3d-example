using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraArm : MonoBehaviour
{
    public Player player;
    public ResourceManager resourceManager;

    float zoom;
    float zoomMin;
    float zoomMax;
    public float zoomSensitivity;
    public float zoomSpeed;

    private void Awake()
    {
        // Waiting for Resource
        if (player == null)
        {
            Debug.Log("[Camera Arm] Waiting for Resource Manager.. (" + Time.time + ")");

            // Get transform from Resource Manager
            Invoke("CatchPlayer", 1);
        }

        // Zoom
        zoom = transform.position.z;
        zoomMin = -1f;
        zoomMax = 1f;
        zoomSensitivity = 0.5f;
        zoomSpeed = 5.0f;
    }

    void LateUpdate()
    {
        FollowPlayer();
        LookAround();
        ZoomOut();
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
            transform.position = player.gameObject.transform.position + new Vector3(0, 0.232f, 0) + ZoomOut();
    }

    void CatchPlayer()
    {
        player = resourceManager.player;
        player.isFreeze = false;
        Debug.Log("[Camera Arm] Player attached. (" + Time.time + ")");
    }

    Vector3 ZoomOut()
    {
        // 스크롤 시 얻는 값
        zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
        zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);

        // 전방벡터을 기준으로 확대/축소
        Vector3 result = transform.forward * zoom;

        // Vector3 반환
        return result;
    }
}
