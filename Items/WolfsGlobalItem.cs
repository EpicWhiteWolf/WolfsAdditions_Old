using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using WolfsAdditions.Items.Accessories;
using WolfsAdditions.Items.Weapons.Melee;

namespace WolfsAdditions.Items;

internal class WolfsGlobalItem : GlobalItem
{
	public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
	{
		if (item.type == 3319)
		{
			((ItemLoot)(itemLoot)).Add(ItemDropRule.Common(ModContent.ItemType<BladeOfCthulhu>(), 1, 1, 1));
		}
		if (item.type == 3326)
		{
			((ItemLoot)(itemLoot)).Add(ItemDropRule.Common(ModContent.ItemType<HordeCharm>(), 1, 1, 1));
		}
	}

	public override bool CanUseItem(Item item, Player player)
	{
		if (player.GetModPlayer<WolfsPlayer>().wolfsArmorSet && (item.type == 110 || item.type == 189 || item.type == 500 || item.type == 2209 || item.type == 293))
		{
			return false;
		}
		return true;
	}

	public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
	{
		if (player.GetModPlayer<WolfsPlayer>().wolfsArmorSet && (item.type == 2221 || item.type == 1595 || item.type == 4001))
		{
			return false;
		}
		return true;
	}

	public override bool CanPickup(Item item, Player player)
	{
		if (player.GetModPlayer<WolfsPlayer>().wolfsArmorSet && (item.type == 184 || item.type == 1735 || item.type == 1868 || item.type == 3455 || item.type == 4143))
		{
			return false;
		}
		return true;
	}
}
