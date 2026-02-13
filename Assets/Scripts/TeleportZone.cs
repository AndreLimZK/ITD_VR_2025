using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class TeleportZone : MonoBehaviour
{
    public int teleportNumber;

    TeleportationArea teleportArea;

    void Start()
    {
        teleportArea = GetComponent<TeleportationArea>();

        teleportArea.selectEntered.AddListener(OnTeleportUsed);
    }

    void OnTeleportUsed(SelectEnterEventArgs args)
    {
        Debug.Log("Teleport " + teleportNumber + " used. Waiting for physical teleport to complete...");

        StartCoroutine(WaitForTeleportComplete());
    }

    IEnumerator WaitForTeleportComplete()
    {
        // Get main camera transform
        Transform cam = Camera.main != null ? Camera.main.transform : null;
        if (cam == null)
        {
            Debug.LogWarning("Main Camera not found; calling TeleportCompleted immediately.");
            TutorialManager.Instance.TeleportCompleted(teleportNumber);
            yield break;
        }

        // Wait for the physical player to reach the teleport location
        Vector3 targetPos = transform.position;
        float timeout = 5f;
        float elapsed = 0f;
        float threshold = 1.0f; 

        // Check every frame until timeout
        while (elapsed < timeout)
        {
            Vector2 camPosXZ = new Vector2(cam.position.x, cam.position.z);
            Vector2 targetXZ = new Vector2(targetPos.x, targetPos.z);
            if (Vector2.Distance(camPosXZ, targetXZ) <= threshold)
            {
                Debug.Log("Physical teleport detected for zone " + teleportNumber);
                TutorialManager.Instance.TeleportCompleted(teleportNumber);
                yield break;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Timed out waiting for physical teleport for zone " + teleportNumber + ". Calling TeleportCompleted anyway.");
        TutorialManager.Instance.TeleportCompleted(teleportNumber);
    }
}

