using Code.Scripts.ComponentData.Cam;
using Code.Scripts.ComponentData.Player;
using Code.Scripts.Extensions;
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
	public partial class CameraZoomSystem : SystemBase {
		Camera mainCamera;
		InputAction zoomAction;

		readonly float zoomSpeed = 125f;
		readonly float minDistance = 5f;
		readonly float maxDistance = 40f;

		protected override void OnCreate() {
			var inputActions = ServiceLocator<PlayerInputActionsService>.Service.GetInputActions();
			this.zoomAction = inputActions.CameraControls.Zoom;
		}

		protected override void OnUpdate() {
			this.mainCamera ??= Camera.main;
			if (this.mainCamera == null) return;

			var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
			var playerTransform = SystemAPI.GetComponent<LocalTransform>(playerEntity);
			var currentCameraOffset = SystemAPI.GetComponent<CameraOffsetData>(playerEntity);

			var zoomInput = this.zoomAction.ReadValue<Vector2>();
			var newZoom = (zoomInput.x + zoomInput.y) * -1;
			if (newZoom != 0) {
				var camPos = this.mainCamera.transform.position.ToFloat3();
				var currentDistance = math.distance(camPos, playerTransform.Position);
				var direction = math.normalize(camPos - playerTransform.Position);
				var zoomDelta = newZoom * this.zoomSpeed * SystemAPI.Time.DeltaTime;
				var newDistance = currentDistance + zoomDelta;

				newDistance = math.clamp(newDistance, this.minDistance, this.maxDistance);
				var newCameraPosition = playerTransform.Position + (direction * newDistance);

				this.mainCamera.transform.position = newCameraPosition;

				currentCameraOffset.Value = newCameraPosition - playerTransform.Position;
				this.EntityManager.SetComponentData(playerEntity, currentCameraOffset);
			}
		}
	}
}