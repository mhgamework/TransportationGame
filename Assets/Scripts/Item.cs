using System;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Item : MonoBehaviour
{
    public string ItemTypeStringID;
    public bool FreeInWorld;

    // Use this for initialization
    void Start()
    {

    }

    void OnEnable()    {
        StartCoroutine(randomizePlayback().GetEnumerator());

    }

    private IEnumerable<YieldInstruction> randomizePlayback()
    {
        transform.FindChild("Model").GetComponent<Animator>().enabled = false;
        yield return new WaitForSeconds(Random.Range(0f, 3f));
        transform.FindChild("Model").GetComponent<Animator>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseUp()
    {
        if (!FreeInWorld) return;
        Debug.Log("Click!");
        if (!Input.GetMouseButtonUp(0)) return;

        var p = LocalGameService.Get.Player;
        if (!p.CanPickup(GetItem())) return;

        if (!p.inPickupRange(transform)) return;

        p.Pickup(GetItem());
    }

    public Item GetItem()
    {
        if (!gameObject) throw new InvalidOperationException("Destroyed!");
        return this;
    }


    public GameObject GetModel()
    {
        return transform.FindChild("Model").gameObject;
    }

    public bool HasSameType(Item item)
    {
        return ItemTypeStringID == item.ItemTypeStringID;
    }
}
