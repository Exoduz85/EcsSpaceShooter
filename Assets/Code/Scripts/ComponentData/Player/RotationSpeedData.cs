using Unity.Entities;

namespace Code.Scripts.ComponentData.Player {
	public struct RotationSpeedData : IComponentData {
		public float PitchSpeed;
		public float YawSpeed;
		public float RollSpeed;
	}
}