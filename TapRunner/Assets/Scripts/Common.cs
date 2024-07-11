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
        public const string TITLE_SCENE = "TitleScene"; // タイトルシーンの名前

        /// <summary>
        /// Ranking
        /// </summary>
        public const string BASE_URL = "https://script.google.com/macros/s/"; // ベースURL

        /// <summary>
        /// PlayerMovement
        /// </summary>
        public const float PLAYER_ROTATE_INITIAL = 45.0f; // プレイヤー回転角度
        public const float INVERSION = -1.0f; //反転するための数値

        /// <summary>
        /// BackGroundMove
        /// </summary>
        public const float K_MAXLENGTH = 1f; // オフセットの最大値
        public const string K_PROPNAME = "_MainTex"; // テクスチャのプロパティ名

        /// <summary>
        /// Spawner
        /// </summary>
        public const int DEFAULT_SPAWN_COUNT = 1; // デフォルトのスポーンカウント
        public const float DEFAULT_SPAWN_INTERVAL = 0.1f; // デフォルトのスポーン間隔
        public const float DEFAULT_DESTROY_WAIT_TIME = 3.0f; // デフォルトの破壊待機時間
        public static readonly Vector3 DEFAULT_MIN_SPAWN_POSITION = Vector3.zero; // デフォルトの最小スポーン位置
        public static readonly Vector3 DEFAULT_MAX_SPAWN_POSITION = Vector3.zero; // デフォルトの最大スポーン位置

        /// <summary>
        /// Destroyer
        /// </summary>
        public const float TIMER_THRESHOLD = 6.0f; // タイマーのしきい値
        public const float POSITION_THRESHOLD = -30.0f; // 位置のしきい値


    }
}
