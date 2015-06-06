using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Transportation
{
    public class BuildRoadState : BaseState
    {
        private ConnectorScript startConnector;
        private ConnectorScript endConnector;
        private RoadScript activeRoad;


        public BuildRoadState(ConnectorScript startConnector, PlayerInteractionScript ps)
        {
            this.startConnector = startConnector;
            endConnector = GameObject.Instantiate(ps.ConnectorPrefab);
            activeRoad = GameObject.Instantiate(ps.RoadPrefab);
            activeRoad.EndpointA = startConnector;
            activeRoad.EndpointB = endConnector;

        }

        protected override IEnumerable<YieldInstruction> Run(PlayerInteractionScript ps)
        {
            var hits = ps.raycastMouseClick();

            var groundPlane = hits.FirstOrDefault(n => n.collider.GetComponentInParent<GroundPlaneScript>() != null);
            if (groundPlane.collider != null)
                endConnector.transform.position = groundPlane.point;

            if (Input.GetMouseButtonUp(0))
            {
                foreach (var hit in hits)
                {
                    var cs = hit.collider.GetComponentInParent<ConnectorScript>();
                    if (cs == endConnector) continue;
                    if (cs)
                    {
                        // End road
                        activeRoad.EndpointB = cs;
                        GameObject.Destroy(endConnector.gameObject);
                        ps.ChangeState(new DefaultState());
                        yield break;

                    }

                }

                // Not clicked on anything relevant
                if (groundPlane.collider != null)
                {
                    // place on ground, already done that so we are done
                    ps.ChangeState(new DefaultState());
                    yield break;
                }
            }

        }
    }
}