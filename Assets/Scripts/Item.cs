using System;
using System.Collections.Generic;
using Assets.Model;
using Assets.Scripts;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Item : MonoBehaviour
{




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
        Debug.Log("Click!");
        if (!Input.GetMouseButtonUp(0)) return;

        var p = LocalGameService.Get.Player;
        if (!p.CanAdd(GetMItem())) return;

        if (!p.inPickupRange(transform)) return;

        p.AddItems(GetMItem());
        Destroy(gameObject);
    }

    public MItem GetMItem()
    {
        if (!gameObject) throw new InvalidOperationException("Destroyed!");
        return new MItem(MItemType.Wood);
    }
}
