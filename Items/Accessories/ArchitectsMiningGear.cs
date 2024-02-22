using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Items.Accessories;

public class ArchitectsMiningGear : ModItem
{
	public override void Load()
	{
		if (Main.netMode != 2)
		{
			EquipLoader.AddEquipTexture(Mod, $"{Texture}_{0}", (EquipType)0, (ModItem)(object)this, (string)null, (EquipTexture)null);
			EquipLoader.AddEquipTexture(Mod, $"{Texture}_{1}", (EquipType)1, (ModItem)(object)this, (string)null, (EquipTexture)null);
			EquipLoader.AddEquipTexture(Mod, $"{Texture}_{2}", (EquipType)2, (ModItem)(object)this, (string)null, (EquipTexture)null);
			EquipLoader.AddEquipTexture(Mod, $"{Texture}_{5}", (EquipType)5, (ModItem)(object)this, (string)null, (EquipTexture)null);
		}
	}

	private void SetUpDrawing()
	{
		if (Main.netMode != 2)
		{
			EquipLoader.GetEquipSlot(Mod, Name, (EquipType)0);
			EquipLoader.GetEquipSlot(Mod, Name, (EquipType)1);
			EquipLoader.GetEquipSlot(Mod, Name, (EquipType)2);
			EquipLoader.GetEquipSlot(Mod, Name, (EquipType)5);
		}
	}

	public override void SetStaticDefaults()
	{
		SetUpDrawing();
	}

	public override void SetDefaults()
	{
		Item.width = 40;
		Item.height = 40;
		Item.value = Item.sellPrice(0, 5, 0, 0);
		Item.rare = 6;
		Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.tileSpeed *= 4f;
		player.pickSpeed -= 10f;
		player.wallSpeed += 10f;
		player.autoPaint = true;
		if (player.whoAmI == Main.myPlayer)
		{
			Player.tileRangeX += 3;
			Player.tileRangeY += 2;
		}
		WolfsPlayer modPlayer = player.GetModPlayer<WolfsPlayer>();
		modPlayer.amgAccessory = true;
		modPlayer.amgHideVanity = hideVisual;
	}

	public override bool IsVanitySet(int head, int body, int legs)
	{
		return true;
	}

	public override void AddRecipes()
	{
		Recipe obj = CreateRecipe(1);
		obj.AddIngredient(3061, 1);
		obj.AddIngredient(88, 1);
		obj.AddIngredient(410, 1);
		obj.AddIngredient(411, 1);
		obj.AddTile(114);
		obj.Register();
	}
}
