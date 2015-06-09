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
    private Action onArrivedAtMoveTarget;

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
            tryMoveToPositionTargetedByMouse();

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
            if (onArrivedAtMoveTarget != null) onArrivedAtMoveTarget();
            onArrivedAtMoveTarget = null;
            return;
        }

        transform.position += Vector3.Normalize(diff) * MoveSpeed * Time.deltaTime;


        modelChild.LookAt(moveTarget.Value);



    }

    private void tryMoveToPositionTargetedByMouse()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        moveTo(hit.point);
    }

    private void moveTo(Vector3 position)
    {
        moveTarget = position;
        onArrivedAtMoveTarget = null;
    }
    private void moveTo(Vector3 position, Action onArrive)
    {
        moveTarget = position;
        onArrivedAtMoveTarget = onArrive;
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
        item.GetComponent<HighlightScript>().HideHighlight();// HACK

        Cart.AddItems(item);
    }


    /// <summary>
    /// Moves in pickup range and executes action, if already in range executes action
    /// </summary>
    /// <param name="t"></param>
    /// <param name="onArrive"></param>
    public void moveInPickupRange(Transform t, Action onArrive)
    {
        if (inPickupRange(t))
        {
            onArrive();
            return;
        }

        var diff = t.position - transform.position;
        diff = diff.normalized * (diff.magnitude - PickupRange + 0.5f);
        moveTo(transform.position + diff, onArrive);
    }
    public bool inPickupRange(Transform t)
    {
        return Vector3.Distance(transform.position, t.position) <= PickupRange;
    }

    public void DropItem(Item item)
    {

        var dir = new Vector3();
        dir.z = 2f;
        dir.x = Random.Range(-0.4f, 0.4f);
        var pos = dudeChild.TransformPoint(dir);

        item.transform.parent = null;
        item.transform.position = pos;
        item.FreeInWorld = true;
        item.gameObject.SetActive(true);

        item.GetComponent<HighlightScript>().HideHighlight();// HACK

    }
}
