using UnityEngine;

namespace Assets.Model
{
    public class MItemType
    {
        static MItemType()
        {
            Wood = new MItemType() { Name = "Wood" };
        }
        public static MItemType Wood { get; private set; }

        public Material Material;
        public string Name;
    }
}