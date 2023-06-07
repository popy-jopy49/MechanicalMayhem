using UnityEngine;
using UnityEditor;

namespace SWAssets
{
    [InitializeOnLoad]
    public class InitializationRequirements
    {
        static InitializationRequirements ()
        {
            //singleton (generics require some hackery to find what we need):
            foreach (GameObject item in Resources.FindObjectsOfTypeAll<GameObject> ()) 
            {
                foreach (Component subItem in item.GetComponents<Component> ())
                {
                    //bypass this component if its currently unavailable due to a broken or missing script:
                    if (subItem == null) continue;

                    string baseType = subItem.GetType().BaseType.ToString();

                    if (baseType.Contains("Singleton")) 
                    {
                        if (!item.GetComponent<Initialization>()) item.AddComponent<Initialization>();
                        continue;
                    }
                }
            }
        }
    }
}