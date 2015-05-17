using UnityEngine;

namespace Assets.Model
{
    public class MItemType
    {
        static MItemType()
        {
            Wood = new MItemType() { Name = "Wood" };
            Wheat = new MItemType() { Name = "Wheat" };
        }
        public static MItemType Wood { get; private set; }
        public static MItemType Wheat { get; private set; }

        public Material Material;
        public string Name;
    }
}