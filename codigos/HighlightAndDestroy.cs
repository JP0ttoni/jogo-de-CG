using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class HighlightAndDestroyWithLight : MonoBehaviour
{
    private Light highlightLight;
    public TMP_Text messageText; // Referência ao componente de texto no Canvas para exibir a mensagem
    private bool isMouseLocked = false;
    private bool isMessageDisplayed = false;

    void Start()
    {
        // Adicionar um componente de luz ao objeto
        highlightLight = gameObject.AddComponent<Light>();
        highlightLight.color = Color.yellow; // Cor do highlight
        highlightLight.intensity = 5; // Intensidade da luz
        highlightLight.range = 2; // Alcance da luz
        highlightLight.enabled = false; // Inicialmente desativado

        if (messageText != null)
        {
            messageText.enabled = false; // Inicialmente desativado
        }
    }

    void OnMouseEnter()
    {
        ControladorDasChaves.chavesDoJogador.Add(4);
        highlightLight.enabled = true;
        StartCoroutine(LockMouseAfterDelay());
    }

    void OnMouseExit()
    {
        highlightLight.enabled = false;
        if (!isMouseLocked)
        {
            StopCoroutine(LockMouseAfterDelay());
        }
    }

    IEnumerator LockMouseAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        LockMouse();
        StartCoroutine(ShowMessageAfterRightClick());
    }

    void LockMouse()
    {
        // Trava o cursor no centro da tela
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isMouseLocked = true;
    }

    void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isMouseLocked = false;
    }

    IEnumerator ShowMessageAfterRightClick()
    {
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }

        UnlockMouse();

        yield return new WaitForSeconds(1.5f);

        if (messageText != null)
        {
            messageText.text = "Agora você pode atirar com o botão esquerdo do mouse";
            messageText.enabled = true;
        }

        isMessageDisplayed = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isMouseLocked)
        {
            Destroy(gameObject);
            UnlockMouse();
            if (messageText != null && isMessageDisplayed)
            {
                messageText.enabled = false;
            }
        }
    }
}
