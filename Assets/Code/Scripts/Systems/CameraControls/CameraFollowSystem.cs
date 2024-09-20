using Code.Scripts.ComponentData.Cam;
using Code.Scripts.ComponentData.Player;
using Code.Scripts.Systems.Initialization.Early;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Code.Scripts.Systems.CameraControls {
	[CreateAfter(typeof(InitializePlayerEntitySystem))]
	public partial class CameraFollowSystem : SystemBase {
		Camera mainCamera;

        protected override void OnUpdate() {
	        this.mainCamera ??= Camera.main;

	        if (this.mainCamera == null) return;

            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = SystemAPI.GetComponent<LocalTransform>(playerEntity);
            var currentCameraOffset = SystemAPI.GetComponent<CameraOffsetData>(playerEntity);

            var cameraPosition = playerTransform.Position + currentCameraOffset.Value;
            this.mainCamera.transform.position = cameraPosition;
        }
	}
}