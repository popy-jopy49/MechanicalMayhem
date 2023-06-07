using UnityEngine;
using System.Collections.Generic;

namespace SWAssets.Utils
{
    public class LayerTagUtils
    {
		public static GameObject[] FindObjectsWithLayer(string layer)
		{
			GameObject[] goArray = Object.FindObjectsOfType<GameObject>();
			List<GameObject> goList = new List<GameObject>();

			for (int i = 0; i < goArray.Length; i++)
			{
				if (goArray[i].layer == LayerMask.NameToLayer(layer))
				{
					goList.Add(goArray[i]);
				}
			}

			if (goList.Count <= 0) return null;

			return goList.ToArray();
		}
		public static GameObject[] FindObjectsWithLayer(int layer)
		{
			GameObject[] goArray = Object.FindObjectsOfType<GameObject>();
			List<GameObject> goList = new List<GameObject>();

			for (int i = 0; i < goArray.Length; i++)
			{
				if (goArray[i].layer == layer)
				{
					goList.Add(goArray[i]);
				}
			}

			if (goList.Count <= 0) return null;

			return goList.ToArray();
		}

		// Set all parent and all children to this layer
		public static void SetAllChildrenLayer(Transform parent, int layer)
		{
			parent.gameObject.layer = layer;
			foreach (Transform trans in parent)
			{
				SetAllChildrenLayer(trans, layer);
			}
		}

		public static int OnlyIncluding(params int[] layers)
		{
			return MakeMask(layers);
		}

		public static int Everything()
		{
			return -1;
		}

		public static int Default()
		{
			return 1;
		}

		public static int Nothing()
		{
			return 0;
		}

		public static int EverythingBut(params int[] layers)
		{
			return ~MakeMask(layers);
		}

		public static bool ContainsLayer(LayerMask layerMask, int layer)
		{
			return (layerMask.value & 1 << layer) != 0;
		}

		static int MakeMask(params int[] layers)
		{
			int mask = 0;
			foreach (int item in layers)
			{
				mask |= 1 << item;
			}
			return mask;
		}
	}
}
