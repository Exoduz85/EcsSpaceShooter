using Level.Settings.AssetManagement;
using UnityEngine;

namespace Code.Scripts.Services.Prefabs {
	public class PrefabRegistryService {
		PrefabRegistry prefabRegistry;

		public PrefabRegistryService() {
			this.prefabRegistry = Resources.Load<PrefabRegistry>("PrefabRegistry");
		}

		public PlayerVisualsData GetPlayerVisuals() {
			return this.prefabRegistry.PlayerVisualsData;
		}
	}

}