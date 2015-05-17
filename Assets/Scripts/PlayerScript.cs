using System;
using Assets.Model;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class PlayerScript : MonoBehaviour
{

    public float MoveSpeed = 3;
    public float PickupRange;
    public CartScript Cart;

    public GameObject ItemPrefab;


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



    public bool CanAdd(params MItem[] items)
    {
        if (Cart == null) return false;
        return Cart.CanAdd(items);
    }
    public void AddItems(params MItem[] items)
    {
        if (!CanAdd(items)) throw new InvalidOperationException();
        Cart.AddItems(items);
    }


    public bool inPickupRange(Transform t)
    {
        return Vector3.Distance(transform.position, t.position) <= PickupRange;
    }

    public void DropItem(MItem item)
    {

        var dir = Random.onUnitSphere;
        dir.z = Math.Abs(dir.z);
        dir.y = 0;
        dir.Normalize();
        var pos = dudeChild.TransformPoint(dir * 2);
        Instantiate(ItemPrefab, pos, Quaternion.identity);
    }
}
