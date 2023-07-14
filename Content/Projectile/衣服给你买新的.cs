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
    public class 衣服给你买新的 : ModProjectile
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
        private bool hasTarget;
        private int targetNPCIndex;
        private Vector2 targetPosition;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (!hasTarget)
            {
                // 当没有敌人目标时，追踪鼠标位置
                targetPosition = Main.MouseWorld;
            }
            else
            {
                // 当有敌人目标时，追踪敌人位置
                if (targetNPCIndex != -1)
                {
                    NPC npc = Main.npc[targetNPCIndex];
                    targetPosition = npc.Center;
                }
                else
                {
                    // 如果敌人已经消失，则停止追踪敌人
                    hasTarget = false;
                    targetPosition = Main.MouseWorld;
                }
            }

            // 设置控制点
            Vector2 controlPoint = new Vector2(Projectile.Center.X, Projectile.Center.Y - 200f); // 调整控制点的高度和位置

            // 计算贝塞尔曲线上的位置
            float t = (float)Projectile.ai[0] / 100f; // 根据时间来控制弹幕在曲线上的位置
            Vector2 position = CalculateBezierPoint(Projectile.Center, controlPoint, targetPosition, t);

            // 更新弹幕位置
            Projectile.position = position;

            // 更新时间
            Projectile.ai[0]++;

            // 如果超过一定时间，让弹幕消失
            if (Projectile.ai[0] > 100)
            {
                Projectile.Kill();
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
        private Vector2 CalculateBezierPoint(Vector2 p0, Vector2 p1, Vector2 p2, float t)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;

            Vector2 p = uu * p0; // P0 * (1 - t)^2
            p += 2 * u * t * p1; // + 2 * (1 - t) * t * P1
            p += tt * p2; // + t^2 * P2
            return p;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // 当击中敌人时，设置敌人为目标，并标记 hasTarget 为 true
            hasTarget = true;
            targetNPCIndex = target.whoAmI;
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
