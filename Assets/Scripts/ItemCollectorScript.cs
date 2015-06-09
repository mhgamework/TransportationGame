using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;

public class ItemCollectorScript : MonoBehaviour
{

    private List<Item> itemsOnCollector = new List<Item>();
    public GameObject HighlightGameObject;
    public Material EmptyHighlightMaterial;
    public Material NotEmptyHighlightMaterial;

    // Use this for initialization
    void Start()
    {
        updateVisual();
    }

    // Update is called once per frame
    void Update()
    {
        if (itemsOnCollector.All(isValidItemOnGround)) return;
        itemsOnCollector = itemsOnCollector.Where(isValidItemOnGround).ToList();
        updateVisual();
    }

    private bool isValidItemOnGround(Item i)
    {
        return i != null && i.FreeInWorld;
    }

    void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item == null) return;
        itemsOnCollector.Add(item);

        updateVisual();
    }



    void OnTriggerExit(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item == null) return;
        itemsOnCollector.Remove(item);
        updateVisual();

    }

    private void updateVisual()
    {
        HighlightGameObject.GetComponent<MeshRenderer>().material = itemsOnCollector.Count == 0
                                                                        ? EmptyHighlightMaterial
                                                                        : NotEmptyHighlightMaterial;
    }

    public IEnumerable<Item> GetItems()
    {
        return itemsOnCollector;
    }
}
