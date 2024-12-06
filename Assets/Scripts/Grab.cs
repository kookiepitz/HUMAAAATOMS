using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TwoHandGrab : XRGrabInteractable
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        // If two controllers grab, adjust behavior here
        Debug.Log($"Grabbed by {args.interactorObject.transform.name}");
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        Debug.Log($"Released by {args.interactorObject.transform.name}");
    }
}
