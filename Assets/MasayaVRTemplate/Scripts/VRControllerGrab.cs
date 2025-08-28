using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static VRController;

public class VRControllerGrab : MonoBehaviour
{
    VRController controller;
    List<Transform> grabbableList = new List<Transform>();
    public IGrabbable currentHeld { get; private set; }

    private void Start()
    {
        controller = GetComponent<VRController>();
        controller.onGrabStart += GrabStart;
        controller.onGrabEnd += GrabEnd;
    }

    private void OnDisable()
    {
        controller.onGrabStart -= GrabStart;
        controller.onGrabEnd -= GrabEnd;
    }

    [ContextMenu("Grab Start")]
    public void GrabStart()
    {
        if(grabbableList.Count > 0)
        {
            currentHeld = grabbableList[0].GetComponent<IGrabbable>();
            currentHeld.GrabStart(this);
        }
    }

    [ContextMenu("Grab End")]
    public void GrabEnd()
    {
        if(currentHeld != null)
        {
            currentHeld.GrabEnd();
        }
    }

    public void GrabGone(bool removeFromList, Transform obj)
    {
        if (removeFromList)
        {
            grabbableList.Remove(obj);
        }
        currentHeld = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            grabbableList.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            grabbableList.Remove(other.transform);
            if (grabbableList.Count == 0)
                grabbableList.Clear();
        }
    }
}
