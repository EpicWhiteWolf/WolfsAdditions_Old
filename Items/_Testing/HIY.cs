/*
using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Items._Testing;

internal class HIY : ModItem
{
	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		Item.DamageType = DamageClass.Ranged;
		Item.width = 8;
		Item.height = 8;
		Item.useTime = 1;
		Item.useAnimation = 1;
		Item.useStyle = 4;
		Item.noMelee = true;
		Item.rare = -1;
		Item.autoReuse = false;
	}

	public override bool? UseItem(Player player)
	{
		WolfsPlayer p = player.GetModPlayer<WolfsPlayer>();
		if (Player.direction == -1)
		{
			p.testY++;
		}
		if (Player.direction == 1)
		{
			p.testY--;
		}
		Main.NewText($"{p.testY}", byte.MaxValue, byte.MaxValue, byte.MaxValue);
		return null;
	}
}
*/