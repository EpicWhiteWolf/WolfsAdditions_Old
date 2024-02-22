using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Crafting;

internal class BalancedFragment : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemIconPulse[Item.type] = true;
		ItemID.Sets.ItemNoGravity[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 20;
		Item.rare = 1;
		Item.maxStack = 999;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(1f, 1f, 1f, 1f);
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		Lighting.AddLight(Item.Center, 0.25f, 0.25f, 0.25f);
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(4);
		obj.AddIngredient(3458, 1);
		obj.AddIngredient(3457, 1);
		obj.AddIngredient(3459, 1);
		obj.AddIngredient(3456, 1);
		obj.AddTile(412);
		obj.Register();
	}
}
