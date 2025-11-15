using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAi : MonoBehaviour
{
    public float speed = 2.5f;          // velocidade base do gato
    public float escapeBoost = 2f;      // força extra para fugir
    public float dangerDistance = 3f;   // distância em que ele começa a fugir

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private NeuralNet brain;        // rede neural que decide a direção
    private GameObject player;      // referência ao jogador

    Vector2 direction;              // direção final do movimento
    float learnTimer = 0f;          // controla tempo entre treinamentos

    private float flipCooldown = 0.2f;  // tempo mínimo entre flips
    private float flipTimer = 0f;

    void Start()
    {
        // pega componentes do gato
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        // encontra o player pela tag
        player = GameObject.FindGameObjectWithTag("Player");

        // cria rede neural: 4 entradas, 6 neurônios escondidos, 2 saídas
        brain = new NeuralNet(4, 6, 2);

        // direção inicial aleatória
        direction = Random.insideUnitCircle.normalized;
    }

    void Update()
    {
        if (player == null) return;

        // diferença de posição entre gato e jogador
        Vector2 diff = player.transform.position - transform.position;

        // evita divisão por zero
        if (diff.sqrMagnitude < 0.01f)
            diff = new Vector2(0.1f, 0.1f);

        // entradas para a rede neural
        float[] input = new float[]
        {
            diff.x,
            diff.y,
            rb.velocity.x,
            rb.velocity.y
        };

        // rede neural decide uma direção
        float[] output = brain.FeedForward(input);
        Vector2 netDir = new Vector2(output[0], output[1]);

        // evita ficar parado sem direção
        if (netDir.sqrMagnitude < 0.05f)
            netDir = Random.insideUnitCircle;

        // lógica de fugir se o player estiver perto
        float distance = diff.magnitude;
        Vector2 escape = Vector2.zero;

        if (distance < dangerDistance)
        {
            // aumenta força de fuga quanto mais perto o player estiver
            float multiplier = Mathf.Lerp(escapeBoost, escapeBoost * 2f, 1f - (distance / dangerDistance));
            escape = -diff.normalized * multiplier;
        }

        // direção final combinando rede neural + fuga
        direction = (netDir + escape).normalized;

        // controla flip do sprite (vira o gato)
        flipTimer -= Time.deltaTime;
        float threshold = 0.3f;

        if (flipTimer <= 0f)
        {
            if (direction.x > threshold && sr.flipX)
            {
                sr.flipX = false;
                flipTimer = flipCooldown;
            }
            else if (direction.x < -threshold && !sr.flipX)
            {
                sr.flipX = true;
                flipTimer = flipCooldown;
            }
        }

        // animação do gato
        bool isIdle = direction.sqrMagnitude < 0.1f;
        anim.SetBool("isRun", !isIdle);

        // aprendizado automático
        learnTimer += Time.deltaTime;
        if (learnTimer > 2f)
        {
            learnTimer = 0f;

            // objetivo: fugir do player
            float[] target =
            {
                -diff.normalized.x,
                -diff.normalized.y
            };

            brain.Train(input, target);
        }
    }

    void FixedUpdate()
    {
        // aplica movimento no rigidbody
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // se encostar no player, ele perde vida
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<MovePlayers>().PerderVida();
        }
    }
}
