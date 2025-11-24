using UnityEngine;

public class DegatMonstre : MonoBehaviour
{
    [SerializeField] private SpellsInfos spellsInfos; // Info sur le spell
    [SerializeField] private GameObject prefabExplosion; // prefab une fois la collision du sort avec un monstre pour donner un résultat
    private int nbDeDommage;
    public LayerMask groundLayer;

    [SerializeField] private bool hitOnImpact = false;

    void Start()
    {
        nbDeDommage = spellsInfos.nombreDeDegat;
    }
    private void OnCollisionEnter(Collision collision)
    {
      
      if(prefabExplosion != null)
        {
            GameObject projectile = Instantiate(prefabExplosion, transform.position, transform.rotation);
            
        }
      MonsterHpLoss vieEnnemi = collision.gameObject.GetComponent<MonsterHpLoss>();
      if (vieEnnemi != null)
      {
        vieEnnemi.PrendreDegats(nbDeDommage);
        Destroy(gameObject);
      }

      if (collision.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {
          Destroy(gameObject);
        }

        if (hitOnImpact)
        {
            Destroy(gameObject);
        }
  
    
      
      
    }
}
