using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Buffs;

public class ArchitectsMiningGear : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[Type] = true;
		Main.buffNoSave[Type] = true;
		Main.buffNoTimeDisplay[Type] = true;
		BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
	}

	public override void Update(Player player,ref int buffIndex)
	{
		WolfsPlayer p = player.GetModPlayer<WolfsPlayer>();
		if (p.amgAccessoryPrevious)
		{
			p.amgVisual = true;
			return;
		}
		player.DelBuff(buffIndex);
		buffIndex--;
	}
}
