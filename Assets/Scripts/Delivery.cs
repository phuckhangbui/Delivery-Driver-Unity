using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

    int destroyPackageForRandomNumber;

    int nextSceneNumber;
    void Start()
    {
        int sceneOrder = SceneManager.GetActiveScene().buildIndex;
        nextSceneNumber = sceneOrder + 1;
        if (nextSceneNumber > 2)
        {
            nextSceneNumber = 0;
        }
        PlayerPrefs.SetInt("nextSceneNumber", nextSceneNumber);


        spriteRenderer = GetComponent<SpriteRenderer>();
        packages.AddRange(GameObject.FindGameObjectsWithTag("Package"));
        customers.AddRange(GameObject.FindGameObjectsWithTag("Customer"));

        destroyPackageForRandomNumber = (int)packages.Count / 2;
        deliveredPackages = 0;


        System.Random random = new System.Random();
        List<GameObject> packagesToDestroy = new List<GameObject>(packages); // Copy the original list

        for (int i = 0; i < destroyPackageForRandomNumber; i++)
        {
            Debug.Log("Loops start here");

            int index = random.Next(0, packagesToDestroy.Count); // Generate random index within the current size of the list
            GameObject packageToDestroy = packagesToDestroy[index];
            Destroy(packageToDestroy); // Destroy the GameObject
            packagesToDestroy.RemoveAt(index); // Remove the destroyed GameObject from the list
        }


        // for (int i = 0; i < notDestroyPackageList.Count; i++)
        // {
        //     if (notDestroyPackageList.Contains(i))
        //     {
        //         // not destroy when in the list
        //     }
        //     else
        //     {
        //         Destroy(packages[i], 0);
        //     }
        // }

        packages.Clear();
        packages.AddRange(packagesToDestroy);

        totalPackages = packages.Count;
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
            totalPackages--;
            spriteRenderer.color = noPackageColor;
            Destroy(other.gameObject, destroyDelay);
            if (totalPackages == 0)
            {
                Debug.Log("Win game!");
                SceneManager.LoadSceneAsync(nextSceneNumber);
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