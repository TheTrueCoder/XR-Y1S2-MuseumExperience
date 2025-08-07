using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.XR;

public class VRController : MonoBehaviour
{
    bool initialised;
    [SerializeField] ControllerHand controllerHand;
    VRRig vrRig;
    VRActions inputs;
    XRController controller;
    public ControllerValues controllerValues { get; private set; }

    public enum ControllerHand
    {
        left,
        right
    }

    public ControllerHand GetHand()
    {
        return controllerHand;
    }

    public void Initialise(VRRig rig)
    {
        vrRig = rig;
        StartCoroutine(LinkingController());
    }

    IEnumerator LinkingController()
    {
        controllerValues = new ControllerValues();
        while(controller == null){
            switch (controllerHand)
            {
                case ControllerHand.left:
                    controller = InputSystem.GetDevice<XRController>(UnityEngine.InputSystem.CommonUsages.LeftHand);
                    break;
                case ControllerHand.right:
                    controller = InputSystem.GetDevice<XRController>(UnityEngine.InputSystem.CommonUsages.RightHand);
                    break;
            }

            yield return null;
        }
        inputs = new VRActions();
        InputUser inputUser = InputUser.PerformPairingWithDevice(controller.device);
        inputUser.AssociateActionsWithUser(inputs);
        inputs.VRInputs.TriggerPressed.performed += ctx => { OnTriggerPressed(ctx); controllerValues.triggerPressed = true; };
        inputs.VRInputs.TriggerPressed.canceled += ctx => { OnTriggerReleased(ctx); controllerValues.triggerPressed = false; };
        inputs.VRInputs.GripPressed.performed += ctx => { OnGripPressed(ctx); controllerValues.gripPressed = true; };
        inputs.VRInputs.GripPressed.canceled += ctx => { OnGripReleased(ctx); controllerValues.gripPressed = false; };
        inputs.VRInputs.AnalogPressed.performed += ctx => { OnAnalogPressed(ctx); controllerValues.analogPressed = true; };
        inputs.VRInputs.AnalogPressed.canceled += ctx => { OnAnalogReleased(ctx); controllerValues.analogPressed = false; };
        inputs.VRInputs.PrimaryButtonPressed.performed += ctx => { OnPrimaryPressed(ctx); controllerValues.primaryButtonPressed = true; };
        inputs.VRInputs.PrimaryButtonPressed.canceled += ctx => { OnPrimaryReleased(ctx); controllerValues.primaryButtonPressed = false; };
        inputs.VRInputs.SecondaryButtonPressed.performed += ctx => { OnSecondaryPressed(ctx); controllerValues.secondaryButtonPressed = true; };
        inputs.VRInputs.SecondaryButtonPressed.canceled += ctx => { OnSecondaryReleased(ctx); controllerValues.secondaryButtonPressed = false; };
        inputs.Enable();

        initialised = true;
    }



    private void Update()
    {
        if (!initialised)
            return;

        controllerValues.triggerValue = inputs.VRInputs.TriggerValue.ReadValue<float>();
        controllerValues.gripValue = inputs.VRInputs.GripValue.ReadValue<float>();
        controllerValues.analogValue = inputs.VRInputs.AnalogValue.ReadValue<Vector2>();
    }

    private void OnTriggerPressed(InputAction.CallbackContext ctx)
    {

    }
    public void OnTriggerReleased(InputAction.CallbackContext ctx)
    {

    }
    public void OnGripPressed(InputAction.CallbackContext ctx)
    {

    }
    public void OnGripReleased(InputAction.CallbackContext ctx)
    {

    }
    private void OnPrimaryPressed(InputAction.CallbackContext ctx)
    {

    }
    private void OnPrimaryReleased(InputAction.CallbackContext ctx)
    {

    }
    private void OnSecondaryPressed(InputAction.CallbackContext ctx)
    {

    }
    private void OnSecondaryReleased(InputAction.CallbackContext ctx)
    {

    }
    public void OnAnalogPressed(InputAction.CallbackContext ctx)
    {

    }
    public void OnAnalogReleased(InputAction.CallbackContext ctx)
    {

    }
}

[System.Serializable]
public class ControllerValues
{
    public bool triggerPressed;
    public float triggerValue;
    public bool gripPressed;
    public float gripValue;
    public bool primaryButtonPressed;
    public bool secondaryButtonPressed;
    public bool analogPressed;
    public Vector2 analogValue;
}
