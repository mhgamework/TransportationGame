using UnityEngine;

namespace Assets.Model
{
    public class MItem
    {
        public MItemType Type { get; private set; }

        public MItem(MItemType type)
        {
            Type = type;
        }
    }
}