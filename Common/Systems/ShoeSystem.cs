using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace shiretrmod.Common.Systems
{
    public class AccessorySystem : ModSystem
    {
        public static AccessorySystem Instance => ModContent.GetInstance<AccessorySystem>();

        private static List<int> _accessories = new List<int>();
        public static List<int> Accessories { get => _accessories; }

        public override void PostSetupContent()
        {
            foreach (var item in ContentSamples.ItemsByType.Values)
            {
                if (item.accessory)
                {
                    _accessories.Add(item.type);
                }
            }

            base.PostSetupContent();
        }

        public void RegisterAccessory(int type)
        {
            if (!_accessories.Contains(type))
            {
                _accessories.Add(type);
            }
        }
    }
}
