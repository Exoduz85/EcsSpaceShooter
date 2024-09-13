using System;
using Unity.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

namespace Level.Settings.AssetManagement {
	[CreateAssetMenu(menuName = "ECS/Prefab Registry", fileName = "New Prefab Registry")]
	public class PrefabRegistry : ScriptableObject {
		public PlayerVisualsData PlayerVisualsData;
	}

	[Serializable]
	public class PlayerVisualsData {
		public Mesh ShipMesh;
		public Material BodyMaterial;

		public BatchMaterialID BodyMaterialID;
		public BatchMeshID ShipMeshID;
		
		[NonSerialized] public RenderMeshArray RenderMeshArray;

		public void InitializeRenderMeshArray()
		{
			this.RenderMeshArray = new RenderMeshArray(
				new [] { this.BodyMaterial },
				new [] { this.ShipMesh },
				new []{new MaterialMeshIndex(){ MaterialIndex = 0, MeshIndex = 0 }}
			);
		}
	}
}