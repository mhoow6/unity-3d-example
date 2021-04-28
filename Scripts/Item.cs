using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int itemScore = 0;
}

public interface IPatrolable
{
    void Patrol();
}

public class RotateItem : Item, IPatrolable
{
    public float rotateSpeed = 10.0f;

    public float patrolspeed = 2;
    public bool isPatrol = false;
    public List<Vector3> patrolPoints = new List<Vector3>();
    private int patrolIndex = 0;

    public int GetItemScore()
    {
        return itemScore;
    }

    public void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[patrolIndex], Time.deltaTime * patrolspeed);

        // Mod 0 is not allowed
        if (patrolIndex == 0 && transform.position == patrolPoints[patrolIndex])
            patrolIndex++;

        if (transform.position == patrolPoints[patrolIndex])
        {
            patrolIndex++;
            patrolIndex = patrolIndex % patrolPoints.Count;
        }
    }

    virtual public void Rotate()
    {
        transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime, Space.World);
    }
}

public class Coin : RotateItem
{
    private void Awake()
    {
        itemScore = 100;
        rotateSpeed = 30.0f;
    }

    private void Update()
    {
        Rotate();

        if (isPatrol)
            Patrol();
    }

}

public class Cube: RotateItem
{
    private void Awake()
    {
        itemScore = 500;
    }

    private void Update()
    {
        Rotate();

        if (isPatrol)
            Patrol();
    }

    public override void Rotate()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }

}



public class Finish : Item
{
    private void Awake()
    {
        itemScore = 0;
    }
}
