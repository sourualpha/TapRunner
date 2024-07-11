using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class PlayerMovement : MonoBehaviour
{
    #region SerializeField
    [SerializeField]
    GameObject PlayerTurbo; // �v���C���[�̌��ɕ\������A�j���[�V����

    [SerializeField]
    GameObject SavePanel; // �ۑ��̃p�l��
    #endregion

    public float speed; // �v���C���[�̑��x
    public float gravity; // �d��
    float playerRotate; // �v���C���[�̉�]�p�x
    Rigidbody2D rb; // Rigidbody2D �R���|�[�l���g
    Animator animator; // �A�j���[�^�[

    // ����������
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerRotate = Common.GrovalConst.PLAYER_ROTATE_INITIAL;
        SavePanel.SetActive(false); // �ۑ��p�l�����\��
    }

    void Update()
    {
        Vector2 force = new Vector2(speed * Time.deltaTime, 0);
        rb.AddForce(force);

        // �v���C���[�̈ʒu���X�V����
        transform.position += new Vector3(0, gravity * Time.deltaTime, 0);

        // �v���C���[�̉�]���X�V����
        transform.rotation = Quaternion.Euler(0, 0, playerRotate);
    }

    // �v���C���[���^�b�v���ꂽ�Ƃ��̏���
    public void Tap()
    {
        gravity *= Common.GrovalConst.INVERSION; // �d�͂̕����𔽓]����
        playerRotate *= Common.GrovalConst.INVERSION; // ��]�p�x�𔽓]����
    }

    // �Փˎ��̏���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // �v���C���[�̃^�[�{��j�󂷂�
            Destroy(PlayerTurbo);

            // �����A�j���[�V�������Đ�����
            animator.Play("Explosions");

            // �ۑ��p�l����\������
            SavePanel.SetActive(true);
        }
    }
}
