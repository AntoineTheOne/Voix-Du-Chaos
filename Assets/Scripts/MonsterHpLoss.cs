using UnityEngine;
using UnityEngine.UI;
public class MonsterHpLoss : MonoBehaviour
{
    [SerializeField] private MonsterInfos monsterInfos; // Info sur le monstre
    [SerializeField] private Animator animator; // Animator du le monstre
    private int nbDePv;

    [SerializeField] private Slider barDeVieSlider;


    private void Start()
    {
        nbDePv = monsterInfos.nombreDeVie;

        if(barDeVieSlider != null)
        {
            barDeVieSlider.maxValue = nbDePv;
            barDeVieSlider.value = nbDePv;
        }
        
    }

    public void PrendreDegats(int amount)
        {
            nbDePv -= amount;

            if(barDeVieSlider != null)
            {
                barDeVieSlider.value = nbDePv;
            }
            
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
