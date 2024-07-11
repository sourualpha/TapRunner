using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public static class GrovalConst
    {
        /// <summary>
        /// GameManager
        /// </summary>
        public const string TITLE_SCENE = "TitleScene"; // �^�C�g���V�[���̖��O

        /// <summary>
        /// Ranking
        /// </summary>
        public const string BASE_URL = "https://script.google.com/macros/s/"; // �x�[�XURL

        /// <summary>
        /// PlayerMovement
        /// </summary>
        public const float PLAYER_ROTATE_INITIAL = 45.0f; // �v���C���[��]�p�x
        public const float INVERSION = -1.0f; //���]���邽�߂̐��l

        /// <summary>
        /// BackGroundMove
        /// </summary>
        public const float K_MAXLENGTH = 1f; // �I�t�Z�b�g�̍ő�l
        public const string K_PROPNAME = "_MainTex"; // �e�N�X�`���̃v���p�e�B��

        /// <summary>
        /// Spawner
        /// </summary>
        public const int DEFAULT_SPAWN_COUNT = 1; // �f�t�H���g�̃X�|�[���J�E���g
        public const float DEFAULT_SPAWN_INTERVAL = 0.1f; // �f�t�H���g�̃X�|�[���Ԋu
        public const float DEFAULT_DESTROY_WAIT_TIME = 3.0f; // �f�t�H���g�̔j��ҋ@����
        public static readonly Vector3 DEFAULT_MIN_SPAWN_POSITION = Vector3.zero; // �f�t�H���g�̍ŏ��X�|�[���ʒu
        public static readonly Vector3 DEFAULT_MAX_SPAWN_POSITION = Vector3.zero; // �f�t�H���g�̍ő�X�|�[���ʒu

        /// <summary>
        /// Destroyer
        /// </summary>
        public const float TIMER_THRESHOLD = 6.0f; // �^�C�}�[�̂������l
        public const float POSITION_THRESHOLD = -30.0f; // �ʒu�̂������l


    }
}
