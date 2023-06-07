using UnityEngine;

namespace SWAssets.Utils
{
	public static class Parse
	{
		// Parse a float, return default if failed
		public static float Float(string txt, float _default) => float.TryParse(txt, out float f) ? f : _default;

		// Parse a int, return default if failed
		public static int Int(string txt, int _default) => int.TryParse(txt, out int i) ? i : _default;

		public static int Int(string txt) => Int(txt, -1);
	}
}
