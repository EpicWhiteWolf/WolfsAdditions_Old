using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Armor;

[AutoloadEquip]
public class FuriaHair : ModItem
{
	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.value = Item.sellPrice(0, 5, 0, 0);
		Item.rare = 8;
	}

	public override void UpdateEquip(Player player)
	{
		StatModifier damage = player.GetDamage(DamageClass.Magic);
		damage *= 1.1f;
		player.GetCritChance(DamageClass.Magic) += 10f;
		player.manaCost *= 0.85f;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == Mod.Find<ModItem>("FuriaCoat").Type)
		{
			return legs.type == Mod.Find<ModItem>("FuriaLeggings").Type;
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "Increases maximum mana by 200\nDouble tap " + Main.cDown + " to send out Pyre Strike";
		player.statManaMax2 += 200;
		player.GetModPlayer<WolfsPlayer>().furiaSet = true;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(3458, 10);
		obj.AddTile(412);
		obj.Register();
	}
}
