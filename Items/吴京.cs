using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using shiretrmod.Content.Projectile;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.DataStructures;
using System;


namespace shiretrmod.Items
{
    public class 吴京 : ModItem
    {
        private object crit;
        private int knockBack;

        public override void SetDefaults()
        {
            Item.damage = 60;
            Item.crit = 10;
            Item.scale = 2f;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.useStyle = 1;
            Item.knockBack = 10f;
            Item.value = 15000;
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.shootSpeed = 10f; // 弹幕的速度
            Item.shoot = ModContent.ProjectileType<china>(); // 弹幕的类型
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            //本函数是武器近战挥舞时触发的操作，通常为生成粒子
            Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, DustID.RainbowMk2, 0, 0, 0, default, 2);
            Dust.NewDust(Entity.position, 16, 16, DustID.RainbowMk2, 0, 0, 0, default, 2f);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 120);//addbuff方法第一个参数为要上的BUFFID，第二个为持续时间（帧）
            player.AddBuff(BuffID.NebulaUpLife3, 30);//为玩家添加半秒星云回复BUFF
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            //本函数用于在武器执行发射弹幕时的操作，返回false可阻止武器原本的发射。true则保留。
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, velocity.RotatedBy(+2), type, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, velocity.RotatedBy(-2), type, damage, knockback, player.whoAmI);
            //这里我额外生成两个散射剑气,注意rotatedby是将向量偏转指定弧度，（6.28也就是2PI为一圈）
            //生成一个弹幕，source是生成源，直接使用参数即可。第二个参数是生成位置，position在玩家处。
            //第三个参数是速度，决定弹幕的初始速度（二维向量），第四个参数是ID，第五个参数是伤害
            //第七个参数是弹幕所有者的索引，通常有player参数时直接填player.whoami，不填这个参数可能会引发错误。
            return true;
        }

        public override void UpdateInventory(Player player)
        {
            Lighting.AddLight(player.Center, new Vector3(0.5f, 0.5f, 0.5f)); // 设置光照颜色
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.Red * 0.8f; // 设置物品颜色和透明度
        }
        //public override void UseStyle(Player player, Rectangle heldItemFrame)
        //{
        // 播放挥动动画
        //    player.itemRotation = MathHelper.ToRadians(-45f * player.direction); // 将武器的旋转角度设置为朝向鼠标的方向

        // 其他挥动动画的代码...
        //}
    }

}