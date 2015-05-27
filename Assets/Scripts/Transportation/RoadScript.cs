using UnityEngine;

namespace Assets.Scripts.Transportation
{
    [ExecuteInEditMode]
    public class RoadScript : MonoBehaviour
    {
        public ConnectorScript EndpointA;
        public ConnectorScript EndpointB;

        public void Update()
        {
            if (!EndpointA || !EndpointB) return;
            var dir = EndpointB.Position - EndpointA.Position;

            transform.position = EndpointA.Position;
            transform.LookAt(EndpointB.Position);
            transform.localScale = new Vector3(1,1,dir.magnitude);

        }
    }
}