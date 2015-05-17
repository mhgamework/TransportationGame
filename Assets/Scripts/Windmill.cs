using UnityEngine;
using System.Collections;

public class Windmill : MonoBehaviour
{

    public GameObject Rotor;
    public float TurnSpeed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Rotor.transform.rotation *= Quaternion.AngleAxis(TurnSpeed, new Vector3(1, 0, 0));
    }
}
