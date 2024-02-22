using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Accessories;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class FuriaWings : ModItem
{
	public override void SetStaticDefaults()
	{
	}

	public override void SetDefaults()
	{
		Item.width = 22;
		Item.height = 20;
		Item.value = 10000;
		Item.rare = 2;
		Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.wingTimeMax = 180;
		player.dashType = 2;
		player.GetModPlayer<WolfsPlayer>().furiaWings = true;
	}

    public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
    {
        ascentWhenFalling = 0.85f;
        ascentWhenRising = 0.15f;
        maxCanAscendMultiplier = 1f;
        maxAscentMultiplier = 3f;
        constantAscend = 0.135f;
    }

    public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
    {
        speed = 9f;
        acceleration = 1.1f;
    }

    public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(3458, 14);
		obj.AddIngredient(320, 10);
		obj.AddIngredient(520, 25);
		obj.AddTile(412);
		obj.Register();
	}
}
