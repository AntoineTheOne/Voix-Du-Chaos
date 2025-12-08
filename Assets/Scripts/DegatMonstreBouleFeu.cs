using UnityEngine;

public class DegatMonstreBouleFeu : MonoBehaviour
{
    [SerializeField] private SpellsInfos spellsInfos; // Info sur le spell
    [SerializeField] private GameObject prefabExplosion; // VFX d'explosion

    [Header("AOE Settings")]
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private LayerMask monsterLayer;

    [Header("Burn VFX")]
    [SerializeField] private GameObject prefabBrulure;
    [SerializeField] private float burnVfxLifeTime = 5f; 
    private int nbDeDommage;
    private void Start()
    {
        if (spellsInfos != null)
        {
            nbDeDommage = spellsInfos.nombreDeDegat;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 1) Spawn VFX
        if (prefabExplosion != null)
        {
            Instantiate(prefabExplosion, transform.position, transform.rotation);
        }

        // 2) AOE Damage
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius, monsterLayer);

        for (int i = 0; i < hits.Length; i++)
        {
            MonsterHpLoss vieEnnemi = hits[i].GetComponentInParent<MonsterHpLoss>();
            if (vieEnnemi != null)
            {
                vieEnnemi.PrendreDegats(nbDeDommage);

                // 3) Spawn burn VFX sur le monstre touché
                if (prefabBrulure != null)
                {
                    GameObject vfx = Instantiate(prefabBrulure, vieEnnemi.transform.position + Vector3.up * 1f, vieEnnemi.transform.rotation);

                    // Si tu veux que l'effet suive le monstre :
                    vfx.transform.SetParent(vieEnnemi.transform);

                    // Optionnel: auto-destruction du VFX
                    Destroy(vfx, burnVfxLifeTime);
                }
            }
        }    
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}