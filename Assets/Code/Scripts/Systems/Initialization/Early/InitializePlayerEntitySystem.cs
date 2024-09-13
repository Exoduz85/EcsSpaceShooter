using Code.Scripts.ComponentData.Input;
using Code.Scripts.ComponentData.Player;
using Code.Scripts.Services;
using Code.Scripts.Services.Prefabs;
using Level.Settings.AssetManagement;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine.Rendering;

namespace Code.Scripts.Systems.Initialization.Early {
	[CreateAfter(typeof(EntitiesGraphicsSystem)), CreateAfter(typeof(InitializeServicesSystem)), CreateAfter(typeof(RegisterMaterialsAndMeshesSystem))]
	public partial class InitializePlayerEntitySystem : SystemBase {
		PlayerVisualsData playerVisualsData;
		EntitiesGraphicsSystem entitiesGraphicsSystem;
		
		protected override void OnCreate() {
			this.playerVisualsData = ServiceLocator<PrefabRegistryService>.Service.GetPlayerVisuals();

			SetupPlayerEntity();
		}

		protected override void OnUpdate() {
		}

		void SetupPlayerEntity() {
			var playerEntity = this.EntityManager.CreateSingleton<PlayerTag>("PlayerEntity");
			
			AddRenderComponents(playerEntity);
			AddTransformAndWorldComponents(playerEntity);
			
			this.EntityManager.AddComponentData(playerEntity, new ThrustInputData());
			this.EntityManager.AddComponentData(playerEntity, new RotationInputData());
			this.EntityManager.AddComponentData(playerEntity, new PhysicsCollider());
		}

		void AddRenderComponents(Entity entity) {
			RenderMeshUtility.AddComponents(entity, 
				this.EntityManager,
				new RenderMeshDescription( 
					shadowCastingMode: ShadowCastingMode.On,
					receiveShadows: true
				),
				this.playerVisualsData.RenderMeshArray,
				MaterialMeshInfo.FromRenderMeshArrayIndices(0,0)
			);
			
			this.EntityManager.AddComponentData(entity, new RenderBounds
			{
				Value = this.playerVisualsData.ShipMesh.bounds.ToAABB()
			});
		}
		
		void AddTransformAndWorldComponents(Entity entity) {
			this.EntityManager.AddComponentData(entity ,new LocalTransform() {
				Position = float3.zero,
				Rotation = quaternion.identity,
				Scale = 100f
			});

			this.EntityManager.AddComponentData(entity, new LocalToWorld() {
				Value = float4x4.Translate(float3.zero)
			});
		}
	}
}