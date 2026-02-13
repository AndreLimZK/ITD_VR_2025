using UnityEngine;

public class CloseUI : MonoBehaviour
{
    public GameObject panel;

    public void Close()
    {
        panel.SetActive(false);
    }
}

