using Unity.Mathematics;
using UnityEngine;

namespace Code.Scripts.Extensions {
	public static class VectorToFloat3Extensions {
		public static float3 ToFloat3(this Vector2 vector2) {
			return ToFloat3((Vector3)vector2);
		}
		
		public static float3 ToFloat3(this Vector3 vector3) {
			return new float3(vector3.x, vector3.y, vector3.z);
		}
	}
}