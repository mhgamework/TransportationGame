using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Transportation
{
    public class PlayerInteractionScript : MonoBehaviour
    {
        public RoadScript RoadPrefab;
        public ConnectorScript ConnectorPrefab;
        public Camera PlayerCamera;
        private Coroutine stateRoutine;

        public void OnEnable()
        {
            if (!RoadPrefab || !PlayerCamera || !ConnectorPrefab) throw new InvalidOperationException();
            ChangeState(new DefaultState());
        }

        public void Update()
        {

        }

        public void ChangeState(BaseState state)
        {

            if (stateRoutine != null)
                StopCoroutine(stateRoutine);
            stateRoutine = StartCoroutine(state.GetCoroutine(this));
        }

        public RaycastHit[] raycastMouseClick()
        {
            return Physics.RaycastAll(PlayerCamera.ScreenPointToRay(Input.mousePosition));
        }
    }
}