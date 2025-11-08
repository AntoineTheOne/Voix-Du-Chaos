using UnityEngine;

public class MonsterHpLoss : MonoBehaviour
{
    [SerializeField] private MonsterInfos monsterInfos; // Info sur le monstre
    private int nbDePv;


    private void Start()
    {
        nbDePv = monsterInfos.nombreDeVie;
    }

    public void PrendreDegats(int amount)
        {
            nbDePv -= amount;
            if (nbDePv <= 0)
            {
                Destroy(transform.root.gameObject);
            }
        }
   
}
