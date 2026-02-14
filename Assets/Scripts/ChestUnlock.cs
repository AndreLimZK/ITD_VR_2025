using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ChestUnlock : MonoBehaviour
{
    public GameObject lid;

    XRSocketInteractor socket;
    XRGrabInteractable grab;

    void Start()
    {
        grab = lid.GetComponent<XRGrabInteractable>();
        grab.enabled = false;

        socket = GetComponentInChildren<XRSocketInteractor>();
        socket.selectEntered.AddListener(OnKeyInserted);
    }

    void OnKeyInserted(SelectEnterEventArgs args)
    {  
        grab.enabled = true;
    }
}

