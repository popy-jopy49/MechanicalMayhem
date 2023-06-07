using UnityEngine;
using System.Collections.Generic;

namespace SWAssets.Utils
{
    public class ArrayUtils
	{
		public static T[] GetComponentFromArray<T>(GameObject[] gameObjects)
		{
			T[] components = new T[gameObjects.Length];

			for (int i = 0; i < gameObjects.Length; i++) components[i] = gameObjects[i].GetComponent<T>();

			return components.Length <= 0 ? null : components;
		}

		public static T[] GetComponentFromArray<T>(Transform[] transforms)
		{
			return GetComponentFromArray<T>(ConvertTransformArrayToGameObject(transforms));
		}

		public static GameObject[] ConvertTransformArrayToGameObject(Transform[] transforms)
		{
			GameObject[] go = new GameObject[transforms.Length];
			for (int i = 0; i < transforms.Length; i++)
			{
				go[i] = transforms[i].gameObject;
			}

			return go;
		}

		public static T[] ArrayAdd<T>(T[] arr, T add)
		{
			T[] ret = new T[arr.Length + 1];
			for (int i = 0; i < arr.Length; i++)
			{
				ret[i] = arr[i];
			}
			ret[arr.Length] = add;
			return ret;
		}

		public static T GetRandom<T>(T[] array) => array[Random.Range(0, array.Length)];

		public static void ShuffleArray<T>(T[] arr, int iterations)
		{
			for (int i = 0; i < iterations; i++)
			{
				int rnd = Random.Range(0, arr.Length);
				T tmp = arr[rnd];
				arr[rnd] = arr[0];
				arr[0] = tmp;
			}
		}

		public static void ShuffleList<T>(List<T> list, int iterations)
		{
			for (int i = 0; i < iterations; i++)
			{
				int rnd = Random.Range(0, list.Count);
				T tmp = list[rnd];
				list[rnd] = list[0];
				list[0] = tmp;
			}
		}

		public static IEnumerable<T> RemoveDuplicates<T>(IEnumerable<T> arr)
		{
			List<T> list = new List<T>();
			foreach (T t in arr)
			{
				if (!list.Contains(t))
				{
					list.Add(t);
				}
			}
			return list;
		}
	}
}
