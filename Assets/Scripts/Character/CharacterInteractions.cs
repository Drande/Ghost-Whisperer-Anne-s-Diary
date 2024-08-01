using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInteractions : MonoBehaviour {
    private readonly List<IInteractable> interactables = new();

    private void Update() {
        if(!interactables.Any()) return;

        if(Input.GetKeyDown(KeyCode.E)) {
            var movement = GameObject.FindWithTag("Player").GetComponent<PlayerInput>();
            movement.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            interactables.FirstOrDefault()?.Interact(() => {
                movement.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                // Handle any logic after interaction regardless of result.
            });
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<IInteractable>(out var component)) {
            interactables.Add(component);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.TryGetComponent<IInteractable>(out var component)) {
            interactables.Remove(component);
        }
    }
}