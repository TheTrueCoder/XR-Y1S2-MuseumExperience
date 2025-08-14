using System.Collections.Generic;
using UnityEngine;

public class VRControllerInteraction : MonoBehaviour
{
    VRController controller;
    List<IInteractable> interactionObjects = new List<IInteractable>();
    public IInteractable currentInteraction { get; private set; }

    private void Start()
    {
        controller = GetComponent<VRController>();
        controller.onInteractStart += InteractStart;
        controller.onInteractEnd += InteractEnd;
    }

    private void OnDisable()
    {
        controller.onInteractStart -= InteractStart;
        controller.onInteractEnd -= InteractEnd;
    }

    //Called when the interact button is pressed
    public void InteractStart()
    {
        if (currentInteraction != null)
        {
            currentInteraction.Interact();
        }
        else
        {
            if(interactionObjects.Count != 0)
            {
                currentInteraction = interactionObjects[0];
                currentInteraction.InteractStart(this);
            }
        }
    }

    //Called when the interact button is released
    public void InteractEnd()
    {
        if (currentInteraction != null)
        {
            currentInteraction.InteractEnd();
        }
    }

    //Can be called by the interactable object or other controller
    public void InteractFinish(bool removeFromList)
    {
        if (removeFromList)
        {
            interactionObjects.Remove(currentInteraction);
        }
        currentInteraction = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            interactionObjects.Add(other.GetComponent<IInteractable>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            interactionObjects.Remove(other.GetComponent<IInteractable>());
            if (interactionObjects.Count == 0)
                interactionObjects.Clear();
        }
    }
}
