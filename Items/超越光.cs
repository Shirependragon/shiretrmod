using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace shiretrmod.Items
{
    public class 超越光 : ModItem
    {

        public override void SetDefaults()
        {
            Item.accessory = true; // 设置为饰品
            Item.rare = ItemRarityID.White; // 饰品稀有度
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.statLife < player.statLifeMax2 * 0.5f)
            {
                player.moveSpeed += 2f; // 当玩家血量低于50%时增加移速
                player.jumpBoost = true;
                player.jumpSpeedBoost += 2f;
            }
            else
            {
                player.moveSpeed += 0.5f;
                player.jumpBoost = true;
                player.jumpSpeedBoost += 1f;
            }
        }
    }
}