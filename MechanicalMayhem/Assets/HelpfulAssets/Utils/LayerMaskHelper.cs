using UnityEngine;

namespace SWAssets.Utils
{
    public class LayerMaskHelper
    {
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