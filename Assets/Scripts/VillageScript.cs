using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;

public class VillageScript : MonoBehaviour
{
    public GameObject HousesParent;
    public Item WoodItemPrefab;
    public Item FlourItemPrefab;
    public ItemCollectorScript ItemCollector;
    public Material HouseEmptyMaterial;
    public Material HouseFullMaterial;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(trySupply().GetEnumerator());

        for (int i = 0; i < HousesParent.transform.childCount; i++)
        {
            HousesParent.transform.GetChild(i).GetComponent<MeshRenderer>().material = HouseEmptyMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerable<YieldInstruction> trySupply()
    {
        while (this != null)
        {
            var woodItem = ItemCollector.GetItems().FirstOrDefault(i => i.HasSameType(WoodItemPrefab));
            var flourItem = ItemCollector.GetItems().FirstOrDefault(i => i.HasSameType(FlourItemPrefab));
            if (woodItem != null && flourItem != null)
            {
                for (int i = 0; i < HousesParent.transform.childCount; i++)
                {
                    var house = HousesParent.transform.GetChild(i);
                    if (house.GetComponent<MeshRenderer>().sharedMaterial != HouseEmptyMaterial) continue;

                    Destroy(woodItem.gameObject);
                    Destroy(flourItem.gameObject);

                    StartCoroutine(peopleLiveInHouse(house).GetEnumerator());
                    break;

                }
            }



            yield return new WaitForSeconds(1);
        }


    }

    IEnumerable<YieldInstruction> peopleLiveInHouse(Transform house)
    {
        house.GetComponent<MeshRenderer>().material = HouseFullMaterial;
        yield return new WaitForSeconds(5);
        house.GetComponent<MeshRenderer>().material = HouseEmptyMaterial;

    }

}
