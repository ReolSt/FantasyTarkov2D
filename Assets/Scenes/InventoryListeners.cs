using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryListeners : MonoBehaviour
{
    private Button closeButton;
    void Start()
    {
        closeButton = transform.Find("Close").GetComponent<Button>();
        closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCloseButtonClick()
    {
        gameObject.SetActive(false);
    }
}
