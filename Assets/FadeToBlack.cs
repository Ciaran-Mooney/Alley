using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using KinematicCharacterController;

public class FadeToBlack : MonoBehaviour
{
    // Reference to the Image component that covers the entire canvas
    public Image blackImage;

    // Duration of the fade in seconds
    public float fadeDuration = 2.0f;

    public static FadeToBlack instance;

    public void Awake()
    {
        instance = this;
    }

    public void Fade()
    {
        // Ensure the image starts fully transparent
        Color color = blackImage.color;
        color.a = 0f;
        blackImage.color = color;

        // Start the fade sequence
        StartCoroutine(FadeInOut());
    }

    IEnumerator FadeInOut()
    {
        // Fade to black
        yield return StartCoroutine(Fade(0f, 1f));

        // Optional: Wait for a moment while the screen is black
        yield return new WaitForSeconds(1.0f);

        GameObject.FindGameObjectWithTag("Player").GetComponent<KinematicCharacterMotor>().SetPositionAndRotation(new Vector3(436.2895f, 0.06860161f, 889.0636f), Quaternion.Euler(new Vector3(0, -100, 0)));

        yield return new WaitForSeconds(1.0f);

        // Fade out of black
        yield return StartCoroutine(Fade(1f, 0f));
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color color = blackImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            // Calculate the new alpha value
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            color.a = alpha;
            blackImage.color = color;
            yield return null;
        }

        // Ensure the final alpha is set
        color.a = endAlpha;
        blackImage.color = color;
    }
}