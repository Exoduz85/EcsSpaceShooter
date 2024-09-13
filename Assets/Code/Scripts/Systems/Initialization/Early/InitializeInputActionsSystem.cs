using Code.Scripts.Services;
using Code.Scripts.Services.PlayerInput;
using Code.Scripts.Systems.Input;
using Unity.Entities;

namespace Code.Scripts.Systems.Initialization.Early {
	[CreateBefore(typeof(ProcessInputSystem)), CreateAfter(typeof(InitializeServicesSystem))]
	public partial class InitializeInputActionsSystem : SystemBase {
		protected override void OnCreate() {
			var playerInputActionsService = ServiceLocator<PlayerInputActionsService>.Service;
			playerInputActionsService.CreateInputActions();
			playerInputActionsService.SetInputActionsActiveState();
		}

		protected override void OnUpdate() {
		}
	}
}