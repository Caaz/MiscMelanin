
using RimWorld;
using System;
using UnityEngine;
using Verse;




namespace MiscMelanin
{

	// Token: 0x02000DF5 RID: 3573
	public static class PawnSkinColors20
	{
		// Token: 0x0600533D RID: 21309 RVA: 0x001C39F4 File Offset: 0x001C1BF4
		public static bool IsDarkSkin(Color color)
		{
			Color skinColor = PawnSkinColors20.GetSkinColor(0.5f);
			return color.r + color.g + color.b <= skinColor.r + skinColor.g + skinColor.b + 0.01f;
		}

        internal static void GetSkinColor(Func<float, Color> getSkinColor)
        {
            throw new NotImplementedException();
        }

        internal static void GetSkinColor(Func<object, bool> equals)
        {
            throw new NotImplementedException();
        }

        // Token: 0x0600533E RID: 21310 RVA: 0x001C3A40 File Offset: 0x001C1C40
        public static Color GetSkinColor(float melanin)
		{
			int skinDataIndexOfMelanin = PawnSkinColors20.GetSkinDataIndexOfMelanin(melanin);
			if (skinDataIndexOfMelanin == PawnSkinColors20.SkinColors.Length - 1)
			{
				return PawnSkinColors20.SkinColors[skinDataIndexOfMelanin].color;
			}
			float t = Mathf.InverseLerp(PawnSkinColors20.SkinColors[skinDataIndexOfMelanin].melanin, PawnSkinColors20.SkinColors[skinDataIndexOfMelanin + 1].melanin, melanin);
			return Color.Lerp(PawnSkinColors20.SkinColors[skinDataIndexOfMelanin].color, PawnSkinColors20.SkinColors[skinDataIndexOfMelanin + 1].color, t);
		}

		// Token: 0x0600533F RID: 21311 RVA: 0x001C3AC4 File Offset: 0x001C1CC4
		public static float RandomMelanin(Faction fac)
		{
			float num;
			if (fac == null)
			{
				num = Rand.Value;
			}
			else
			{
				num = Rand.Range(Mathf.Clamp01(fac.centralMelanin - fac.def.geneticVariance), Mathf.Clamp01(fac.centralMelanin + fac.def.geneticVariance));
			}
			int num2 = 0;
			int num3 = 0;
			while (num3 < PawnSkinColors20.SkinColors.Length && num >= PawnSkinColors20.SkinColors[num3].selector)
			{
				num2 = num3;
				num3++;
			}
			if (num2 == PawnSkinColors20.SkinColors.Length - 1)
			{
				return PawnSkinColors20.SkinColors[num2].melanin;
			}
			float t = Mathf.InverseLerp(PawnSkinColors20.SkinColors[num2].selector, PawnSkinColors20.SkinColors[num2 + 1].selector, num);
			return Mathf.Lerp(PawnSkinColors20.SkinColors[num2].melanin, PawnSkinColors20.SkinColors[num2 + 1].melanin, t);
		}

		// Token: 0x06005340 RID: 21312 RVA: 0x001C3BA8 File Offset: 0x001C1DA8
		public static float GetMelaninCommonalityFactor(float melanin)
		{
			int skinDataIndexOfMelanin = PawnSkinColors20.GetSkinDataIndexOfMelanin(melanin);
			if (skinDataIndexOfMelanin == PawnSkinColors20.SkinColors.Length - 1)
			{
				return PawnSkinColors20.GetSkinDataCommonalityFactor(skinDataIndexOfMelanin);
			}
			float t = Mathf.InverseLerp(PawnSkinColors20.SkinColors[skinDataIndexOfMelanin].melanin, b: PawnSkinColors20.SkinColors[skinDataIndexOfMelanin + 1].melanin, melanin);
			return Mathf.Lerp(PawnSkinColors20.GetSkinDataCommonalityFactor(skinDataIndexOfMelanin), PawnSkinColors20.GetSkinDataCommonalityFactor(skinDataIndexOfMelanin + 1), t);
		}

		// Token: 0x06005341 RID: 21313 RVA: 0x001C3C0C File Offset: 0x001C1E0C
		public static float GetRandomMelaninSimilarTo(float value, float clampMin = 0f, float clampMax = 1f)
		{
			return Mathf.Clamp01(Mathf.Clamp(Rand.Gaussian(value, 0.05f), clampMin, clampMax));
		}

		// Token: 0x06005342 RID: 21314 RVA: 0x001C3C28 File Offset: 0x001C1E28
		public static float GetSkinDataCommonalityFactor(int skinDataIndex)
		{
			float num = 0f;
			for (int i = 0; i < PawnSkinColors20.SkinColors.Length; i++)
			{
				num = Mathf.Max(num, PawnSkinColors20.GetTotalAreaWhereClosestToSelector(i));
			}
			return PawnSkinColors20.GetTotalAreaWhereClosestToSelector(skinDataIndex) / num;
		}

