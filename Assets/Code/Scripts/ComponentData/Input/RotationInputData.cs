using Unity.Entities;
using Unity.Mathematics;

namespace Code.Scripts.ComponentData.Input {
	public struct RotationInputData : IComponentData {
		public float3 Rotation;
	}
}