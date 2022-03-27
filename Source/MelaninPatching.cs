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

		public static bool prefix(Color __result) { 
		__result = PawnSkinColors20.GetSkinColor(1);
         return false;   }
		public static Color GetSkinColor(float melanin) { PawnSkinColors20.GetSkinColor(melanin)}

	}
}