		// Token: 0x06005343 RID: 21315 RVA: 0x001C3C64 File Offset: 0x001C1E64
		public static float GetTotalAreaWhereClosestToSelector(int skinDataIndex)
		{
			float num = 0f;
			if (skinDataIndex == 0)
			{
				num += PawnSkinColors20.SkinColors[skinDataIndex].selector;
			}
			else if (PawnSkinColors20.SkinColors.Length > 1)
			{
				num += (PawnSkinColors20.SkinColors[skinDataIndex].selector - PawnSkinColors20.SkinColors[skinDataIndex - 1].selector) / 2f;
			}
			if (skinDataIndex == PawnSkinColors20.SkinColors.Length - 1)
			{
				num += 1f - PawnSkinColors20.SkinColors[skinDataIndex].selector;
			}
			else if (PawnSkinColors20.SkinColors.Length > 1)
			{
				num += (PawnSkinColors20.SkinColors[skinDataIndex + 1].selector - PawnSkinColors20.SkinColors[skinDataIndex].selector) / 2f;
			}
			return num;
		}

		// Token: 0x06005344 RID: 21316 RVA: 0x001C3D24 File Offset: 0x001C1F24
		public static int GetSkinDataIndexOfMelanin(float melanin)
		{
			int result = 0;
			int num = 0;
			while (num < PawnSkinColors20.SkinColors.Length && melanin >= PawnSkinColors20.SkinColors[num].melanin)
			{
				result = num;
				num++;
			}
			return result;
		}

		// Token: 0x04003148 RID: 12616
		public static readonly PawnSkinColors20.SkinColorData[] SkinColors = new PawnSkinColors20.SkinColorData[]
		{
				new PawnSkinColors20.SkinColorData(0.05f, 0f, new Color32(0,100,0, byte.MaxValue)),
				new PawnSkinColors20.SkinColorData(0.1f, 0.06f, new Color32(254, 240, 190, byte.MaxValue)),
				new PawnSkinColors20.SkinColorData(0.15f, 0.11f, new Color32(253, 221, 196, byte.MaxValue)),
				new PawnSkinColors20.SkinColorData(0.2f, 0.16f, new Color32(189,183,107, byte.MaxValue)),
				new PawnSkinColors20.SkinColorData(0.25f, 0.21f, new Color32(255,127,80, byte.MaxValue)),
				new PawnSkinColors20.SkinColorData(0.3f, 0.26f, new Color32(0,255,0, byte.MaxValue)),
				new PawnSkinColors20.SkinColorData(0.35f, 0.31f, new Color32(251, 188, 137, byte.MaxValue)),
				new PawnSkinColors20.SkinColorData(0.4f, 0.36f, new Color32(250, 185, 171, byte.MaxValue)),
				new PawnSkinColors20.SkinColorData(0.45f, 0.41f, new Color32(250, 184, 166, byte.MaxValue)),
				new PawnSkinColors20.SkinColorData(0.5f, 0.46f, new Color32(0,255,255, byte.MaxValue)),
				new PawnSkinColors20.SkinColorData(0.55f, 0.51f, new Color32(217, 168, 127, byte.MaxValue)),
				new PawnSkinColors20.SkinColorData(0.6f, 0.56f, new Color32(205, 156, 115, byte.MaxValue)),
				new PawnSkinColors20.SkinColorData(0.65f, 0.61f, new Color32(191, 132, 105, byte.MaxValue)),
				new PawnSkinColors20.SkinColorData(0.7f, 0.66f, new Color32(189, 126, 95, byte.MaxValue)),
				new PawnSkinColors20.SkinColorData(0.75f, 0.71f, new Color32(151, 113, 66, byte.MaxValue)),
				new PawnSkinColors20.SkinColorData(0.8f, 0.76f, new Color32(128,0,0, byte.MaxValue)),
				new PawnSkinColors20.SkinColorData(0.85f, 0.81f, new Color32(110, 63, 42, byte.MaxValue)),
				new PawnSkinColors20.SkinColorData(0.9f, 0.86f, new Color32(0,128,0, byte.MaxValue)),
				new PawnSkinColors20.SkinColorData(0.95f, 0.91f, new Color32(0,128,128, byte.MaxValue)),
				new PawnSkinColors20.SkinColorData(1f, 0.96f, new Color32(0,0,128, byte.MaxValue))
		};

		// Token: 0x02002316 RID: 8982
		public struct SkinColorData
		{
			// Token: 0x0600C81C RID: 51228 RVA: 0x003F9165 File Offset: 0x003F7365
			public SkinColorData(float melanin, float selector, Color color)
			{
				this.melanin = melanin;
				this.selector = selector;
				this.color = color;
			}

			// Token: 0x040086D5 RID: 34517
			public float melanin;

			// Token: 0x040086D6 RID: 34518
			public float selector;

			// Token: 0x040086D7 RID: 34519
			public Color color;
		}
	}

}
