using UnityEngine;
using UnityEngine.UI;

public class PlayerHitHpLoss : MonoBehaviour
{
[SerializeField] private InfoJoueur infoJoueur; // Info sur le joueur
[SerializeField] private string projectile = "Projectile";
[SerializeField] private Slider barDeVieSlider;

    [SerializeField] private Animator animator; // Animator du le monstre
    [SerializeField] private MonsterManager monsterManager; // Monster manager

[SerializeField] private ChangementScene changementScene;
  private int nbDePVJoueur;

    void Start()
    {
        nbDePVJoueur = infoJoueur.nombreDeVie;
        if(barDeVieSlider != null)
        {
            barDeVieSlider.maxValue = nbDePVJoueur;
            barDeVieSlider.value = nbDePVJoueur;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(projectile))
        {
            ProjectileScript projectile = other.GetComponent<ProjectileScript>(); //trouver le script ProjectileScript dans l'objet qui est renté en contact dans la zone du joueur
            if (projectile != null)
            {
                nbDePVJoueur -= projectile.infoDegatProjectile.nbDegatProjectile;
            }
            if(barDeVieSlider != null)
                {
                    barDeVieSlider.value = nbDePVJoueur;
                }
                
           
            Destroy(other.transform.root.gameObject);
            Debug.Log(nbDePVJoueur);
        
        }
        if (nbDePVJoueur <= 0)
        {
            nbDePVJoueur = 0;
            barDeVieSlider.value = 0;

             barDeVieSlider.gameObject.SetActive(false);
            
            changementScene.defeat = true;
            changementScene.endgame = true;

            DisableAllEnemies();

        }
    }
    private void DisableAllEnemies()
    {
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
}
