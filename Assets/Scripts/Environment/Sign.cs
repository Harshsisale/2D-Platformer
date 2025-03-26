using UnityEngine;
using TMPro;
using System.Collections;

public class Sign : MonoBehaviour
{
    [Header("Sign UI")]
    public GameObject messageUI;


    [Header("Interaction Prompt")]
    public GameObject interactionPromptObject;
    public TextMeshProUGUI interactionPromptText;
    [TextArea] public string promptMessage = "Press [E]";
    public float typingSpeed = 0.05f;

    private bool playerInRange = false;
    private bool hasShownPrompt = false; // ✅ track whether we've shown the prompt
    private Coroutine promptCoroutine;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            bool isActive = messageUI.activeSelf;
            messageUI.SetActive(!isActive);

            // ✅ Always hide the prompt when message opens
            interactionPromptObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (!hasShownPrompt)
            {
                interactionPromptObject.SetActive(true);

                if (promptCoroutine != null)
                    StopCoroutine(promptCoroutine);

                interactionPromptText.text = "";
                promptCoroutine = StartCoroutine(TypePrompt());

                hasShownPrompt = true; // ✅ mark prompt as shown
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            messageUI.SetActive(false);
            interactionPromptObject.SetActive(false);

            if (promptCoroutine != null)
                StopCoroutine(promptCoroutine);

            interactionPromptText.text = "";

            hasShownPrompt = false; // ✅ reset on exit so it shows next time
        }
    }

    private IEnumerator TypePrompt()
    {
        foreach (char letter in promptMessage)
        {
            interactionPromptText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
