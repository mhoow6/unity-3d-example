using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // Prefabs
    Player _player;
    Item coin;
    Item cube;
    Item finish;
    GameObject terrain;

    public GameManager manager;
    public Transform cameraArm;
    public PoolManager pool;

    public Player player
    {
        get => _player;
    }

    private void Awake()
    {
        // Component Check
        if (manager == null)
            Debug.Log("[Resource Manager] You need to attach the Game Manager");
    }


    private void Start()
    {
        CreatePlayer();
        CreateTerrain();
        CreateItem();
    }

    public void CreatePlayer()
    {
        if (manager.stage == 0)
        {
            _player = Instantiate(Resources.Load<Player>("Player"));
            _player.manager = manager;
            _player.cameraArm = cameraArm;

            _player.name = "Player";
            _player.transform.position = new Vector3(0.03785592f, 1.229101f, 0.6858789f);
        }
    }

    public void CreateTerrain()
    {
        if (manager.stage == 0)
        {
            terrain = Instantiate(Resources.Load<GameObject>("Terrain"));
            terrain.name = "Terrain";
            terrain.transform.localScale = new Vector3(11.381f, 1f, 13.994f);
        }
    }

    public void CreateItem()
    {
        if (manager.stage == 0)
        {
            coin = pool.CreateItem("Coin");
            cube = pool.CreateItem("Cube");
            finish = pool.CreateItem("Finish");
            finish.isItem = false;

            coin.transform.position = new Vector3(-3.1f, 1.58f, 2.55f);
            coin.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            cube.transform.position = new Vector3(3.83f, 1.64f, 3.48f);

            finish.transform.position = new Vector3(0.06f, 1.569f, 6.401f);
        }   
    }
}
