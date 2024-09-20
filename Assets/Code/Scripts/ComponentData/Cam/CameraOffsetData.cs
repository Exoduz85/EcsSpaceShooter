using Unity.Entities;
using Unity.Mathematics;

namespace Code.Scripts.ComponentData.Cam {
	public struct CameraOffsetData : IComponentData {
		public float3 Value;
	}
}