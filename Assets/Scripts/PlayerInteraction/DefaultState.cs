using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Transportation
{
    public class DefaultState : BaseState
    {
        protected override IEnumerable<YieldInstruction> Run(PlayerInteractionScript ps)
        {
            if (Input.GetMouseButtonUp(0))
            {
                var hits = ps.raycastMouseClick();

                foreach (var hit in hits)
                {
                    var cs = hit.collider.GetComponentInParent<ConnectorScript>();
                    if (cs)
                    {
                        ps.ChangeState(new BuildRoadState(cs));
                        yield break;
                    }

                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                var road = ps.raycastMouseClick().Select(h => h.collider.GetComponentInParent<RoadScript>()).FirstOrDefault(n => n != null);
                if (road)
                {
                    GameObject.Destroy(road.gameObject);
                    yield break;
                }
            }


        }


    }
}