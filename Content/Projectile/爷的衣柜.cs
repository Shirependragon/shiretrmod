using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.DataStructures;

namespace shiretrmod.Content.Projectile
{
    public class 爷的衣柜 : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 16;//弹幕宽度
            Projectile.height = 16;//弹幕高度
            Projectile.aiStyle = -1; // 使用散射型弹幕的 AI 样式
            // AIType = ProjectileID.Bullet;
            Projectile.friendly = true;//是否友善,false时不对敌人造成伤害
            Projectile.hostile = false;//是否敌对,true时对玩家造成伤害
            Projectile.tileCollide = false;//是否穿墙
            Projectile.timeLeft = 200;//存留时间
            Projectile.alpha = 1;//弹幕透明度
            Projectile.light = 1f;//弹幕发光
            Projectile.scale = 1f;//弹幕大小
            Projectile.DamageType = DamageClass.Magic;//伤害类型
            Projectile.penetrate = 1;//穿透数量
            Projectile.usesLocalNPCImmunity = false;//是否独立无敌帧
            Projectile.ignoreWater = true;//是否水中减速
            Projectile.extraUpdates = 1;//额外刷新
            Projectile.localNPCHitCooldown = 1;//独立无敌帧
        }

        public override void AI()
        {
            if (Projectile.timeLeft > 50)
            {
                Player player = Main.player[Projectile.owner];
                float rotationSpeed = 0.05f;

                // 控制粒子生成的频率
                int particleFrequency = 5; // 调整生成频率，值越大生成的粒子越少

                if (Projectile.timeLeft % particleFrequency == 0)
                {
                    // 添加粒子效果
                    Vector2 offset = new Vector2(0, -20); // 调整粒子位置偏移
                    Vector2 pos = Projectile.Center + offset;
                    Vector2 velocity = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(30)); // 随机旋转粒子速度
                    Dust.NewDustPerfect(pos, 229, velocity); // 使用火焰效果的粒子，你可以根据需要更改粒子类型
                }

                // 调整速度和位置
                Projectile.velocity = Projectile.velocity.RotatedBy(rotationSpeed);
                Projectile.position = player.position + Projectile.velocity * 0.1f * (300f - Projectile.timeLeft);
            }
            else
            {
                // 使用插值渐进运动追击鼠标
                Vector2 targetPos = Main.MouseWorld;
                Vector2 pos = Vector2.Lerp(Projectile.Center, targetPos, 0.3f);
                Projectile.velocity = pos - Projectile.Center;

            }

            // 贴图翻转和修正
            if (Projectile.velocity.X > 0)
            {
                Projectile.direction = Projectile.spriteDirection = -1;
            }
            else
            {
                Projectile.direction = Projectile.spriteDirection = 1;
                Projectile.rotation += MathHelper.Pi;
            }
            for (int i = 0; i < 5; i++)
            {
                float alpha = 0.5f - (float)i * 0.1f; // 设置透明度递减
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 229, 0f, 0f, 0, default, 1f);
                Main.dust[dustIndex].position = Projectile.Center;
                Main.dust[dustIndex].velocity = Vector2.Zero;
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].scale = 1.5f; // 设置粒子尺寸
                Main.dust[dustIndex].fadeIn = 1f; // 淡入效果
                Main.dust[dustIndex].noLight = false; // 禁用粒子光照
                Main.dust[dustIndex].noLightEmittence = false; // 禁用粒子发光
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = (Texture2D)TextureAssets.Projectile[Projectile.type];
            Rectangle rectangle = texture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);
            Vector2 origin = rectangle.Size() / 2f;

            Vector2 toPlayer = Main.player[Projectile.owner].Center - Projectile.Center;
            float rotation;

            if (Projectile.timeLeft > 0)
            {
                rotation = toPlayer.ToRotation() + MathHelper.PiOver2;
                rotation += MathHelper.ToRadians(Main.GameUpdateCount % 360);
            }
            else
            {
                Vector2 toMouse = Main.MouseWorld - Projectile.Center;
                rotation = toMouse.ToRotation() + MathHelper.PiOver2;

            }

            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, rectangle, lightColor, rotation, origin, 1f, Projectile.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);

            return false;
        }

    }
}
