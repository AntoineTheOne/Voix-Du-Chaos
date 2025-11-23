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
    [SerializeField] private float projectileSpeed = 25f; 

    [Header("Detection Ranges")]
    [SerializeField] private float engagementRange = 10f;


    [SerializeField] private Animator animator;
    

    private bool isPlayerInRange;

    private void Awake()
    {
        
        if (playerTransform == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
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

        // Le firePoint regarde vers le joueur (ajuste légèrement la hauteur)
        Vector3 targetPosistion = playerTransform.position + Vector3.up * 1.0f;
        firePoint.LookAt(targetPosistion);
        GameObject projectileInstance = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody projectileRb = projectileInstance.GetComponent<Rigidbody>();
        Vector3 direction = (targetPosistion - firePoint.position).normalized;

        //donne une vitesse constante au projectile
        projectileRb.linearVelocity = direction * projectileSpeed;

        // détruit le projectile après 3 secondes
        Destroy(projectileInstance, 3f);
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
        {
            navAgent.SetDestination(playerTransform.position);
        }
            
        animator.SetBool("IsInRange", false);
    }

    private void PerformAttack()
    {
        navAgent.SetDestination(transform.position);
        Vector3 lookPosition = playerTransform.position;
        lookPosition.y = transform.position.y;
        transform.LookAt(lookPosition);
        animator.SetBool("IsInRange", true);

        if (!isOnAttackCooldown)
        {
            FireProjectile();
            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private void UpdateBehaviourState()
    {

        if (isPlayerInRange)
            PerformAttack();
        else
            PerformChase();
    }
}
