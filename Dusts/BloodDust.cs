using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Dusts;

public class BloodDust : ModDust
{
	public override void OnSpawn(Dust dust)
	{
		dust.noGravity = false;
		dust.velocity.Y *= 0.2f;
		dust.velocity.X *= 0f;
	}

	public override bool Update(Dust dust)
	{
		dust.position += dust.velocity;
		dust.rotation += dust.velocity.X;
		dust.scale -= 0.02f;
		if (dust.scale < 0.5f)
		{
			dust.active = false;
		}
		return false;
	}
}
