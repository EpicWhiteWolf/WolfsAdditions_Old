using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace WolfsAdditions.Dusts;

public class WolfSetDust : ModDust
{
	public override void OnSpawn(Dust dust)
	{
		dust.noGravity = true;
	}

	public override bool Update(Dust dust)
	{
		dust.velocity *= 1f;
		dust.scale -= 0.005f;
		if (dust.scale <= 0.5f)
		{
			dust.active = false;
		}
		Lighting.AddLight(dust.position, 0.5f * dust.scale, 0.5f * dust.scale, 0.5f * dust.scale);
		return false;
	}

	public override Color? GetAlpha(Dust dust, Color lightColor)
	{
		return new Color(1f, 1f, 1f, 1f);
	}
}
