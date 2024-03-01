using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;


public class Delivery : MonoBehaviour
{
    [SerializeField]
    Color32 hasPackageColor = new Color32(1, 1, 1, 1);

    [SerializeField]
    Color32 noPackageColor = new Color32(1, 1, 1, 1);

    SpriteRenderer spriteRenderer;
    GameObject[] packages;

    GameObject[] customers;

    [SerializeField] float destroyDelay = 0.5f;
    bool hasPackage = false;

    int totalPackage;
    int remainPackage;

    [SerializeField]
    private TextMeshProUGUI status;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        packages = GameObject.FindGameObjectsWithTag("Package");
        customers = GameObject.FindGameObjectsWithTag("Customer");

        packages[1].SetActive(false);
        packages[2].SetActive(false);

        totalPackage = packages.Length;
        remainPackage = packages.Length;

        setStatusText(totalPackage, remainPackage, hasPackage);

    }

    void setStatusText(int totalPackage, int remainPackage, bool hasPackage)
    {
        string message = "Total Package: " + totalPackage + "<br>Remain Package: " + remainPackage;
        if (hasPackage)
        {
            message = message + "<br>Has Package in car";
        }
        else
        {
            message = message + "<br>No Package in car";
        }

        status.text = message;
    }



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
            remainPackage--;
            spriteRenderer.color = noPackageColor;
            Destroy(other.gameObject, destroyDelay);
            if (remainPackage == 0)
            {
                Debug.Log("Win game!");
                SceneManager.LoadSceneAsync(1);
            }
            packages[remainPackage].SetActive(true);
        }
        setStatusText(totalPackage, remainPackage, hasPackage);
    }


}
