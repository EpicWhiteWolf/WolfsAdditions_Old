using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace WolfsAdditions.Projectiles;

internal class WallCharge : ModProjectile
{
	private int stuck;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((Entity)Projectile).width = 3;
		((Entity)Projectile).height = 4;
		Projectile.friendly = true;
		Projectile.penetrate = -1;
		Projectile.timeLeft = 300;
		DrawOffsetX = -8;
		DrawOriginOffsetY = -10;
		Projectile.aiStyle = 0;
	}

	public override void AI()
	{
		if (Projectile.owner == Main.myPlayer)
		{
			Projectile.hostile = true;
		}
		int posX = (int)((Entity)Projectile).position.X / 16;
		int posY = (int)((Entity)Projectile).position.Y / 16;
		Tile val = ((Tilemap)(Main.tile))[posX, posY];
		if (((Tile)(val)).WallType > 0)
		{
			Projectile projectile = Projectile;
			((Entity)projectile).velocity = ((Entity)projectile).velocity * 0f;
			return;
		}
		if (((Tilemap)(Main.tile))[posX + 1, posY] != (ArgumentException)null)
		{
			val = ((Tilemap)(Main.tile))[posX + 1, posY];
			if (((Tile)(val)).HasUnactuatedTile)
			{
				bool[] tileSolid = Main.tileSolid;
				val = ((Tilemap)(Main.tile))[posX + 1, posY];
				if (tileSolid[((Tile)(val)).TileType])
				{
					goto IL_02a8;
				}
				bool[] tileSolidTop = Main.tileSolidTop;
				val = ((Tilemap)(Main.tile))[posX + 1, posY];
				if (tileSolidTop[((Tile)(val)).TileType])
				{
					val = ((Tilemap)(Main.tile))[posX + 1, posY];
					if (((Tile)(val)).TileFrameY == 0)
					{
						goto IL_02a8;
					}
				}
			}
		}
		if (((Tilemap)(Main.tile))[posX - 1, posY] != (ArgumentException)null)
		{
			val = ((Tilemap)(Main.tile))[posX - 1, posY];
			if (((Tile)(val)).HasUnactuatedTile)
			{
				bool[] tileSolid2 = Main.tileSolid;
				val = ((Tilemap)(Main.tile))[posX - 1, posY];
				if (tileSolid2[((Tile)(val)).TileType])
				{
					goto IL_02a8;
				}
				bool[] tileSolidTop2 = Main.tileSolidTop;
				val = ((Tilemap)(Main.tile))[posX - 1, posY];
				if (tileSolidTop2[((Tile)(val)).TileType])
				{
					val = ((Tilemap)(Main.tile))[posX - 1, posY];
					if (((Tile)(val)).TileFrameY == 0)
					{
						goto IL_02a8;
					}
				}
			}
		}
		if (((Tilemap)(Main.tile))[posX, posY + 1] != (ArgumentException)null)
		{
			val = ((Tilemap)(Main.tile))[posX, posY + 1];
			if (((Tile)(val)).HasUnactuatedTile)
			{
				bool[] tileSolid3 = Main.tileSolid;
				val = ((Tilemap)(Main.tile))[posX, posY + 1];
				if (tileSolid3[((Tile)(val)).TileType])
				{
					goto IL_02a8;
				}
				bool[] tileSolidTop3 = Main.tileSolidTop;
				val = ((Tilemap)(Main.tile))[posX, posY + 1];
				if (tileSolidTop3[((Tile)(val)).TileType])
				{
					val = ((Tilemap)(Main.tile))[posX, posY + 1];
					if (((Tile)(val)).TileFrameY == 0)
					{
						goto IL_02a8;
					}
				}
			}
		}
		if (((Tilemap)(Main.tile))[posX, posY - 1] != (ArgumentException)null)
		{
			val = ((Tilemap)(Main.tile))[posX, posY - 1];
			if (((Tile)(val)).HasUnactuatedTile)
			{
				bool[] tileSolid4 = Main.tileSolid;
				val = ((Tilemap)(Main.tile))[posX, posY - 1];
				if (tileSolid4[((Tile)(val)).TileType])
				{
					goto IL_02a8;
				}
				bool[] tileSolidTop4 = Main.tileSolidTop;
				val = ((Tilemap)(Main.tile))[posX, posY - 1];
				if (tileSolidTop4[((Tile)(val)).TileType])
				{
					val = ((Tilemap)(Main.tile))[posX, posY - 1];
					if (((Tile)(val)).TileFrameY == 0)
					{
						goto IL_02a8;
					}
				}
			}
		}
		stuck = 0;
		((Entity)Projectile).velocity.Y += 0.2f;
		((Entity)Projectile).velocity.X *= 0.99f;
		if (((Entity)Projectile).velocity.Y > 12f)
		{
			((Entity)Projectile).velocity.Y = 12f;
		}
		return;
		IL_02a8:
		if (stuck > 0)
		{
			Projectile projectile2 = Projectile;
			((Entity)projectile2).velocity = ((Entity)projectile2).velocity * 0f;
			stuck = 1;
		}
		stuck++;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		SoundEngine.PlaySound(SoundID.Item14, (Vector2?)((Entity)Projectile).position, (SoundUpdateCallback)null);
		for (int i = 0; i < 50; i++)
		{
			int dustIndex = Dust.NewDust(new Vector2(((Entity)Projectile).position.X, ((Entity)Projectile).position.Y), ((Entity)Projectile).width, ((Entity)Projectile).height, 31, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[dustIndex];
			obj.velocity *= 1.4f;
		}
		for (int j = 0; j < 80; j++)
		{
			int dustIndex2 = Dust.NewDust(new Vector2(((Entity)Projectile).position.X, ((Entity)Projectile).position.Y), ((Entity)Projectile).width, ((Entity)Projectile).height, 6, 0f, 0f, 100, default(Color), 3f);
			Main.dust[dustIndex2].noGravity = true;
			Dust obj2 = Main.dust[dustIndex2];
			obj2.velocity *= 5f;
			dustIndex2 = Dust.NewDust(new Vector2(((Entity)Projectile).position.X, ((Entity)Projectile).position.Y), ((Entity)Projectile).width, ((Entity)Projectile).height, 6, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[dustIndex2];
			obj3.velocity *= 3f;
		}
		for (int g = 0; g < 2; g++)
		{
			int goreIndex = Gore.NewGore(Projectile.InheritSource(Projectile), new Vector2(((Entity)Projectile).position.X + (float)(((Entity)Projectile).width / 2) - 24f, ((Entity)Projectile).position.Y + (float)(((Entity)Projectile).height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
			Main.gore[goreIndex].scale = 1.5f;
			Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
			Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
			goreIndex = Gore.NewGore(Projectile.InheritSource(Projectile), new Vector2(((Entity)Projectile).position.X + (float)(((Entity)Projectile).width / 2) - 24f, ((Entity)Projectile).position.Y + (float)(((Entity)Projectile).height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
			Main.gore[goreIndex].scale = 1.5f;
			Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
			Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
			goreIndex = Gore.NewGore(Projectile.InheritSource(Projectile), new Vector2(((Entity)Projectile).position.X + (float)(((Entity)Projectile).width / 2) - 24f, ((Entity)Projectile).position.Y + (float)(((Entity)Projectile).height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
			Main.gore[goreIndex].scale = 1.5f;
			Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
			Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
			goreIndex = Gore.NewGore(Projectile.InheritSource(Projectile), new Vector2(((Entity)Projectile).position.X + (float)(((Entity)Projectile).width / 2) - 24f, ((Entity)Projectile).position.Y + (float)(((Entity)Projectile).height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
			Main.gore[goreIndex].scale = 1.5f;
			Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
			Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
		}
		int explosionRadius = 3;
		explosionRadius = 7;
		int minTileX = (int)(((Entity)Projectile).position.X / 16f - (float)explosionRadius);
		int maxTileX = (int)(((Entity)Projectile).position.X / 16f + (float)explosionRadius);
		int minTileY = (int)(((Entity)Projectile).position.Y / 16f - (float)explosionRadius);
		int maxTileY = (int)(((Entity)Projectile).position.Y / 16f + (float)explosionRadius);
		if (minTileX < 0)
		{
			minTileX = 0;
		}
		if (maxTileX > Main.maxTilesX)
		{
			maxTileX = Main.maxTilesX;
		}
		if (minTileY < 0)
		{
			minTileY = 0;
		}
		if (maxTileY > Main.maxTilesY)
		{
			maxTileY = Main.maxTilesY;
		}
		bool canKillWalls = false;
		Tile val;
		for (int x2 = minTileX; x2 <= maxTileX; x2++)
		{
			for (int y = minTileY; y <= maxTileY; y++)
			{
				float num = Math.Abs((float)x2 - ((Entity)Projectile).position.X / 16f);
				float diffY = Math.Abs((float)y - ((Entity)Projectile).position.Y / 16f);
				if (Math.Sqrt(num * num + diffY * diffY) < (double)explosionRadius && ((Tilemap)(Main.tile))[x2, y] != (ArgumentException)null)
				{
					val = ((Tilemap)(Main.tile))[x2, y];
					if (((Tile)(val)).WallType == 0)
					{
						canKillWalls = true;
						break;
					}
				}
			}
		}
		AchievementsHelper.CurrentlyMining = true;
		for (int k = minTileX; k <= maxTileX; k++)
		{
			for (int l = minTileY; l <= maxTileY; l++)
			{
				float num2 = Math.Abs((float)k - ((Entity)Projectile).position.X / 16f);
				float diffY2 = Math.Abs((float)l - ((Entity)Projectile).position.Y / 16f);
				if (!(Math.Sqrt(num2 * num2 + diffY2 * diffY2) < (double)explosionRadius))
				{
					continue;
				}
				bool canKillTile = true;
				if (((Tilemap)(Main.tile))[k, l] != (ArgumentException)null)
				{
					val = ((Tilemap)(Main.tile))[k, l];
					if (((Tile)(val)).HasTile)
					{
						canKillTile = true;
						bool[] tileDungeon = Main.tileDungeon;
						val = ((Tilemap)(Main.tile))[k, l];
						if (!tileDungeon[((Tile)(val)).TileType])
						{
							val = ((Tilemap)(Main.tile))[k, l];
							if (((Tile)(val)).TileType != 88)
							{
								val = ((Tilemap)(Main.tile))[k, l];
								if (((Tile)(val)).TileType != 21)
								{
									val = ((Tilemap)(Main.tile))[k, l];
									if (((Tile)(val)).TileType != 26)
									{
										val = ((Tilemap)(Main.tile))[k, l];
										if (((Tile)(val)).TileType != 107)
										{
											val = ((Tilemap)(Main.tile))[k, l];
											if (((Tile)(val)).TileType != 108)
											{
												val = ((Tilemap)(Main.tile))[k, l];
												if (((Tile)(val)).TileType != 111)
												{
													val = ((Tilemap)(Main.tile))[k, l];
													if (((Tile)(val)).TileType != 226)
													{
														val = ((Tilemap)(Main.tile))[k, l];
														if (((Tile)(val)).TileType != 237)
														{
															val = ((Tilemap)(Main.tile))[k, l];
															if (((Tile)(val)).TileType != 221)
															{
																val = ((Tilemap)(Main.tile))[k, l];
																if (((Tile)(val)).TileType != 222)
																{
																	val = ((Tilemap)(Main.tile))[k, l];
																	if (((Tile)(val)).TileType != 223)
																	{
																		val = ((Tilemap)(Main.tile))[k, l];
																		if (((Tile)(val)).TileType != 211)
																		{
																			val = ((Tilemap)(Main.tile))[k, l];
																			if (((Tile)(val)).TileType != 404)
																			{
																				goto IL_0927;
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
						canKillTile = false;
						goto IL_0927;
					}
				}
				goto IL_09a4;
				IL_0927:
				if (!Main.hardMode)
				{
					val = ((Tilemap)(Main.tile))[k, l];
					if (((Tile)(val)).TileType == 58)
					{
						canKillTile = false;
					}
				}
				if (!TileLoader.CanExplode(k, l))
				{
					canKillTile = false;
				}
				if (canKillTile)
				{
					WorldGen.KillTile(k, l, false, false, false);
					val = ((Tilemap)(Main.tile))[k, l];
					if (!((Tile)(val)).HasTile && Main.netMode != 0)
					{
						NetMessage.SendData(17, -1, -1, (NetworkText)null, 0, (float)k, (float)l, 0f, 0, 0, 0);
					}
				}
				goto IL_09a4;
				IL_09a4:
				if (!canKillTile)
				{
					continue;
				}
				for (int x = k - 1; x <= k + 1; x++)
				{
					for (int y2 = l - 1; y2 <= l + 1; y2++)
					{
						int num3;
						if (((Tilemap)(Main.tile))[x, y2] != (ArgumentException)null)
						{
							val = ((Tilemap)(Main.tile))[x, y2];
							num3 = ((((Tile)(val)).WallType > 0) ? 1 : 0);
						}
						else
						{
							num3 = 0;
						}
						if (((uint)num3 & (canKillWalls ? 1u : 0u)) == 0)
						{
							continue;
						}
						int num4 = x;
						int num5 = y2;
						val = ((Tilemap)(Main.tile))[x, y2];
						if (WallLoader.CanExplode(num4, num5, (int)((Tile)(val)).WallType))
						{
							WorldGen.KillWall(x, y2, false);
							val = ((Tilemap)(Main.tile))[x, y2];
							if (((Tile)(val)).WallType == 0 && Main.netMode != 0)
							{
								NetMessage.SendData(17, -1, -1, (NetworkText)null, 2, (float)x, (float)y2, 0f, 0, 0, 0);
							}
						}
					}
				}
			}
		}
		AchievementsHelper.CurrentlyMining = false;
	}
}
