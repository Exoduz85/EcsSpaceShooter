using Code.Scripts.ComponentData.Cam;
using Code.Scripts.ComponentData.Player;
using Code.Scripts.Services;
using Code.Scripts.Services.PlayerInput;
using Code.Scripts.Systems.Initialization.Early;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Scripts.Systems.CameraControls {
	[UpdateAfter(typeof(CameraFollowSystem)), CreateAfter(typeof(InitializePlayerEntitySystem))]
	public partial class CameraRotationSystem : SystemBase {
		readonly float rotationSpeed = 40f;
		Camera mainCamera;
		InputAction rotationAction;

		protected override void OnCreate() {
			var inputActions = ServiceLocator<PlayerInputActionsService>.Service.GetInputActions();
			this.rotationAction = inputActions.CameraControls.Rotation;
		}

		protected override void OnUpdate() {
			this.mainCamera ??= Camera.main;
			if (this.mainCamera == null) return;

			var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
			var currentCameraOffset = SystemAPI.GetComponent<CameraOffsetData>(playerEntity);
			var rotationInput = this.rotationAction.ReadValue<Vector2>();

			if (rotationInput != Vector2.zero) {
				var yaw = rotationInput.x * this.rotationSpeed * SystemAPI.Time.DeltaTime;
				var pitch = -rotationInput.y * this.rotationSpeed * SystemAPI.Time.DeltaTime;
				var cameraRotation = quaternion.Euler(math.radians(pitch), math.radians(yaw), 0);

				currentCameraOffset.Value = math.mul(cameraRotation, currentCameraOffset.Value);
				
				this.EntityManager.SetComponentData(playerEntity, currentCameraOffset);
			}

			var playerTransform = SystemAPI.GetComponent<LocalTransform>(playerEntity);
			var cameraPosition = playerTransform.Position + currentCameraOffset.Value;
			
			this.mainCamera.transform.position = cameraPosition;
			this.mainCamera.transform.rotation = Quaternion.LookRotation(playerTransform.Position - cameraPosition, Vector3.up);
		}
	}
}