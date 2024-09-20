using Code.Scripts.ComponentData.Input;
using Code.Scripts.ComponentData.Player;
using Code.Scripts.Systems.Input;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

namespace Code.Scripts.Systems.Movement {
	[CreateAfter(typeof(ProcessInputSystem))]
	public partial class ApplyRotationSystem : SystemBase {
		protected override void OnUpdate() {
			var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            
			if (playerEntity == Entity.Null) return;

			var pitchInput = this.EntityManager.GetComponentData<PitchInputData>(playerEntity).Value;
			var yawInput = this.EntityManager.GetComponentData<YawInputData>(playerEntity).Value;
			var rollInput = this.EntityManager.GetComponentData<RollInputData>(playerEntity).Value;
			var rotationSpeedData = this.EntityManager.GetComponentData<RotationSpeedData>(playerEntity);
			var physicsVelocity = this.EntityManager.GetComponentData<PhysicsVelocity>(playerEntity);

			var deltaTime = SystemAPI.Time.DeltaTime;

			var angularChange = new float3(
				math.radians(pitchInput * rotationSpeedData.PitchSpeed * deltaTime),
				math.radians(yawInput * rotationSpeedData.YawSpeed * deltaTime),
				math.radians(-rollInput * rotationSpeedData.RollSpeed * deltaTime)    
			);

			physicsVelocity.Angular += angularChange;

			this.EntityManager.SetComponentData(playerEntity, physicsVelocity);
		}
	}
}