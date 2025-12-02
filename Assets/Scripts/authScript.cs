using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class authScript : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField usernameInputField;

    [SerializeField]
    private TMP_InputField passwordInputField;

    [SerializeField]
    private GameObject messagePanel;

    [SerializeField]
    private string correctPassword = "password123";

    [SerializeField]
    private string correctUsername = "andre";
    public bool errorUI = false;

    private bool isAuthenticated = false;

    void Start()
    {
        messagePanel.SetActive(false);
    }

    public void Authenticate()
    {
        if (passwordInputField.text == correctPassword)
        {
            isAuthenticated = true;
        }
        else
        {
            isAuthenticated = false;
        }
    }

    public void OnTeleportButtonPressed()
    {
        if (isAuthenticated)
        {
            Debug.Log("Loading SampleScene...");
            SceneManager.LoadScene(0);
        }
        else
        {
            errorUI = true;
            Debug.Log("Cannot teleport: Authentication required. Please enter the correct password first.");
            StartCoroutine(ShowErrorForSeconds(3f));
        }
    }

    private IEnumerator ShowErrorForSeconds(float duration)
    {
        messagePanel.SetActive(true);
        yield return new WaitForSeconds(duration);
        messagePanel.SetActive(false);
    }

}
