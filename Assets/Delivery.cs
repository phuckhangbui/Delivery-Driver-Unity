using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Delivery : MonoBehaviour
{
    [SerializeField]
    Color32 hasPackageColor = new Color32(1, 1, 1, 1);

    [SerializeField]
    Color32 noPackageColor = new Color32(1, 1, 1, 1);

    int packageNotDeliver;

    SpriteRenderer spriteRenderer;
    GameObject[] packages;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        packageNotDeliver = GameObject.FindGameObjectsWithTag("Package").Length;
        packages = GameObject.FindGameObjectsWithTag("Package");
        packages[1].SetActive(false);
        packages[2].SetActive(false);
    }

    [SerializeField] float destroyDelay = 0.5f;
    bool hasPackage = false;

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Ouch!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Package" && !hasPackage)
        {
            Debug.Log("Hit Package");
            hasPackage = true;
            spriteRenderer.color = hasPackageColor;
            Destroy(other.gameObject, destroyDelay);
        };

        if (other.tag == "Customer" && hasPackage)
        {
            Debug.Log("Package Deliver");
            hasPackage = false;
            packageNotDeliver--;
            spriteRenderer.color = noPackageColor;
            if (packageNotDeliver == 0)
            {
                Debug.Log("Win game!");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            packages[packageNotDeliver].SetActive(true);
        }
    }


}
