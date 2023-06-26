using shiretrmod.Common.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace shiretrmod.Common.UI
{
    public class AccessorySlot : ModAccessorySlot
    {
        public override string Name => "AccessorySlot";

        public override string FunctionalTexture => "Terraria/Images/Item_";

        public override bool CanAcceptItem(Item checkItem, AccessorySlotType context)
        {
            return AccessorySystem.Accessories.Contains(checkItem.type);
        }

        public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo)
        {
            return AccessorySystem.Accessories.Contains(item.type);
        }

        public override bool IsVisibleWhenNotEnabled()
        {
            return false;
        }

        public override void OnMouseHover(AccessorySlotType context)
        {
            Main.hoverItemName = context switch
            {
                AccessorySlotType.FunctionalSlot => "Accessory",
                AccessorySlotType.VanitySlot => "Vanity Accessory",
                AccessorySlotType.DyeSlot => "Dye",
                _ => "Unknown"
            };

            base.OnMouseHover(context);
        }
    }
}
