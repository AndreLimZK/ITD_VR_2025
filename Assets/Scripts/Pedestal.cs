using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Pedestal : MonoBehaviour
{
    public XRSocketInteractor pedestalSocket;
    public GameObject victoryCanvas; 
    public GameObject teleportArea;

    private void Start()
    {
        if (victoryCanvas != null)
        {
            victoryCanvas.SetActive(false); 
        }

        if (teleportArea != null)
        {
            teleportArea.SetActive(false); 
        }
    }

    void OnEnable()
    {
        // Check if the socket interactor is assigned
        pedestalSocket.selectEntered.AddListener(OnObjectPlaced);
    }

    void OnDisable()
    {
        pedestalSocket.selectEntered.RemoveListener(OnObjectPlaced);
    }

    void OnObjectPlaced(SelectEnterEventArgs args)
    {
        Debug.Log("Object placed in pedestal socket.");
        TriggerWin();
    }

    void TriggerWin()
    {
        if (victoryCanvas != null)
        {
            victoryCanvas.SetActive(true);
            Debug.Log("Activity Successfully Completed!");
        }
    }
}
