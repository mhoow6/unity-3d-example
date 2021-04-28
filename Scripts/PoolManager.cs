using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    List<Item> items = new List<Item>();

    public Transform itemParent;

    private void Awake()
    {
        if (itemParent == null)
        {
            itemParent = new GameObject("itemParent").transform;
        }
    }

    public Item CreateItem(string _name)
    {
        if (_name != null)
        {
            Item usableItem = items.Find(item => (item.gameObject.activeSelf == false) && (item.gameObject.name == _name));

            if (usableItem != null)
            {
                usableItem.gameObject.SetActive(true);
                return usableItem;
            }

            GameObject item = Instantiate(Resources.Load<GameObject>(_name));

            Item itemScript = null;
            
            if (_name == "Coin")
                itemScript = item.AddComponent<Coin>();
            else if (_name == "Cube")
                itemScript = item.AddComponent<Cube>();
            else if (_name == "Finish")
                itemScript = item.AddComponent<Finish>();

            itemScript.gameObject.name = _name;

            item.transform.SetParent(itemParent);

            items.Add(itemScript);

            return itemScript;
        }

        return null;
    }
}
