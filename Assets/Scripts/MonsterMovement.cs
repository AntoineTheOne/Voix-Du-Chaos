using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;

    [Header("Layers")]
    [SerializeField] private LayerMask playerLayerMask;

    [Header("Combat Settings")]
    [SerializeField] private float attackCooldown = 1f;
    private bool isOnAttackCooldown;
    [SerializeField] private float forwardShotForce = 20f;

    [Header("Detection Ranges")]
    [SerializeField] private float engagementRange = 10f;

    private bool isPlayerInRange;

    private void Awake()
    {
        if (playerTransform == null)
        {
            GameObject playerObject = GameObject.Find("Player");
            if (playerObject != null)
                playerTransform = playerObject.transform;
        }

        if (navAgent == null)
            navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        DetectPlayer();
        UpdateBehaviourState();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, engagementRange);
    }

    private void DetectPlayer()
    {
        isPlayerInRange = Physics.CheckSphere(transform.position, engagementRange, playerLayerMask);
    }

    private void FireProjectile()
    {
        if (projectilePrefab == null || firePoint == null || playerTransform == null) return;

        // Le firePoint regarde directement vers le joueur (légère correction de hauteur si besoin)
        Vector3 targetPos = playerTransform.position + Vector3.up * 1.0f; // ajuste 1.0f si nécessaire
        firePoint.LookAt(targetPos);

        // Instancie le projectile orienté dans la direction du firePoint
        Rigidbody projectileRb = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation)
            .GetComponent<Rigidbody>();

        // Lancer du projectile (aucune force verticale)
        projectileRb.AddForce(firePoint.forward * forwardShotForce, ForceMode.Impulse);

        Destroy(projectileRb.gameObject, 3f);
    }

    private IEnumerator AttackCooldownRoutine()
    {
        isOnAttackCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isOnAttackCooldown = false;
    }

    private void PerformChase()
    {
        if (playerTransform != null)
            navAgent.SetDestination(playerTransform.position);
    }

    private void PerformAttack()
    {
        navAgent.SetDestination(transform.position);

        // Le monstre regarde seulement horizontalement vers le joueur
        Vector3 lookPos = playerTransform.position;
        lookPos.y = transform.position.y;
        transform.LookAt(lookPos);

        if (!isOnAttackCooldown)
        {
            FireProjectile();
            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private void UpdateBehaviourState()
    {
        if (playerTransform == null) return;

        if (isPlayerInRange)
            PerformAttack();
        else
            PerformChase();
    }
}
