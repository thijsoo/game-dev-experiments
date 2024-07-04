using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public float fadeTime = 2f;
    private float timeElapsed = 0f;
    public Vector3 moveSpeed = new Vector3(0, 75, 0);
    private TextMeshProUGUI textMeshPro;
    RectTransform textTransform;
    private Color startColor;

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;
    }

    private void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime;

        timeElapsed += Time.deltaTime;

        if (timeElapsed < fadeTime)
        {
            float newAlpha = startColor.a * (1 - timeElapsed / fadeTime);
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}