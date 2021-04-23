using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float rotateSpeed;
    public int itemScore;

    private void Awake()
    {
        rotateSpeed = 30.0f;
    }

    private void Update()
    {
        transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime, Space.World);
    }
}
