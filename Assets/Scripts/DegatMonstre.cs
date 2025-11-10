using UnityEngine;

public class DegatMonstre : MonoBehaviour
{
    [SerializeField] private SpellsInfos spellsInfos; // Info sur le spell

    private int nbDeDommage;

    void Start()
    {
        nbDeDommage = spellsInfos.nombreDeDegat;
    }
    private void OnCollisionEnter(Collision collision)
    {
      Destroy(gameObject, 5f);
      MonsterHpLoss vieEnnemi = collision.gameObject.GetComponent<MonsterHpLoss>();
      if (vieEnnemi != null)
      {
        vieEnnemi.PrendreDegats(nbDeDommage);
        Destroy(gameObject);
      }
      
    }
}
