using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace shiretrmod.Items
{
    public class 神念之刃 : ModItem
    {

        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.shoot = ProjectileID.None;
            Item.shootSpeed = 0f;
        }

        public override bool CanUseItem(Player player)
        {
            // 检测右键点击事件，并触发冲刺效果
            if (player.itemAnimation == 0 && player.altFunctionUse == 2 && player.HeldItem.type == Item.type)
            {
                float dashDistance = 500f; // 冲刺距离
                Vector2 dashVelocity = Vector2.Normalize(Main.MouseWorld - player.Center) * dashDistance;
                player.velocity = dashVelocity;
            }
            return base.CanUseItem(player);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            // 反弹弹幕逻辑
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (projectile.active && projectile.hostile && !projectile.friendly && projectile.damage > 0 && player.Distance(projectile.Center) <= 40f)
                {
                    projectile.velocity *= -1; // 反弹弹幕
                    projectile.owner = player.whoAmI; // 将弹幕归属于玩家
                }
            }
        }
    }
}