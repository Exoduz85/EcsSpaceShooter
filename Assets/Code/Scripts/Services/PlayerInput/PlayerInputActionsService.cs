﻿using System;
using UnityEngine.InputSystem;

namespace Code.Scripts.Services.PlayerInput {
	public class PlayerInputActionsService {
		PlayerInputActions inputActions;
		
		public void CreateInputActions() {
			this.inputActions = new PlayerInputActions();
		}

		public void SetInputActionsActiveState(bool enabled = true) {
			if (enabled) {
				this.inputActions.Enable();
				return;
			}
			this.inputActions.Disable();
		}

		public PlayerInputActions GetInputActions() {
			return this.inputActions;
		}

		public bool TryGetAction(Guid guid, out InputAction inputAction) {
			inputAction = this.inputActions.FindAction(guid.ToString());
			return inputAction != null;
		}
	}
}