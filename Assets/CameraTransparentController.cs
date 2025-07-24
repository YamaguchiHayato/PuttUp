using System.Collections.Generic;
using UnityEngine;

public class CameraTransparentController : MonoBehaviour
{
    public Transform ball; // ボールのTransformをInspectorで指定
    public LayerMask stageLayerMask; // ステージ用レイヤーをInspectorで指定
    public float transparentAlpha = 0.3f; // 透過時のアルファ値

    private Dictionary<Renderer, Color> originalColors = new Dictionary<Renderer, Color>();
    private List<Renderer> transparentRenderers = new List<Renderer>();

    void LateUpdate()
    {
        // まず前回透過したオブジェクトを元に戻す
        RestoreRenderers();

        if (ball == null) return;

        Vector3 cameraPos = transform.position;
        Vector3 ballPos = ball.position;
        Vector3 dir = ballPos - cameraPos;
        float distance = dir.magnitude;

        // カメラからボールまでの間にある全てのステージオブジェクトを取得
        RaycastHit[] hits = Physics.RaycastAll(cameraPos, dir.normalized, distance, stageLayerMask);
        foreach (var hit in hits)
        {
            Renderer rend = hit.collider.GetComponent<Renderer>();
            if (rend != null)
            {
                if (!originalColors.ContainsKey(rend))
                {
                    originalColors[rend] = rend.material.color;
                }
                Color c = rend.material.color;
                c.a = transparentAlpha;
                rend.material.color = c;

                // シェーダーがStandardの場合は透過モードに
                if (rend.material.shader.name == "Standard")
                {
                    rend.material.SetFloat("_Mode", 3); // Transparent
                    rend.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    rend.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    rend.material.SetInt("_ZWrite", 0);
                    rend.material.DisableKeyword("_ALPHATEST_ON");
                    rend.material.EnableKeyword("_ALPHABLEND_ON");
                    rend.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    rend.material.renderQueue = 3000;
                }
                transparentRenderers.Add(rend);
            }
        }
    }

    void RestoreRenderers()
    {
        foreach (var rend in transparentRenderers)
        {
            if (rend != null && originalColors.ContainsKey(rend))
            {
                rend.material.color = originalColors[rend];
                // 必要なら元のシェーダーモードに戻す処理も追加
            }
        }
        transparentRenderers.Clear();
        originalColors.Clear();
    }
} 