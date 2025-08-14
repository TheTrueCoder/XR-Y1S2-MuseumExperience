using UnityEngine;

public class VRRig : MonoBehaviour
{
    [SerializeField] Color outlineColor;
    public static VRRig Instance { get; private set; }
    [field: SerializeField] public Transform head { get; private set; }
    [SerializeField] VRController[] controllers;

    public enum ControllerHand
    {
        Left, Right
    };

    public enum ControllerButton
    {
        Trigger, Grip, Thumbstick, Primary, Secondary
    }

    private void Start()
    {
        Instance = this;
        controllers[0].Initialise(this);
        controllers[1].Initialise(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = outlineColor;
        Vector3 rightForward = transform.position + transform.right + transform.forward;
        Vector3 rightBack = transform.position + transform.right - transform.forward;
        Vector3 leftForward = transform.position - transform.right + transform.forward;
        Vector3 leftBack = transform.position - transform.right - transform.forward;
        Gizmos.DrawLine(rightForward, rightBack);
        Gizmos.DrawLine(leftForward, leftBack);
        Gizmos.DrawLine(rightForward, leftForward);
        Gizmos.DrawLine(rightBack, leftBack);
    }

    public VRController GetController(ControllerHand hand)
    {
        if(hand == ControllerHand.Left)
        {
            return controllers[0];
        }
        else
        {
            return controllers[1];
        }
    }
}
