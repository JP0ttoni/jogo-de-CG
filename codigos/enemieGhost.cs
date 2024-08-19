using UnityEngine;
using UnityEngine.AI; // Necessário se estiver usando o NavMesh

public class InimigoScript : MonoBehaviour
{
    public Transform player; // Referência ao jogador
    public float chaseRange = 10f; // Distância máxima para o inimigo começar a perseguir
    public float attackRange = 2f; // Distância de ataque
    public float moveSpeed = 1.5f; // Velocidade de movimento
    public float attackDamage = 10f; // Dano causado ao jogador
    public float returnSpeed = 2f; // Velocidade de retorno à posição original

    private Vector3 originalPosition; // Posição inicial do inimigo
    private bool isChasing = false; // Se o inimigo está perseguindo o jogador

    private NavMeshAgent navMeshAgent; // Componente NavMeshAgent (se usar NavMesh)

    public int enemy_life = 1; 

    void Start()
    {
        originalPosition = transform.position; // Salva a posição inicial
        navMeshAgent = GetComponent<NavMeshAgent>(); // Obtém o componente NavMeshAgent
        if (navMeshAgent != null)
        {
            navMeshAgent.speed = moveSpeed; // Define a velocidade do NavMeshAgent
        }
    }

    void Update()
    {
        if(enemy_life <= 0)
        {
            Destroy(gameObject);
        }
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            isChasing = true;
            if (distanceToPlayer <= attackRange)
            {
            }
            else
            {
                // Persegue o jogador
                ChasePlayer();
            }
        }
        else
        {
            isChasing = false;
            ReturnToOriginalPosition();
        }
    }

    private void ChasePlayer()
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.SetDestination(player.position); // Move o inimigo em direção ao jogador
        }
        else
        {
            // Movimenta o inimigo manualmente se não estiver usando NavMesh
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            transform.LookAt(player.position); // Gira o inimigo para olhar para o jogador
        }
    }

    private void ReturnToOriginalPosition()
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.SetDestination(originalPosition); // Move o inimigo de volta à posição original
        }
        else
        {
            // Movimenta o inimigo manualmente se não estiver usando NavMesh
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, returnSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, originalPosition) < 0.1f)
            {
                transform.position = originalPosition; // Garante que o inimigo retorne exatamente à posição original
            }
        }
    }

}
