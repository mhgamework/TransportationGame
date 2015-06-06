using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class StorageScript : MonoBehaviour
{

    public List<Slot> Slots; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [Serializable]
    public class Slot
    {
        public Item ItemPrefab;
        public int Amount;
    }
}
