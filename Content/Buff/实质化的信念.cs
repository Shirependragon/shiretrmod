using Terraria;
using Terraria.ModLoader;

namespace shiretrmod.Content.Buff;
public class 实质化的信念 : ModBuff
{
    public override void Update(Player player, ref int buffIndex)
    {
        
    }
    public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
    {
        tip += "\n神念之刃的Buff, 暂时为空";
        base.ModifyBuffText(ref buffName, ref tip, ref rare);
    }
}