using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabSwordTransform : MonoBehaviour
{
    private Vector3 rightHandPosition = new Vector3(0.01f, -0.02f, 0);
    private Quaternion rightHandRotation = new Quaternion(0.49f, 0.06f, 0.83f, 0.26f);

    private Vector3 leftHandPosition = new Vector3(0.01f, -0.02f, 0);
    private Quaternion leftHandRotation = new Quaternion(0.49f, 0.06f, 0.83f, 0.26f);

    private bool updated = false;
    private bool isTrigger = true;

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
                //once only
                if (isTrigger)
                {
                    interactable.colliders[0].isTrigger = false;
                    isTrigger = false;
                }

                if (interactable.selectingInteractor.gameObject.name == "LeftHand Controller")
                {
                    //transform.parent.SetPositionAndRotation(leftHandPosition,leftHandRotation);
                }
                else
                {
                    //transform.parent.SetPositionAndRotation(rightHandPosition, rightHandRotation);
                }
                updated = true;
            }
        }
    }
}
