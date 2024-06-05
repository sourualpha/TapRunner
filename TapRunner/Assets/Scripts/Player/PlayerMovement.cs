using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    #region SerializeField
    [SerializeField]
    GameObject PlayerTurbo; //後ろのアニメーション

    [SerializeField]
    GameObject SavePanel; //保存のパネル
    #endregion
    public float speed;
    public float gravity;
    float playerRotate;
    Rigidbody2D rb;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerRotate = 45;
        SavePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 force = new Vector2(speed * Time.deltaTime, 0);
        this.rb.AddForce(force);
        this.transform.position += new Vector3(0, gravity * Time.deltaTime, 0);
        this.transform.rotation = Quaternion.Euler(0, 0, playerRotate);

    }

    public void Tap()
    {
        gravity *= -1.0f;
        playerRotate*= -1.0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            Destroy(PlayerTurbo);
            animator.Play("Explosions");
            SavePanel.SetActive(true);
        }
    }

}
