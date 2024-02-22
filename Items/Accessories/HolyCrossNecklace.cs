using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Accessories;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
internal class HolyCrossNecklace : ModItem
{
	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		Item.width = 30;
		Item.height = 30;
		Item.value = 10000;
		Item.rare = 6;
		Item.accessory = true;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(554, 1);
		obj.AddIngredient((Mod)null, "HolyGrail", 1);
		obj.AddTile(114);
		obj.Register();
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		Lighting.AddLight(Item.Center, 1f, 1f, 0.75f);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.longInvince = true;
		player.GetModPlayer<WolfsPlayer>().accHolyGrail = true;
	}
}
