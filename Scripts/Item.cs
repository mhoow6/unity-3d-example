using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float rotateSpeed;
    public int itemScore;
    public bool isItem;

    private void Awake()
    {
        rotateSpeed = 30.0f;
        isItem = true;
    }

    private void Update()
    {
        if (isItem) 
            transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime, Space.World);
    }
}
