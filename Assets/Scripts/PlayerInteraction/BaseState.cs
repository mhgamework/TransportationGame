using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Transportation
{
    public abstract class BaseState
    {
        public IEnumerator<YieldInstruction> GetCoroutine(PlayerInteractionScript ps)
        {
            return privateRun(ps).GetEnumerator();
        }

        private IEnumerable<YieldInstruction> privateRun(PlayerInteractionScript ps)
        {
            yield return new WaitForFixedUpdate(); // To prevent state change loops to freeze the engine
            while (true)
            {
                yield return ps.StartCoroutine(Run(ps).GetEnumerator());
            }
        }

        protected abstract IEnumerable<YieldInstruction> Run(PlayerInteractionScript ps);
    }
}