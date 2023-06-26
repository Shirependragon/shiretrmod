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
    public class 开天Projectile : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 32;//弹幕宽度
            Projectile.height = 32;//弹幕高度
            Projectile.aiStyle = -1; // 使用散射型弹幕的 AI 样式
            // AIType = ProjectileID.Bullet;
            Projectile.friendly = true;//是否友善,false时不对敌人造成伤害
            Projectile.hostile = false;//是否敌对,true时对玩家造成伤害
            Projectile.tileCollide = false;//是否穿墙
            Projectile.timeLeft = 300;//存留时间
            Projectile.alpha = 1;//弹幕透明度
            Projectile.light = 1f;//弹幕发光
            Projectile.scale = 2f;//弹幕大小
            Projectile.DamageType = DamageClass.MeleeNoSpeed;//伤害类型
            Projectile.penetrate = -1;//穿透数量
            Projectile.usesLocalNPCImmunity = true;//是否独立无敌帧
            Projectile.ignoreWater = false;//是否水中减速
            Projectile.extraUpdates = 1;//额外刷新
            Projectile.localNPCHitCooldown = 10;//独立无敌帧
        }

        public override void AI()
        {
            float MaxSpeed = 10f; // 横纵向最大速度
            float accSpeed = 0.5f; // 横纵向加速度
            float switchDistance = 50f; // 切换追击方式的距离阈值

            NPC targetNPC = null;
            float minDistance = float.MaxValue;

            // 寻找最近的敌对NPC
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.CanBeChasedBy() && npc.lifeMax > 5 && npc.life > 0 && npc.friendly == false)
                {
                    float distance = Vector2.Distance(Projectile.Center, npc.Center);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        targetNPC = npc;
                    }
                }
            }

            if (targetNPC != null)
            {
                if (minDistance > switchDistance)
                {
                    // 使用插值渐进运动追击最近敌对NPC
                    Vector2 targetPos = targetNPC.Center;
                    Vector2 pos = Vector2.Lerp(Projectile.Center, targetPos, 0.1f);
                    Projectile.velocity = pos - Projectile.Center;
                }
                else
                {
                    // 使用正交惯性追击最近敌对NPC
                    Vector2 targetPos = targetNPC.Center;

                    if (Projectile.Center.X - targetPos.X < 0f)
                        Projectile.velocity.X += Projectile.velocity.X < 0 ? 2 * accSpeed : accSpeed;
                    else
                        Projectile.velocity.X -= Projectile.velocity.X > 0 ? 2 * accSpeed : accSpeed;

                    if (Projectile.Center.Y - targetPos.Y < 0f)
                        Projectile.velocity.Y += Projectile.velocity.Y < 0 ? 2 * accSpeed : accSpeed;
                    else
                        Projectile.velocity.Y -= Projectile.velocity.Y > 0 ? 2 * accSpeed : accSpeed;

                    if (Math.Abs(Projectile.velocity.X) > MaxSpeed)
                        Projectile.velocity.X = MaxSpeed * Math.Sign(Projectile.velocity.X);

                    if (Math.Abs(Projectile.velocity.Y) > MaxSpeed)
                        Projectile.velocity.Y = MaxSpeed * Math.Sign(Projectile.velocity.Y);
                }

            }
            else
            {
                // 没有检测到敌人时，围绕玩家转动
                Player player = Main.player[Projectile.owner];
                Vector2 playerPos = player.Center;
                float radius = 100f;
                float rotationSpeed = 0.05f;
                float angle = Projectile.velocity.ToRotation() + rotationSpeed;
                Vector2 newPos = playerPos + new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * radius;
                Projectile.velocity = newPos - Projectile.Center;
            }

            // 自动绘制时的帧图控制
            int animationSpeed = 3; // 调整动画速度
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= animationSpeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % Main.projFrames[Projectile.type];
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
                int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 292, 0f, 0f, 0, default, 1f);
                Main.dust[dustIndex].position = Projectile.Center;
                Main.dust[dustIndex].velocity = Vector2.Zero;
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].scale = 1.5f; // 设置粒子尺寸
                Main.dust[dustIndex].fadeIn = 1f; // 淡入效果
                Main.dust[dustIndex].noLight = true; // 禁用粒子光照
                Main.dust[dustIndex].noLightEmittence = true; // 禁用粒子发光
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = (Texture2D)TextureAssets.Projectile[Projectile.type];
            Rectangle rectangle = texture.Frame(1, Main.projFrames[Projectile.type], 0, Projectile.frame);

            Vector2 origin = rectangle.Size() / 2f;

            Vector2 toPlayer = Main.player[Projectile.owner].Center - Projectile.Center;
            float rotation = toPlayer.ToRotation() + MathHelper.PiOver2;

            // 修改旋转角度，使贴图始终面向玩家
            rotation += MathHelper.ToRadians(Main.GameUpdateCount % 360);

            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, rectangle, lightColor, rotation, origin, 1f, Projectile.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);

            return false;
        }

    }
}
