using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Transportation
{
    public class ConnectorScript : MonoBehaviour
    {
        public Vector3 Position { get { return transform.position; } }

        public GameObject OwnedBy;

        public void RemoveIfUnused()
        {
            if (GameObject.FindObjectsOfType<RoadScript>().Any(r => r.EndpointA == this || r.EndpointB == this)) return;
            if (OwnedBy) return;
            Destroy(gameObject);
        }
    }
}