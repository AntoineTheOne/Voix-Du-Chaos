using UnityEngine;
using UnityEngine.UI;

public class PlayerHitHpLoss : MonoBehaviour
{
[SerializeField] private InfoJoueur infoJoueur; // Info sur le joueur
[SerializeField] private string projectile = "Projectile";
[SerializeField] private Slider barDeVieSlider;
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
    }
}
