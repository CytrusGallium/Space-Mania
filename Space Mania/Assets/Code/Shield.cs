using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private static Shield singleton;
    private GameObject shield;
    private bool isShielded = false;
    
    void Awake()
    {
        if (singleton == null)
            singleton = this;
        else
            Debug.LogWarning("Shield singleton not NULL.");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        shield = transform.Find("Shield").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealthAndShield.CurrentShield <= 0)
        {
            isShielded = false;
            shield.gameObject.SetActive(false);
            return;
        }

        if (Input.GetKey(KeyCode.E))
        {
            isShielded = true;
            shield.gameObject.SetActive(true);
        }
        else
        {
            isShielded = false;
            shield.gameObject.SetActive(false);
        }
    }

    public static bool IsShielded()
    {
        return singleton.isShielded;
    }
}
