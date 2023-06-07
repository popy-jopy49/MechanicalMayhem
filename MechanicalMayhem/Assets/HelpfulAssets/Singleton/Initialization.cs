using UnityEngine;
using System.Reflection;

namespace SWAssets
{
    public class Initialization : MonoBehaviour
    {
        //Init:
        void Awake()
        {
            //singleton initialization:
            InitializeSingleton();
        }

        //Private Methods:
        void InitializeSingleton()
        {
            foreach (Component item in GetComponents<Component>())
            {
                string baseType = item.GetType().BaseType.ToString();

                if (baseType.Contains("Singleton"))
                {
                    MethodInfo m = item.GetType().BaseType.GetMethod("Initialize", BindingFlags.NonPublic | BindingFlags.Instance);

                    m.Invoke(item, new Component[] { item });
                }
            }
        }
    }
}