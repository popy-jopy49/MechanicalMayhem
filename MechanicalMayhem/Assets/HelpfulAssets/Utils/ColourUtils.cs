using UnityEngine;
using System;

namespace SWAssets.Utils
{
	public class ColourUtils
	{
		public static bool Color32Equal(Color32 colA, Color32 colB)
		{
			bool r = colA.r == colB.r;
			bool g = colA.g == colB.g;
			bool b = colA.b == colB.b;
			bool a = colA.a == colB.a;
			return r && g && b && a;
		}

		// Return a color going from Red to Yellow to Green, like a heat map
		public static Color GetRedGreenColor(float value)
		{
			float r = value <= .5f ? 1f : 1f - (value - .5f) * 2f;
			float g = value <= .5f ? value * 2f : 1f;
			return new Color(r, g, 0f, 1f);
		}

		#region HexDec
		// Returns 00-FF, value 0->255
		public static string IntToHex(int value) => value.ToString("X2");

		// Returns 0-255
		public static int HexToInt(string hex) => Convert.ToInt32(hex, 16);

		// Returns a hex string based on a number between 0->1
		public static string DecimalToHex(float value) => IntToHex((int)Mathf.Round(value * 255f));

		// Returns a float between 0->1
		public static float HexToDecimal(string hex) => HexToInt(hex) / 255f;

		// Get Hex Color FF00FF
		public static string GetStringFromColor(Color color)
		{
			string red = DecimalToHex(color.r);
			string green = DecimalToHex(color.g);
			string blue = DecimalToHex(color.b);
			return red + green + blue;
		}

		// Get Hex Color FF00FFAA
		public static string GetStringFromColorWithAlpha(Color color) => GetStringFromColor(color) + DecimalToHex(color.a);

		// Sets out values to Hex String 'FF'
		public static void GetStringFromColor(Color color, out string red, out string green, out string blue, out string alpha)
		{
			red = DecimalToHex(color.r);
			green = DecimalToHex(color.g);
			blue = DecimalToHex(color.b);
			alpha = DecimalToHex(color.a);
		}

		// Get Hex Color FF00FF
		public static string GetStringFromColor(float r, float g, float b)
		{
			string red = DecimalToHex(r);
			string green = DecimalToHex(g);
			string blue = DecimalToHex(b);
			return red + green + blue;
		}

		// Get Hex Color FF00FFAA
		public static string GetStringFromColor(float r, float g, float b, float a) => GetStringFromColor(r, g, b) + DecimalToHex(a);

		// Get Color from Hex string FF00FFAA
		public static Color GetColorFromString(string color)
		{
			float red = HexToDecimal(color.Substring(0, 2));
			float green = HexToDecimal(color.Substring(2, 2));
			float blue = HexToDecimal(color.Substring(4, 2));
			float alpha = 1f;
			if (color.Length >= 8)
			{
				// Color string contains alpha
				alpha = HexToDecimal(color.Substring(6, 2));
			}
			return new Color(red, green, blue, alpha);
		}
		#endregion

		#region Sequencial
		private static int sequencialColorIndex = -1;
		private static Color[] sequencialColors = new[] {
			GetColorFromString("26a6d5"),
			GetColorFromString("41d344"),
			GetColorFromString("e6e843"),
			GetColorFromString("e89543"),
			GetColorFromString("0f6ad0"),//("d34141"),
		    GetColorFromString("b35db6"),
			GetColorFromString("c45947"),
			GetColorFromString("9447c4"),
			GetColorFromString("4756c4"),
		};

		public static void ResetSequencialColors() => sequencialColorIndex = -1;

		public static Color GetSequencialColor() => sequencialColors[(sequencialColorIndex + 1) % sequencialColors.Length];
		#endregion

		#region Arrays
		public static void MergeColorArrays(Color[] baseArray, Color[] overlay)
		{
			for (int i = 0; i < baseArray.Length; i++)
			{
				if (overlay[i].a > 0)
				{
					// Not empty color
					if (overlay[i].a >= 1)
					{
						// Fully replace
						baseArray[i] = overlay[i];
					}
					else
					{
						// Interpolate colors
						float alpha = overlay[i].a;
						baseArray[i].r += (overlay[i].r - baseArray[i].r) * alpha;
						baseArray[i].g += (overlay[i].g - baseArray[i].g) * alpha;
						baseArray[i].b += (overlay[i].b - baseArray[i].b) * alpha;
						baseArray[i].a += alpha;
					}
				}
			}
		}

		public static void ReplaceColorArrays(Color[] baseArray, Color[] replaceArray, Color ignoreColor)
		{
			for (int i = 0; i < baseArray.Length; i++)
			{
				if (baseArray[i] != ignoreColor)
				{
					baseArray[i] = replaceArray[i];
				}
			}
		}

		public static void MaskColorArrays(Color[] baseArray, Color[] mask)
		{
			for (int i = 0; i < baseArray.Length; i++)
			{
				if (baseArray[i].a > 0f)
				{
					baseArray[i].a = mask[i].a;
				}
			}
		}

		public static void TintColorArray(Color[] baseArray, Color tint)
		{
			for (int i = 0; i < baseArray.Length; i++)
			{
				// Apply tint
				baseArray[i].r = tint.r * baseArray[i].r;
				baseArray[i].g = tint.g * baseArray[i].g;
				baseArray[i].b = tint.b * baseArray[i].b;
			}
		}

		public static void TintColorArrayInsideMask(Color[] baseArray, Color tint, Color[] mask)
		{
			for (int i = 0; i < baseArray.Length; i++)
			{
				if (mask[i].a > 0)
				{
					// Apply tint
					Color baseColor = baseArray[i];
					Color fullyTintedColor = tint * baseColor;
					float interpolateAmount = mask[i].a;
					baseArray[i].r = baseColor.r + (fullyTintedColor.r - baseColor.r) * interpolateAmount;
					baseArray[i].g = baseColor.g + (fullyTintedColor.g - baseColor.g) * interpolateAmount;
					baseArray[i].b = baseColor.b + (fullyTintedColor.b - baseColor.b) * interpolateAmount;
				}
			}
		}

		public static Color TintColor(Color baseColor, Color tint)
		{
			// Apply tint
			baseColor.r = tint.r * baseColor.r;
			baseColor.g = tint.g * baseColor.g;
			baseColor.b = tint.b * baseColor.b;

			return baseColor;
		}
		#endregion

		#region Similar
		public static bool IsColorSimilar255(Color colorA, Color colorB, int maxDiff) => IsColorSimilar(colorA, colorB, maxDiff / 255f);

		public static bool IsColorSimilar(Color colorA, Color colorB, float maxDiff) => GetColorDifference(colorA, colorB) < maxDiff;

		public static float GetColorDifference(Color colorA, Color colorB)
		{
			float rDiff = Mathf.Abs(colorA.r - colorB.r);
			float gDiff = Mathf.Abs(colorA.g - colorB.g);
			float bDiff = Mathf.Abs(colorA.b - colorB.b);
			float aDiff = Mathf.Abs(colorA.a - colorB.a);

			float totalDiff = rDiff + gDiff + bDiff + aDiff;
			return totalDiff;
		}
		#endregion
	}
}
