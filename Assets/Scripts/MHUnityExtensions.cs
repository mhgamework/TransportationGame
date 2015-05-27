using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public static class MHUnityExtensions
    {
         public static IEnumerable<Transform> GetChildren(this Transform t)
         {
             for (int i = 0; i < t.childCount; i++) yield return t.GetChild(i);
         }
    }
}