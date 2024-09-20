using Code.Scripts.ComponentData.Input;
using Code.Scripts.ComponentData.Player;
using Code.Scripts.Systems.Input;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

namespace Code.Scripts.Systems.Movement {
	[CreateAfter(typeof(ProcessInputSystem))]
	public partial class ApplyThrustSystem : SystemBase {
        protected override void OnUpdate() {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();

            var thrustInput = this.EntityManager.GetComponentData<ThrustInputData>(playerEntity);
            var currentThrust = this.EntityManager.GetComponentData<CurrentThrustData>(playerEntity);
            var maxThrust = this.EntityManager.GetComponentData<MaxThrustData>(playerEntity);
            var thrustAcceleration = this.EntityManager.GetComponentData<ThrustAccelerationData>(playerEntity);
            var physicsVelocity = this.EntityManager.GetComponentData<PhysicsVelocity>(playerEntity);

            var deltaTime = SystemAPI.Time.DeltaTime;
            currentThrust.Value = math.clamp(
                currentThrust.Value + thrustAcceleration.Value * thrustInput.Value * deltaTime, 
                -maxThrust.Value, 
                maxThrust.Value
            );

            if (currentThrust.Value != 0) {
                var thrustForce = math.right() * currentThrust.Value * deltaTime;
                physicsVelocity.Linear += thrustForce;
                this.EntityManager.SetComponentData(playerEntity, physicsVelocity);
            }

            this.EntityManager.SetComponentData(playerEntity, currentThrust);
        }
	}
}