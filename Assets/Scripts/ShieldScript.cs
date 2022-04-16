using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShieldScript : XRGrabInteractable
{

    protected override void OnSelectExiting(SelectExitEventArgs args)
    {
        base.OnSelectExiting(args);
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (rigidbody.useGravity)
        {
            return;
        }

        rigidbody.useGravity = true;
        Collider col = GetComponentInChildren<Collider>();
        col.isTrigger = false;
    }
}
