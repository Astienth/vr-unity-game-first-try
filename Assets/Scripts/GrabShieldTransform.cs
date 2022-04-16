using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabShieldTransform : MonoBehaviour
{
    private Vector3 leftHandPosition = new Vector3(-0.26f, 0.24f, -0.18f);
    private Quaternion leftHandRotation = new Quaternion(0.52f, 0.48f, -0.64f, -0.32f);

    private Vector3 rightHandPosition = new Vector3(-0.18f, 0.26f, -0.14f);
    private Quaternion rightHandRotation = new Quaternion(0.45f, 0.74f, -0.28f, -0.42f);

    private bool updated = false;

    // Update is called once per frame
    void Update()
    {
        XRGrabInteractable interactable = GetComponentInParent<XRGrabInteractable>();
        if (!interactable.isSelected)
        {
            updated = false;
            return;
        }
        else
        {
            if (updated)
            {
                return;
            }
            else
            {
                if (interactable.selectingInteractor.gameObject.name == "LeftHand Controller")
                {
                    //transform.parent.SetPositionAndRotation(leftHandPosition,leftHandRotation);
                    transform.parent.SetPositionAndRotation(new Vector3(0,0,0), new Quaternion(0,0,0,0));
                }
                else
                {
                    //transform.parent.SetPositionAndRotation(rightHandPosition,rightHandRotation);
                    transform.parent.SetPositionAndRotation(new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
                }
                updated = true;
            }
        }
    }
}
