using UnityEngine;
using System.Collections;
using Assets;

public class ResourceScript : MonoBehaviour,IResourceEndpoint
{

    public ResourceUnitScript ResourcePrefab;

    public float SpawnInterval = 1;
    public float NextSpawn = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    public ResourceUnitScript TryTakeResource()
    {
        if (NextSpawn > Time.timeSinceLevelLoad)
            return null;

        NextSpawn = Time.timeSinceLevelLoad + SpawnInterval;

        return Instantiate(ResourcePrefab);
    }

    public bool TryStoreResource(ResourceUnitScript res)
    {
        return false;
    }
}
