

    
using UnityEngine;

public class ShowTextOnCollision : MonoBehaviour
{
    public float transparentAlpha = 0.3f; // 透過時の透明度
    private Material material;
    private Color originalColor;

    void Start()
    {
        // このオブジェクトのマテリアルを取得（インスタンス化される）
        material = GetComponent<Renderer>().material;

        // 元の色を保存（後で戻すため）
        originalColor = material.color;

        // マテリアルのShaderがTransparent対応になっているか確認
        if (material.shader.name == "Standard")
        {
            // Rendering ModeをTransparentに設定
            material.SetFloat("_Mode", 3); // 3 = Transparent
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 1); // 深度バッファに書き込む（0だと消えることがある）
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.renderQueue = 3000;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Color newColor = originalColor;
            newColor.a = transparentAlpha; // 透過度を設定
            material.color = newColor;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            material.color = originalColor; // 元の色に戻す
        }
    }
}
