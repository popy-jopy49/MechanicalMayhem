using UnityEngine;

namespace SWAssets.Utils
{
    public static class VectorUtils
	{
		#region Angle-Vector Conversions
		public static Vector3 GetVectorFromAngle(int angle)
		{
			float angleRad = angle * (Mathf.PI / 180f);
			return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
		}

		public static Vector3 GetVectorFromAngle(float angle)
		{
			float angleRad = angle * (Mathf.PI / 180f);
			return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
		}

		public static float GetAngleFromVector(Vector3 dir, bool one80 = false)
		{
			dir = dir.normalized;
			float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			if (n < 0 && one80) n += 360;

			return n;
		}

		public static float GetAngleFromVectorXZ(Vector3 dir, bool one80 = false)
		{
			dir = dir.normalized;
			float n = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
			if (n < 0 && one80) n += 360;

			return n;
		}

		public static int GetAngleFromVectorInt(Vector3 dir, bool one80 = false)
		{
			dir = dir.normalized;
			float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			if (n < 0 && one80) n += 360;
			int angle = Mathf.RoundToInt(n);

			return angle;
		}

		public static int GetAngleFromVectorIntXZ(Vector3 dir, bool one80 = false)
		{
			dir = dir.normalized;
			float n = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
			if (n < 0 && one80) n += 360;
			int angle = Mathf.RoundToInt(n);

			return angle;
		}

		public static Vector3 ApplyRotationToVector(Vector3 vec, Vector3 vecRotation)
		{
			return ApplyRotationToVector(vec, VectorUtils.GetAngleFromVector(vecRotation));
		}

		public static Vector3 ApplyRotationToVector(Vector3 vec, float angle)
		{
			return Quaternion.Euler(0, 0, angle) * vec;
		}

		public static Vector3 ApplyRotationToVector3D(Vector3 vec, float angle)
		{
			return Quaternion.Euler(0, angle, 0) * vec;
		}
		#endregion

		#region Distance Methods
		public static float GetDistanceInLine(float startPos, float endPos) => Mathf.Abs(startPos - endPos);
		public static Vector2 GetDistanceIn2DShape(Vector2 startPos, Vector2 endPos)
		{
			float x = GetDistanceInLine(startPos.x, endPos.x);
			float y = GetDistanceInLine(startPos.y, endPos.y);
			return new Vector2(x, y);
		}
		#endregion

		#region Interpolation
		//interpolation:
		/// <summary>
		/// Linear interpolation.
		/// </summary>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		/// <param name="percentage">Percentage.</param>
		public static float LinearInterpolate(float from, float to, float percentage)
		{
			return (to - from) * percentage + from;
		}

		/// <summary>
		/// Linear interpolation.
		/// </summary>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		/// <param name="percentage">Percentage.</param>
		public static Vector2 LinearInterpolate(Vector2 from, Vector2 to, float percentage)
		{
			return new Vector2(LinearInterpolate(from.x, to.x, percentage), LinearInterpolate(from.y, to.y, percentage));
		}

		/// <summary>
		/// Linear interpolation.
		/// </summary>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		/// <param name="percentage">Percentage.</param>
		public static Vector3 LinearInterpolate(Vector3 from, Vector3 to, float percentage)
		{
			return new Vector3(LinearInterpolate(from.x, to.x, percentage), LinearInterpolate(from.y, to.y, percentage), LinearInterpolate(from.z, to.z, percentage));
		}

		/// <summary>
		/// Linear interpolation.
		/// </summary>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		/// <param name="percentage">Percentage.</param>
		public static Vector4 LinearInterpolate(Vector4 from, Vector4 to, float percentage)
		{
			return new Vector4(LinearInterpolate(from.x, to.x, percentage), LinearInterpolate(from.y, to.y, percentage), LinearInterpolate(from.z, to.z, percentage), LinearInterpolate(from.w, to.w, percentage));
		}

		/// <summary>
		/// Linear interpolation.
		/// </summary>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		/// <param name="percentage">Percentage.</param>
		public static Rect LinearInterpolate(Rect from, Rect to, float percentage)
		{
			return new Rect(LinearInterpolate(from.x, to.x, percentage), LinearInterpolate(from.y, to.y, percentage), LinearInterpolate(from.width, to.width, percentage), LinearInterpolate(from.height, to.height, percentage));
		}

		/// <summary>
		/// Linear interpolation.
		/// </summary>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		/// <param name="percentage">Percentage.</param>
		public static Color LinearInterpolate(Color from, Color to, float percentage)
		{
			return new Color(LinearInterpolate(from.r, to.r, percentage), LinearInterpolate(from.g, to.g, percentage), LinearInterpolate(from.b, to.b, percentage), LinearInterpolate(from.a, to.a, percentage));
		}

		/// <summary>
		/// Linear interpolation.
		/// </summary>
		/// <param name="from">From.</param>
		/// <param name="to">To.</param>
		/// <param name="percentage">Percentage.</param>
		public static Quaternion LinearInterpolate(Quaternion from, Quaternion to, float percentage)
		{
			return new Quaternion(LinearInterpolate(from.x, to.x, percentage), LinearInterpolate(from.y, to.y, percentage), LinearInterpolate(from.z, to.z, percentage), LinearInterpolate(from.w, to.w, percentage));
		}
		#endregion

		#region Animation Curves
		//animation curves:
		/// <summary>
		/// Evaluates the curve.
		/// </summary>
		/// <returns>The value evaluated at the percentage of the clip.</returns>
		/// <param name="curve">Curve.</param>
		/// <param name="percentage">Percentage.</param>
		public static float EvaluateCurve(AnimationCurve curve, float percentage)
		{
			return curve.Evaluate(curve[curve.length - 1].time * percentage);
		}
		#endregion

		#region Rectangle Calculations
		public static Vector3 GetRandomPositionWithinRectangle(float xMin, float xMax, float yMin, float yMax) =>
			new Vector3(UnityEngine.Random.Range(xMin, xMax), UnityEngine.Random.Range(yMin, yMax));

		public static Vector3 GetRandomPositionWithinRectangle(Vector3 lowerLeft, Vector3 upperRight) =>
			new Vector3(UnityEngine.Random.Range(lowerLeft.x, upperRight.x), UnityEngine.Random.Range(lowerLeft.y, upperRight.y));

		public static Vector3 GetRandomPositionWithinRectangle(Rect rect) =>
			new Vector3(UnityEngine.Random.Range(0, rect.width * 2), UnityEngine.Random.Range(0, rect.height * 2));
		#endregion
	}
}
