using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;
using Assets.Scripts;

public class VillageScript : MonoBehaviour
{
    public GameObject HousesParent;
    public Item WoodItemPrefab;
    public Item FlourItemPrefab;
    public ItemCollectorScript ItemCollector;
    public Material HouseEmptyMaterial;
    public Material HouseFullMaterial;
    private int housesProvided;

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
            yield return new WaitForFixedUpdate();

            var woodItem = ItemCollector.GetItems().FirstOrDefault(i => i.HasSameType(WoodItemPrefab));
            var flourItem = ItemCollector.GetItems().FirstOrDefault(i => i.HasSameType(FlourItemPrefab));
            if (woodItem == null || flourItem == null)
                continue;
            bool itemsDestroyed = false;
            for (int i = 0; i < 4; i++)
            {
                var house = getEmptyHouses().FirstOrDefault();
                if (house == null) break;
                if (!itemsDestroyed)
                {
                    Destroy(woodItem.gameObject);
                    Destroy(flourItem.gameObject);
                    itemsDestroyed = true;
                }
                house.GetComponent<MeshRenderer>().material = HouseFullMaterial;
                housesProvided++;
                yield return new WaitForSeconds(1);
            }
          

        }


    }

    private IEnumerable<Transform> getEmptyHouses()
    {
        return
            HousesParent.transform.GetChildren()
                        .Where(c => c.GetComponent<MeshRenderer>().sharedMaterial == HouseEmptyMaterial);
    }

    public bool IsVillageProvided()
    {
        return HousesParent.transform.childCount <= housesProvided;
    }

}
