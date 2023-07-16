using Terraria;
using Terraria.ModLoader;

namespace shiretrmod.Content.Buff;
public class DashCooldown : ModBuff
{

    public override void Update(Player player, ref int buffIndex)
    {

    }
    public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
    {
        tip = "神念之刃冲刺冷却中";
        base.ModifyBuffText(ref buffName, ref tip, ref rare);
    }
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
        base.SetStaticDefaults();
    }
}