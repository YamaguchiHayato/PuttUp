

    
using UnityEngine;

public class ShowTextOnCollision : MonoBehaviour
{
    public float transparentAlpha = 0.3f; // ���ߎ��̓����x
    private Material material;
    private Color originalColor;

    void Start()
    {
        // ���̃I�u�W�F�N�g�̃}�e���A�����擾�i�C���X�^���X�������j
        material = GetComponent<Renderer>().material;

        // ���̐F��ۑ��i��Ŗ߂����߁j
        originalColor = material.color;

        // �}�e���A����Shader��Transparent�Ή��ɂȂ��Ă��邩�m�F
        if (material.shader.name == "Standard")
        {
            // Rendering Mode��Transparent�ɐݒ�
            material.SetFloat("_Mode", 3); // 3 = Transparent
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 1); // �[�x�o�b�t�@�ɏ������ށi0���Ə����邱�Ƃ�����j
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
            newColor.a = transparentAlpha; // ���ߓx��ݒ�
            material.color = newColor;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            material.color = originalColor; // ���̐F�ɖ߂�
        }
    }
}
