using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using shiretrmod.Content.Buff;

namespace shiretrmod.Items
{
    public class 神念之刃 : ModItem
    {

        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Generic;
            Item.width = 40;
            Item.height = 40;
            Item.scale = 3;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = false;
            Item.shoot = ProjectileID.None;
            Item.shootSpeed = 0f;
            Item.accessory = true;
        }

        public override bool CanUseItem(Player player)
        {

            float dashDistance = 20f; // 冲刺距离
            Vector2 dashVelocity = Vector2.Normalize(Main.MouseWorld - player.Center) * dashDistance;
            player.velocity = dashVelocity;

            // 使玩家朝向鼠标
            // 获取鼠标在游戏世界中的位置
            Vector2 mousePosition = Main.MouseWorld;

            // 判断玩家应该面向哪个方向
            if (player.position.X + (player.width / 2) < mousePosition.X)
            {
                player.direction = 1; // 面向右边
            }
            else
            {
                player.direction = -1; // 面向左边
            }
            return base.CanUseItem(player);
        }
        public override bool? UseItem(Player player)
        {
            foreach (Projectile proj in Main.projectile) // 遍历所有的projectile
            {

                if (proj.hostile && !proj.friendly && player.Distance(proj.Center) <= 150f) // 如果是敌对弹幕，并且距离玩家足够近
                {
                    // 反弹并设置为友好弹幕
                    proj.velocity *= -1;
                    proj.owner = player.whoAmI;
                    proj.hostile = false;
                    proj.friendly = true;
                }
            }
            return base.UseItem(player);
        }
        public override void HoldItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<实质化的信念>(), 60);
            base.HoldItem(player);
        }

        public override void UpdateAccessory(Player player, bool hideVisual) // 饰品属性设置
        {
            // +8防御，*1.25速度
            player.statDefense += 8;
            player.moveSpeed *= 1.25f;
            base.UpdateAccessory(player, hideVisual);
        }
    }
}