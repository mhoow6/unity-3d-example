using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCamera : MonoBehaviour
{
    public Transform playerTransform;
    public ResourceManager resourceManager;

    Vector3 offset;

    private void Awake()
    {
        // Waiting for Resource
        if (playerTransform == null)
        {
            Debug.Log("[Custom Camera] Waiting for Resource Manager.. (" + Time.time + ")");
        }

        // Get transform from Resource Manager
        Invoke("CatchPlayer", 1);

    }

    private void LateUpdate()
    {
        if (playerTransform != null)
            transform.position = playerTransform.position + offset;
    }

    void CatchPlayer()
    {
        playerTransform = resourceManager.player.transform;
        offset = transform.position - playerTransform.position;
        Debug.Log("[Custom Camera] Player attached. (" + Time.time + ")");
    }
}
