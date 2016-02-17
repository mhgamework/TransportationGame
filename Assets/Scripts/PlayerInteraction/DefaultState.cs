using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Transportation
{
    public class DefaultState : BaseState
    {
        protected override IEnumerable<YieldInstruction> Run(PlayerInteractionScript ps)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                // Set tower
                var hits = ps.raycastMouseClick();
                var groundPlane = hits.FirstOrDefault(n => n.collider.GetComponentInParent<GroundPlaneScript>() != null);
                if (groundPlane.collider != null)
                {
                    var tower =Object.Instantiate(ps.TowerPrefab);
                    tower.transform.position = groundPlane.point;
                }



            }

            if (Input.GetMouseButtonUp(0))
            {
                var hits = ps.raycastMouseClick();

                foreach (var hit in hits)
                {
                    var cs = hit.collider.GetComponentInParent<ConnectorScript>();
                    if (cs)
                    {
                        ps.ChangeState(new BuildRoadState(cs, ps));
                        yield break;
                    }

                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                var road = ps.raycastMouseClick().Select(h => h.collider.GetComponentInParent<RoadScript>()).FirstOrDefault(n => n != null);
                if (road)
                {
                    road.RemoveRoad();
                    yield break;
                }
            }


        }


    }
}