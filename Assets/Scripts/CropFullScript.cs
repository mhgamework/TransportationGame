﻿using Assets.Scripts;
using UnityEngine;
using System.Collections;

public class CropFullScript : MonoBehaviour
{
    public Item ItemPrefab;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseUp()
    {
        if (!Input.GetMouseButtonUp(0)) return;

        var p = LocalGameService.Get.Player;

        var item = Instantiate(ItemPrefab.gameObject).GetComponent<Item>();
        item.gameObject.SetActive(false);

        if (!p.CanPickup(item)) return;
        DestroyImmediate(item.gameObject);
        p.moveInPickupRange(transform, () =>
        {
            item = Instantiate(ItemPrefab.gameObject).GetComponent<Item>();

            p.Pickup(item);

            transform.GetComponentInParent<CropScript>().GrowthPercentage = 0;
        });


    }
}
