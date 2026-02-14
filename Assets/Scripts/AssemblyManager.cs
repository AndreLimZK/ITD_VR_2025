using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class AssemblyManager : MonoBehaviour
{
    public XRSocketInteractor socketA, socketB, socketC;

    public XRGrabInteractable baseGrab;
    public Collider baseCollider;
    private bool isFinished = false;

    void Start()
    {
        baseGrab.enabled = false;
        baseCollider.enabled = false;
    }

    void OnEnable()
    {
        socketA.selectEntered.AddListener(CheckCombination);
        socketB.selectEntered.AddListener(CheckCombination);
        socketC.selectEntered.AddListener(CheckCombination);
    }

    void OnDisable()
    {
        socketA.selectEntered.RemoveListener(CheckCombination);
        socketB.selectEntered.RemoveListener(CheckCombination);
        socketC.selectEntered.RemoveListener(CheckCombination);
    }

    void CheckCombination(SelectEnterEventArgs args)
    {
        if (socketA.hasSelection && socketB.hasSelection && socketC.hasSelection && !isFinished)
        {
            isFinished = true; // Prevents the code from running twice
            StartCoroutine(WaitAndComplete());
        }
    }

    IEnumerator WaitAndComplete()
    {
        // Wait for a split second to let the last piece snap into the hole
        yield return new WaitForSeconds(0.15f);

        XRSocketInteractor[] allSockets = { socketA, socketB, socketC };

        foreach (var socket in allSockets)
        {
            var piece = socket.firstInteractableSelected as XRGrabInteractable;
            if (piece != null)
            {
                piece.transform.SetParent(this.transform);
                piece.enabled = false;
                if (piece.TryGetComponent(out Rigidbody rb)) rb.isKinematic = true;
                if (piece.TryGetComponent(out Collider col)) col.enabled = false;
            }
            // KILL THE SOCKET: This ensures it can't interfere with the grab
            socket.enabled = false;
            socket.gameObject.SetActive(false);
        }

        // Reactivate the base XR Grab Interactable and Collider
        baseGrab.enabled = true;
        baseCollider.enabled = true;

        // Reactivate the physics on the base 
        if (TryGetComponent(out Rigidbody baseRb)) 
        {
            baseRb.isKinematic = false;  
            baseRb.useGravity = true;   
        } 

        Debug.Log("Assembly Finished and Grabbable!");
        Pedestal pedestal = FindAnyObjectByType<Pedestal>();
        if (pedestal != null && pedestal.teleportArea != null)
        {
            pedestal.teleportArea.SetActive(true);
            Debug.Log("Pedestal teleport enabled.");
        }
    }
}
