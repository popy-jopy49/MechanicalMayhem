using UnityEngine;
using SWAssets.Utils;

namespace SWAssets
{
	public static class TransformExtensionMethods
	{
		public static void ScaleTransformFromLine(this Transform target, Vector2 startPos, Vector2 endPos)
		{
			Vector3 centerPos = (endPos + startPos) / 2f;

			float scaleX = Vector2.Distance(startPos, endPos) / 2f;

			target.position = centerPos;
			target.localScale = new Vector3(scaleX, -0.32f, 1f);
		}
		public static void ScaleTransformFrom2D(this Transform target, Vector2 startPos, Vector2 endPos)
		{
			Vector3 centerPos = (endPos + startPos) / 2f;

			Vector2 scale = VectorUtils.GetDistanceIn2DShape(startPos, endPos);

			target.position = centerPos;
			target.localScale = new Vector3(scale.x, scale.y, 1f);
		}

		public static void PointTransformAt2D(this Transform thisTransform, Vector3 position)
		{
			Vector3 dir = (position - thisTransform.position).normalized;

			float angle = VectorUtils.GetAngleFromVector(dir);
			thisTransform.eulerAngles = new Vector3(0f, 0f, angle + 180f);
		}

	}
}
