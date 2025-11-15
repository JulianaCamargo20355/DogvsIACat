using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MovePlayers : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    Vector2 input;
    public int vidas = 10;

    // UI que mostra as vidas
    public TextMeshProUGUI vidaText;

    void Start()
    {
        // pega componentes do player
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        AtualizarUI();
    }

    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

        // Flip do sprite
        if (input.x > 0) sr.flipX = false;
        if (input.x < 0) sr.flipX = true;

        // Ativa animação 
        bool isIdle = input.sqrMagnitude == 0;
        anim.SetBool("isRun", !isIdle);
    }

    void FixedUpdate()
    {
        // Movimentação do jogador
        rb.velocity = input * speed;
    }

    // Chamado pelo gato 
    public void PerderVida()
    {
        vidas--;
        AtualizarUI();

        if (vidas <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }


    void AtualizarUI()
    {
        if (vidaText != null)
        {
            vidaText.text = "" + vidas;
        }
    }
}
