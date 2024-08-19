using System.Collections;
using UnityEngine;
using TMPro;

public class Mensagem2 : MonoBehaviour
{
    public TMP_Text texto;
    [Range(0.1f, 10.0f)] public float distancia = 3;
    public GameObject Jogador;
    public float fadeDuration = 0.6f; // Tempo para aparecer/desaparecer
    private bool isFading = false; // Flag para evitar múltiplas corrotinas
    private bool messageDisabled = false; // Flag para desativar a mensagem permanentemente

    void Start()
    {
        Color color = texto.color;
        color.a = 0;
        texto.color = color;
        texto.enabled = false;
    }

    void Update()
    {
        if (messageDisabled) return;

        if (Vector3.Distance(transform.position, Jogador.transform.position) < distancia)
        {
            if (!isFading)
            {
                StartCoroutine(FadeTextToFullAlpha());
            }
        }
        else
        {
            if (!isFading)
            {
                StartCoroutine(FadeTextToZeroAlpha());
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            messageDisabled = true;
            StopAllCoroutines(); // Para quaisquer corrotinas em andamento
            texto.enabled = false; // Desativa o texto imediatamente
        }
    }

    private IEnumerator FadeTextToFullAlpha()
    {
        isFading = true;
        texto.enabled = true;
        Color color = texto.color;
        float startAlpha = color.a;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, 1f, elapsedTime / fadeDuration);
            texto.color = color;
            yield return null;
        }

        color.a = 1f;
        texto.color = color;
        isFading = false;
    }

    private IEnumerator FadeTextToZeroAlpha()
    {
        isFading = true;
        Color color = texto.color;
        float startAlpha = color.a;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);
            texto.color = color;
            yield return null;
        }

        color.a = 0f;
        texto.color = color;
        texto.enabled = false;
        isFading = false;
    }
}
