using UnityEngine;
using UnityEngine.UI;

public class MonsterHpLoss : MonoBehaviour
{
    [SerializeField] private ChangementScene changementScene;
    [SerializeField] private MonsterInfos monsterInfos; // Info sur le monstre
    [SerializeField] private Animator animator; // Animator du le monstre
    [SerializeField] private MonsterManager monsterManager; // Monster manager

    [SerializeField] private Slider barDeVieSlider;
    [Header("Audio")]
    [SerializeField] public AudioSource deathaudio;
    private int nbDePv;


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
                            if (deathaudio != null)
                deathaudio.Play();
                if(animator != null)
                    {
                        animator.SetBool("IsDead", true);
                        Destroy(GetComponent<MonsterMovement>());
                        Destroy(GetComponent<UnityEngine.AI.NavMeshAgent>());
                    }
                monsterManager.nbMonstreApparu -= 1;
                if (gameObject.name == "francois")
                {
                    barDeVieSlider.gameObject.SetActive(false);
                    changementScene.victory = true;
                    changementScene.endgame = true;
                    GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

                    foreach (GameObject monster in monsters)
                    {
                        Animator anim = monster.GetComponent<Animator>();
                        if (anim != null)
                            anim.SetBool("IsDead", true);

                        MonsterMovement move = monster.GetComponent<MonsterMovement>();
                        if (move != null)
                            Destroy(move);

                        UnityEngine.AI.NavMeshAgent agent = monster.GetComponent<UnityEngine.AI.NavMeshAgent>();
                        if (agent != null)
                            Destroy(agent);

                        Destroy(monster, 5f);
                    }
                }
                Destroy(transform.root.gameObject, 5f);
            }

        }
   
}
