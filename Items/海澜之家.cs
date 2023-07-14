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
    public class 海澜之家 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 60; // 设置法杖的伤害值
            Item.DamageType = MagicDamageClass.Magic; // 标记法杖为魔法武器
            Item.mana = 5; // 每次使用消耗的法力值
            Item.width = 40; // 设置法杖的宽度
            Item.height = 40; // 设置法杖的高度
            Item.useTime = 20; // 设置法杖使用的时间（以帧为单位）
            Item.useAnimation = 20; // 设置法杖使用时的动画时间（以帧为单位）
            Item.useStyle = ItemUseStyleID.Swing; // 设置法杖的使用方式
            Item.noMelee = true; // 禁止法杖进行近战攻击
            Item.knockBack = 2f; // 设置法杖的击退力量
            Item.value = Item.sellPrice(silver: 10); // 设置法杖的售价
            Item.rare = ItemRarityID.Pink; // 设置法杖的稀有度
            Item.UseSound = SoundID.Item20; // 设置法杖的使用声音
            Item.autoReuse = true; // 设置法杖是否自动重复使用
            Item.shootSpeed = 10f; // 设置法杖发射的弹幕速度
            Item.mana = 10; // 每次使用消耗的法力值
            Item.useTurn = true; // 设置法杖使用时是否可以转向
            Item.UseSound = SoundID.Item8; // 设置法杖的使用声音
            Item.staff[Item.type] = true;//让本武器为法杖类型（因为贴图是45度，如果不这么做发射时就会倾斜45度）
            Item.shoot = ModContent.ProjectileType<爷的衣柜>(); // 设置法杖发射的弹幕类型
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
            Lighting.AddLight(player.Center, new Vector3(1f, 1f, 1f)); // 设置光照颜色
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.Cyan * 0.8f; // 设置物品颜色和透明度
        }
    }
}