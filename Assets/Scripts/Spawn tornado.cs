using UnityEngine;
using UnityEngine.InputSystem;

public class Spawntornado : MonoBehaviour
{
    public GameObject tornadoPrefab;
    [SerializeField]
    private float spawnDistance;
    public float groundCheckDistance = 10f;
    public LayerMask groundLayer;

    public void CastTornado()
    {
        // Step 1: Find a position in front of the player
        Vector3 spawnPos = transform.position + transform.forward * spawnDistance;

        // Step 2: Raycast down from above that position to find ground height
        Vector3 rayStart = spawnPos + Vector3.up * groundCheckDistance;
        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, groundCheckDistance * 2f, groundLayer))
        {
            spawnPos = hit.point + Vector3.up * 0.1f; // small offset above ground
        }

        // Step 3: Spawn the tornado, facing same direction as player
        Instantiate(tornadoPrefab, spawnPos, Quaternion.LookRotation(transform.forward));
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OnAttack(){
        //Debug.Log("cliqué");
        CastTornado();
    }
}
