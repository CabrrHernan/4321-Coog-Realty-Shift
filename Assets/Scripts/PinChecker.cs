using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PullPin : MonoBehaviour
{
    private FixedJoint pinJoint;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    void Start()
    {
        pinJoint = GetComponent<FixedJoint>();
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (pinJoint != null)
        {
            Destroy(pinJoint); // Detach pin from hydrant
        }
    }
}
