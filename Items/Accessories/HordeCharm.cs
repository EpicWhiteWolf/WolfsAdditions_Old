using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Accessories;

internal class HordeCharm : ModItem
{
	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		Item.width = 30;
		Item.height = 30;
		Item.value = Item.sellPrice(0, 0, 50, 0);
		Item.rare = 4;
		Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<WolfsPlayer>().sylSig = true;
		StatModifier damage = player.GetDamage(DamageClass.Summon);
		damage *= 0.5f;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(57, 10);
		obj.AddTile(16);
		obj.Register();
	}
}
