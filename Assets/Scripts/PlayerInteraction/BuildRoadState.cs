using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Transportation
{
    public class BuildRoadState : BaseState
    {
        private readonly ConnectorScript startConnector;

        public BuildRoadState(ConnectorScript startConnector)
        {
            this.startConnector = startConnector;
        }

        protected override IEnumerable<YieldInstruction> Run(PlayerInteractionScript ps)
        {
            while (!Input.GetMouseButtonUp(0)) yield return new WaitForFixedUpdate();
            var hits = ps.raycastMouseClick();
            foreach (var hit in hits)
            {
                var cs = hit.collider.GetComponentInParent<ConnectorScript>();
                if (cs)
                {
                    // End road
                    var newRoad = Object.Instantiate(ps.RoadPrefab);
                    newRoad.EndpointA = startConnector;
                    newRoad.EndpointB = cs;
                    ps.ChangeState(new DefaultState());
                    yield break;

                }

            }
        }
    }
}