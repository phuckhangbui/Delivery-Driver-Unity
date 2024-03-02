using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class Delivery : MonoBehaviour
{
    [SerializeField] Color32 hasPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] Color32 noPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] float destroyDelay = 0.5f;
    [SerializeField] private TextMeshProUGUI status;

    SpriteRenderer spriteRenderer;
    List<GameObject> packages = new List<GameObject>();
    List<GameObject> customers = new List<GameObject>();

    int totalPackages;
    int deliveredPackages;
    bool hasPackage = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        packages.AddRange(GameObject.FindGameObjectsWithTag("Package"));
        customers.AddRange(GameObject.FindGameObjectsWithTag("Customer"));

        totalPackages = packages.Count;
        deliveredPackages = 0;

        SetStatusText();
        foreach (GameObject package in packages)
        {
            package.SetActive(false);
        }

        if (packages.Count > 0)
        {
            packages[0].SetActive(true);
        }
    }

    void SetStatusText()
    {
        string message = "Total Packages: " + totalPackages + "\nDelivered: " + deliveredPackages;
        message += hasPackage ? "\nHas Package in car" : "\nNo Package in car";
        status.text = message;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Package") && !hasPackage)
        {
            Debug.Log("Hit Package");
            hasPackage = true;
            spriteRenderer.color = hasPackageColor;
            Destroy(other.gameObject, destroyDelay);
            packages.Remove(other.gameObject);
            ActivateNextPackage();
        }
        else if (other.CompareTag("Customer") && hasPackage)
        {
            Debug.Log("Package Deliver");
            hasPackage = false;
            deliveredPackages++;
            spriteRenderer.color = noPackageColor;
            Destroy(other.gameObject, destroyDelay);
            if (deliveredPackages >= totalPackages)
            {
                Debug.Log("Win game!");
                SceneManager.LoadSceneAsync(1);
            }
        }
        SetStatusText();
    }

    void ActivateNextPackage()
    {
        foreach (GameObject package in packages)
        {
            if (!package.activeSelf)
            {
                package.SetActive(true);
                return;
            }
        }
    }
}
