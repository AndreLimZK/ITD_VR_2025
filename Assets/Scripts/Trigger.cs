using UnityEngine;

public class Trigger: MonoBehaviour
{
    public int triggerNumber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TutorialManager.Instance.TriggerCompleted(triggerNumber);
        }
    }
}

