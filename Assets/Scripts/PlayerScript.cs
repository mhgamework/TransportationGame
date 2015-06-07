using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class PlayerScript : MonoBehaviour
{

    public float MoveSpeed = 3;
    public float PickupRange;
    public CartScript Cart;


    private Vector3? moveTarget;

    private Transform modelChild;
    private Transform dudeChild;

    // Use this for initialization
    void Start()
    {
        modelChild = transform.FindChild("Model");
        dudeChild = modelChild.FindChild("Dude");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            tryMoveTo();

        moveToTarget();

        if (Cart != null) Cart.transform.parent = modelChild;


    }

    void OnDrawGizmos()
    {

    }

    private void moveToTarget()
    {
        if (moveTarget == null) return;
        moveTarget -= new Vector3(0, moveTarget.Value.y, 0);

        var diff = moveTarget.Value - transform.position;

        if (diff.magnitude < MoveSpeed * Time.deltaTime)
        {
            transform.position = moveTarget.Value;
            moveTarget = null;
            return;
        }

        transform.position += Vector3.Normalize(diff) * MoveSpeed * Time.deltaTime;


        modelChild.LookAt(moveTarget.Value);



    }

    private void tryMoveTo()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        moveTarget = hit.point;
    }



    public bool CanPickup(Item item)
    {
        if (Cart == null) return false;
        return Cart.CanAdd(item);
    }
    public void Pickup(Item item)
    {
        if (!CanPickup(item)) throw new InvalidOperationException();
        item.FreeInWorld = false;
        Cart.AddItems(item);
    }


    public bool inPickupRange(Transform t)
    {
        return Vector3.Distance(transform.position, t.position) <= PickupRange;
    }

    public void DropItem(Item item)
    {
        var dir = Random.onUnitSphere;
        dir.z = Math.Abs(dir.z);
        dir.y = 0;
        dir.Normalize();
        var pos = dudeChild.TransformPoint(dir * 2);

        item.transform.parent = null;
        item.transform.position = pos;
        item.FreeInWorld = true;
        item.gameObject.SetActive(true);

    }
}
