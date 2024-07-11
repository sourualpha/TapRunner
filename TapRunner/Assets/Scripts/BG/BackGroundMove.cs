using UnityEngine;
using UnityEngine.UI;

public class BackGroundMove : MonoBehaviour
{

    [SerializeField]
    private Vector2 m_offsetSpeed; // オフセットの移動速度

    private Material m_material; // イメージのマテリアル


    private void Start()
    {
        // Image コンポーネントを取得し、マテリアルを取得
        if (GetComponent<Image>() is Image i)
        {
            m_material = i.material;
        }
    }


    private void Update()
    {
        if (m_material)
        {
            // xとyの値が0 ～ 1でリピートするようにする
            var x = Mathf.Repeat(Time.time * m_offsetSpeed.x, Common.GrovalConst.K_MAXLENGTH);
            var y = Mathf.Repeat(Time.time * m_offsetSpeed.y, Common.GrovalConst.K_MAXLENGTH);
            var offset = new Vector2(x, y);
            m_material.SetTextureOffset(Common.GrovalConst.K_PROPNAME, offset);
        }
    }

    // オブジェクトが破棄されるときの処理
    private void OnDestroy()
    {
        // ゲームをやめた後にマテリアルのOffsetを戻しておく
        if (m_material)
        {
            m_material.SetTextureOffset(Common.GrovalConst.K_PROPNAME, Vector2.zero);
        }
    }
}
