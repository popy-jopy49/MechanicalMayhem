using UnityEngine;

namespace SWAssets
{
	static class GameObjectExtensionMethods
	{

		public static GameObject FindDeactivatedGameObject(this GameObject go, Transform parent, string objectName) =>
			parent.Find(objectName).gameObject;

		public static GameObject FindDeactivatedGameObject(this GameObject go, string parentName, string objectName) =>
			GameObject.Find(parentName).transform.Find(objectName).gameObject;

		public static GameObject FindDeactivatedGameObject(this GameObject go, Transform parent, int objectIndex) =>
			parent.GetChild(objectIndex).gameObject;

		public static GameObject FindDeactivatedGameObject(this GameObject go, string parentName, int objectIndex) =>
			GameObject.Find(parentName).transform.GetChild(objectIndex).gameObject;

	}
}
