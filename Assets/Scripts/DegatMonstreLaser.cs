using UnityEngine;

public class DegatMonstreLaser : MonoBehaviour
{
    [SerializeField] private SpellsInfos spellsInfos; // Info sur le spell

    private int nbDeDommage;

    void Start()
    {
        nbDeDommage = spellsInfos.nombreDeDegat;
    }
    private void OnTriggerEnter(Collider other)
    {
      Destroy(gameObject, 5f);
      MonsterHpLoss vieEnnemi = other.GetComponent<MonsterHpLoss>();
      if (vieEnnemi != null)
      {
        vieEnnemi.PrendreDegats(nbDeDommage);
        
      }
      
    }
}
