using Code.Scripts.Services;
using Code.Scripts.Services.PlayerInput;
using Code.Scripts.Services.Prefabs;
using Unity.Entities;
using Unity.Rendering;

namespace Code.Scripts.Systems.Initialization.Early {
	[CreateBefore(typeof(RegisterMaterialsAndMeshesSystem)), CreateAfter(typeof(EntitiesGraphicsSystem))]
	public partial class InitializeServicesSystem : SystemBase {
		protected override void OnCreate() {
			RegisterServices();
		}

		protected override void OnUpdate() { }
		
		void RegisterServices() {
			var playerInputActionsService = new PlayerInputActionsService();
			ServiceLocator<PlayerInputActionsService>.Service = playerInputActionsService;

			var prefabRegistryService = new PrefabRegistryService();
			ServiceLocator<PrefabRegistryService>.Service = prefabRegistryService;
		}
	}
}