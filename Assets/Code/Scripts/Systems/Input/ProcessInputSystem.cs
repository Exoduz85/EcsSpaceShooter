using Code.Scripts.ComponentData.Input;
using Code.Scripts.ComponentData.Player;
using Code.Scripts.Services;
using Code.Scripts.Services.PlayerInput;
using Code.Scripts.Systems.Initialization.Early;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Scripts.Systems.Input {
	[CreateAfter(typeof(InitializeInputActionsSystem))]
	public partial class ProcessInputSystem : SystemBase {
		InputAction thrust;
		InputAction fire;
		InputAction pitch;
		InputAction yaw;
		InputAction roll;

		protected override void OnCreate() {
			var inputActionService = ServiceLocator<PlayerInputActionsService>.Service;
			var shipControls = inputActionService.GetInputActions().ShipControls;
			this.thrust = shipControls.Thrust;
			this.fire = shipControls.Fire;
			this.pitch = shipControls.Pitch;
			this.yaw = shipControls.Yaw;
			this.roll = shipControls.Roll;
		}

		protected override void OnUpdate() {
			var thrust = this.thrust.ReadValue<float>();
			var fire = this.fire.ReadValue<float>();
			var pitch = this.pitch.ReadValue<float>();
			var yaw = this.yaw.ReadValue<float>();
			var roll = this.roll.ReadValue<float>();
			
			var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
			
			if (playerEntity != Entity.Null) {
				Debug.Log($"New thrust: {thrust}");
				var thrustInputData = this.EntityManager.GetComponentData<ThrustInputData>(playerEntity);
				thrustInputData.Thrust = thrust;
				this.EntityManager.SetComponentData(playerEntity, thrustInputData);
			}
		}
	}
}