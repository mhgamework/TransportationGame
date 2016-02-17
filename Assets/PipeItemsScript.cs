using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Assets.Scripts.Transportation;

public class PipeItemsScript : MonoBehaviour
{
    private RoadScript road;

    private List<Element> elements = new List<Element>();

    public float TransportSpeed = 1;

    // Use this for initialization
    void Start()
    {
        road = GetComponent<RoadScript>();
    }

    // Update is called once per frame
    void Update()
    {

        if (road.EndpointA != null && road.EndpointA.OwnedBy != null)
        {
            // Try take
            var res = ((IResourceEndpoint)road.EndpointA.OwnedBy.GetComponent(typeof(IResourceEndpoint))).TryTakeResource();
            if (res != null)
                elements.Add(new Element() { Unit = res, Position = 0 });
        }


        foreach (var el in elements)
        {
            el.Position += Time.deltaTime * TransportSpeed;
            var a = road.EndpointA.transform.position;
            var b = road.EndpointB.transform.position;
            el.Unit.transform.position = Vector3.Lerp(a, b, el.Position / (a - b).magnitude);
        }
    }

    public class Element
    {
        public ResourceUnitScript Unit;
        public float Position;
    }


}
