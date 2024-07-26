using UnityEngine;

public class EdgeGrower : MonoBehaviour
{
    public float animationSpeed = 0.05f;
    private SpriteRenderer spriteRenderer;
    private float currentTime;
    private string shaderProperty = "_Edge";

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentTime = 0f;
        spriteRenderer.material.SetFloat(shaderProperty, 0.07f);
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        float edgeValue = Mathf.Clamp01(0.05f + currentTime * animationSpeed);
        spriteRenderer.material.SetFloat(shaderProperty, edgeValue);
    }
}
