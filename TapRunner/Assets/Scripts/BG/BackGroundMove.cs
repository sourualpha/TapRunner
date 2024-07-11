using UnityEngine;
using UnityEngine.UI;

public class BackGroundMove : MonoBehaviour
{

    [SerializeField]
    private Vector2 m_offsetSpeed; // �I�t�Z�b�g�̈ړ����x

    private Material m_material; // �C���[�W�̃}�e���A��


    private void Start()
    {
        // Image �R���|�[�l���g���擾���A�}�e���A�����擾
        if (GetComponent<Image>() is Image i)
        {
            m_material = i.material;
        }
    }


    private void Update()
    {
        if (m_material)
        {
            // x��y�̒l��0 �` 1�Ń��s�[�g����悤�ɂ���
            var x = Mathf.Repeat(Time.time * m_offsetSpeed.x, Common.GrovalConst.K_MAXLENGTH);
            var y = Mathf.Repeat(Time.time * m_offsetSpeed.y, Common.GrovalConst.K_MAXLENGTH);
            var offset = new Vector2(x, y);
            m_material.SetTextureOffset(Common.GrovalConst.K_PROPNAME, offset);
        }
    }

    // �I�u�W�F�N�g���j�������Ƃ��̏���
    private void OnDestroy()
    {
        // �Q�[������߂���Ƀ}�e���A����Offset��߂��Ă���
        if (m_material)
        {
            m_material.SetTextureOffset(Common.GrovalConst.K_PROPNAME, Vector2.zero);
        }
    }
}
