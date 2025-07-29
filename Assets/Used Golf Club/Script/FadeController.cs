using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public float duration = 1.0f; // 出現にかかる時間
    private Renderer rend;
    private Vector3 initialScale;

    void Start()
    {
        rend = GetComponent<Renderer>();
        initialScale = transform.localScale;

        // 最初は透明（または見えない状態）
        SetAlpha(0f);
        transform.localScale = Vector3.zero;

        // 自然に出現させるコルーチンを開始
        StartCoroutine(Appear());
    }

    private System.Collections.IEnumerator Appear()
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;

            // 透明度を増やす
            SetAlpha(t);

            // スケールを大きく
            transform.localScale = Vector3.Lerp(Vector3.zero, initialScale, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // 最終的に完全に表示
        SetAlpha(1f);
        transform.localScale = initialScale;
    }

    private void SetAlpha(float alpha)
    {
        if (rend != null && rend.material.HasProperty("_Color"))
        {
            Color color = rend.material.color;
            color.a = alpha;
            rend.material.color = color;
        }
    }
}