using shiretrmod.Common.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace shiretrmod.Common.UI
{
    public class AccessorySlot1 : ModAccessorySlot
    {
        public override string Name => "AccessorySlot1";

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

    public class AccessorySlot2 : ModAccessorySlot
    {
        public override string Name => "AccessorySlot2";

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
    public class AccessorySlot3 : ModAccessorySlot
    {
        public override string Name => "AccessorySlot3";

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
