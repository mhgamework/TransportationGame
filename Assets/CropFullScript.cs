using Assets.Model;
using Assets.Scripts;
using UnityEngine;
using System.Collections;

public class CropFullScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseUp()
    {
        if (!Input.GetMouseButtonUp(0)) return;

        var p = LocalGameService.Get.Player;

        var item = new MItem(MItemType.Wheat);

        if (!p.CanAdd(item)) return;
        if (!p.inPickupRange(transform)) return;

        p.AddItems(item);

        transform.GetComponentInParent<CropScript>().GrowthPercentage = 0;
    }
}
