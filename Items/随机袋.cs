using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace shiretrmod.Items
{
    public class 随机袋 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 99;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = 2;
            Item.value = Item.sellPrice(copper: 1); // 设置售价为1铜币
            Item.useTurn = true;
            Item.autoReuse = false;
        }

        // public override bool CanUseItem(Player player)
        // {
        //     return true;
        // }

        // public override bool? UseItem(Player player)
        // {
        //     SpawnRewards(player);
        //     Item.stack--;
        //     return true;
        // }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            SpawnRewards(player);
        }
        void SpawnRewards(Player player)
        {
            SpawnCoins(player);
            SpawnItems(player);
        }

        void SpawnCoins(Player player)
        {
            int coinAmount = Main.rand.Next(1, 6); // 生成1到5个金币
            player.QuickSpawnItem(new EntitySource_DebugCommand($"{nameof(shiretrmod)}_{nameof(shiretrmod)}"), ItemID.CopperCoin, coinAmount);
        }

        void SpawnItems(Player player)
        {
            int[] itemIds = { ModContent.ItemType<包包给你买新的>(), ModContent.ItemType<超越光>(), ModContent.ItemType<海澜之家>(), ModContent.ItemType<黑枪>(), ModContent.ItemType<开天>(), ModContent.ItemType<老马徽章>(), ModContent.ItemType<神念之刃>(), ModContent.ItemType<吴京>(), ModContent.ItemType<滋滋滋>(), }; // 需要生成的物品ID数组

            int itemAmount = Main.rand.Next(1, 2); // 生成0到3个物品

            for (int i = 0; i < itemAmount; i++)
            {
                int randomIndex = Main.rand.Next(itemIds.Length);
                int itemId = itemIds[randomIndex];
                player.QuickSpawnItem(new EntitySource_DebugCommand($"{nameof(shiretrmod)}_{nameof(shiretrmod)}"), itemId, 1);
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GoldBar, 1); // 1个金锭
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
