using UnityEngine;

public class MonsterHpLoss : MonoBehaviour
{
    [SerializeField] private MonsterInfos monsterInfos; // Info sur le monstre
    [SerializeField] private Animator animator; // Animator du le monstre
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
                if(animator != null)
                    {
                        animator.SetBool("IsDead", true);
                        Destroy(GetComponent<MonsterMovement>());
                        Destroy(GetComponent<UnityEngine.AI.NavMeshAgent>());
                    }
                Destroy(transform.root.gameObject, 5f);
            }
        }
   
}
