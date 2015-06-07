using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;

public class Windmill : MonoBehaviour
{
    public Item WheatItemPrefab;
    public Item FlourItemPrefab;
    public ItemCollectorScript ItemCollector;
    public GameObject Rotor;
    public float TurnSpeed;
    private bool working = false;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(makeFlour().GetEnumerator());
    }

    // Update is called once per frame
    void Update()
    {
        if (working)
            Rotor.transform.rotation *= Quaternion.AngleAxis(TurnSpeed, new Vector3(1, 0, 0));

    }


    private IEnumerable<YieldInstruction> makeFlour()
    {

        while (this != null)
        {
            yield return new WaitForFixedUpdate();
            var item = ItemCollector.GetItems().FirstOrDefault(i => i.HasSameType(WheatItemPrefab));
            if (item == null) continue;

            DestroyImmediate(item.gameObject);

            working = true;
            yield return new WaitForSeconds(3);
            working = false;

            var flourItem = Instantiate(FlourItemPrefab);
            flourItem.transform.position = transform.position + new Vector3(5, 0, 0) + Random.insideUnitSphere;
            flourItem.transform.position = new Vector3(flourItem.transform.position.x, 0, flourItem.transform.position.z);

            yield return new WaitForSeconds(0.5f);
        }


    }


}
