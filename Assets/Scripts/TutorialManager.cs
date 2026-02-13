using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    public int currentTriggerIndex = 1;
    public int currentTeleportIndex = 1;

    public GameObject[] teleportAreas; // Assign in Inspector
    public GameObject congratsUI;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Disable teleport areas at start
        foreach (GameObject tp in teleportAreas)
        {
            tp.SetActive(false);
        }

        congratsUI.SetActive(false);
    }

    public void TriggerCompleted(int triggerNumber)
    {
        if (triggerNumber == currentTriggerIndex)
        {
            Debug.Log("Trigger " + triggerNumber + " completed.");
            currentTriggerIndex++;

            if (currentTriggerIndex > 3)
            {
                EnableTeleports();
            }
        }
    }

    void EnableTeleports()
    {
        Debug.Log("All triggers cleared. Teleports enabled.");
        teleportAreas[0].SetActive(true);
    }

    public void TeleportCompleted(int teleportNumber)
    {
        if (teleportNumber == currentTeleportIndex)
        {
            currentTeleportIndex++;

            if (currentTeleportIndex == 2)
            {
                teleportAreas[1].SetActive(true);
            }
            else if (currentTeleportIndex > 2)
            {
                ShowCongrats();
            }
        }
    }

    void ShowCongrats()
    {
        congratsUI.SetActive(true);
    }
}
