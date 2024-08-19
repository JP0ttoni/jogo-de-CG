using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    public Image flashImage; // A Imagem do Canvas que será usada para o efeito de piscar
    public float flashDuration = 0.1f; // Duração do piscar

    private void Start()
    {
        flashImage.enabled = false;
        if (flashImage != null)
        {
            flashImage.color = new Color(1, 0, 0, 0); // Inicialmente transparente
        }
    }

    public void FlashScreen()
    {
        if (flashImage != null)
        {
            flashImage.enabled = true;
            StartCoroutine(FlashCoroutine());
        }
    }

    private IEnumerator FlashCoroutine()
    {
        flashImage.color = new Color(1, 0, 0, 0.25f); // Vermelho totalmente opaco
        yield return new WaitForSeconds(flashDuration);
        flashImage.color = new Color(1, 0, 0, 0); // Transparente novamente
        flashImage.enabled = false;
    }
}