using Code.Scripts.Services;
using Code.Scripts.Services.Prefabs;
using Level.Settings.AssetManagement;
using Unity.Entities;
using Unity.Rendering;

namespace Code.Scripts.Systems.Initialization.Early {
	[CreateAfter(typeof(EntitiesGraphicsSystem)), CreateAfter(typeof(InitializeServicesSystem))]
	public partial class RegisterMaterialsAndMeshesSystem : SystemBase {
		EntitiesGraphicsSystem entitiesGraphicsSystem;
		PlayerVisualsData playerVisualsData;

		protected override void OnCreate() {
			RegisterMaterialsAndMeshes();
		}

		protected override void OnUpdate() { }
		
		void RegisterMaterialsAndMeshes() {
			this.playerVisualsData = ServiceLocator<PrefabRegistryService>.Service.GetPlayerVisuals();
			this.entitiesGraphicsSystem = this.World.GetOrCreateSystemManaged<EntitiesGraphicsSystem>();

			this.playerVisualsData.BodyMaterialID = this.entitiesGraphicsSystem.RegisterMaterial(this.playerVisualsData.BodyMaterial);
			this.playerVisualsData.ShipMeshID = this.entitiesGraphicsSystem.RegisterMesh(this.playerVisualsData.ShipMesh);
			
			this.playerVisualsData.InitializeRenderMeshArray();
		}
	}
}