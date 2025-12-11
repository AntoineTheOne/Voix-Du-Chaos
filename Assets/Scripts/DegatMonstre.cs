using UnityEngine;

public class DegatMonstre : MonoBehaviour
{
    [SerializeField] private SpellsInfos spellsInfos; // Info sur le spell
    [SerializeField] private GameObject prefabExplosion; // prefab une fois la collision du sort avec un monstre pour donner un résultat
    private int nbDeDommage;
    [SerializeField] private bool hitOnImpact = false;
    [SerializeField] private AudioSource audioexplosion;

    void Start()
    {

      if(spellsInfos != null)
        {
          nbDeDommage = spellsInfos.nombreDeDegat;
        }
        
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


        if (audioexplosion != null)
            {
                audioexplosion.Play();
            }
        

            if (!gameObject.CompareTag("Invincible")) // si le sort touché n'a le tag "invicible" fait ceci
            {
                Destroy(gameObject);
            }
        
      }

      if (hitOnImpact == true)
      {
        Destroy(gameObject);
      }
    }
}
