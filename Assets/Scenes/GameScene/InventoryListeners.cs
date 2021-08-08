using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryListeners : MonoBehaviour
{
    private Button closeButton;
    private void Start()
    {
        closeButton = transform.Find("Close").GetComponent<Button>();
        closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnCloseButtonClick()
    {
        gameObject.SetActive(false);
    }
}
