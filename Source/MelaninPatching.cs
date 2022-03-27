using MiscMelanin;
using HarmonyLib;
using Verse;
using RimWorld;
using UnityEngine;

namespace MelaninPatching
{
	[StaticConstructorOnStartup]
	internal static class HarmonyPatches

	{
		public static Harmony harmony;
		static HarmonyPatches()
		{
			harmony = new Harmony("JavierLoustaunau.MiscMelanin");
			harmony.PatchAll();
		}
		[HarmonyReversePatch]
		[HarmonyPatch(typeof(Pawn_StoryTracker), "get_SkinColor")]
		public static bool prefix(Color __result, Pawn_StoryTracker __instance)
		{
			if (!__instance.skinColorOverride.HasValue)
			{
				__result = PawnSkinColors20.GetSkinColor(__instance.melanin);
			}
			__result = __instance.skinColorOverride.Value;
			return false;
		}
	}
}