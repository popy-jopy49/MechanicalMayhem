using UnityEngine;
using System;
using System.Threading.Tasks;

namespace SWAssets.Utils
{
	public static class ClassAccessor
	{
		public static object CallMethod(string typeName, string methodName) => CallMethod(typeName, methodName, null);
		public static object CallMethod(string typeName, string methodName, object[] parameters) => Type.GetType(typeName).GetMethod(methodName).Invoke(null, parameters);
		public static object GetField(string typeName, string fieldName) => Type.GetType(typeName).GetField(fieldName).GetValue(null);
		public static Type GetNestedType(string typeName, string nestedTypeName) => Type.GetType(typeName).GetNestedType(nestedTypeName);
	}

	public static class Utils
	{
		// Screen Shake - MUST HAVE
		public static async Task ShakeCamera(float intensity, float timer, Camera worldCamera = null)
		{
			if (!worldCamera) worldCamera = Camera.main;

			Vector3 lastCameraMovement = Vector3.zero;

			while (timer > 0f)
			{
				timer -= Time.deltaTime;
				Vector3 randomMovement = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized * intensity;
				worldCamera.transform.localPosition -= lastCameraMovement + randomMovement;
				lastCameraMovement = randomMovement;
				await Task.Yield();
			}
		}
	}

}
