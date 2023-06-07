using UnityEngine;

namespace SWAssets.Utils
{
	public class Mouse
	{
		public static Vector2 Position => UnityEngine.InputSystem.Mouse.current.position.ReadValue();

		public enum Button
		{
			Left = 0,
			Right = 1,
			Middle = 2
		}
	}

	public class Mouse2D
	{
		public static Vector3 GetMouseWorldPosition()
		{
			Vector3 vec = GetMouseWorldPositionWithZ(Mouse.Position, Camera.main);
			vec.z = 0f;
			return vec;
		}

		public static Vector3 GetMouseWorldPositionWithZ() => GetMouseWorldPositionWithZ(Mouse.Position, Camera.main);

		public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera) => GetMouseWorldPositionWithZ(Mouse.Position, worldCamera);

		public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera) => worldCamera.ScreenToWorldPoint(screenPosition);
	}

	public class Mouse3D
	{
		public static Vector3 GetMouseWorldPosition()
		{
			Vector3 v3 = GetMouseWorldPositionWithZ(Mouse.Position, Camera.main);
			v3.z = 0;
			return v3;
		}

		public static Vector3 GetMouseWorldPositionWithZ() => GetMouseWorldPositionWithZ(Mouse.Position, Camera.main);

		public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera) => GetMouseWorldPositionWithZ(Mouse.Position, worldCamera);

		public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
		{
			Ray ray = worldCamera.ScreenPointToRay(screenPosition);
			if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
			{
				return hitInfo.point;
			}

			return Vector3.zero;
		}

		public static Transform CollidingWith() =>
			Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.Position), out RaycastHit hitInfo, Mathf.Infinity) ?
			hitInfo.transform : null;

		public static Vector3 GetDirToMouse(Vector3 fromPosition) => (GetMouseWorldPosition() - fromPosition).normalized;
	}
}
