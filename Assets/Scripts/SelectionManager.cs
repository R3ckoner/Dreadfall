using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; set; }

    public bool onTarget;

    public GameObject selectedObject;
    public GameObject interaction_Info_UI;
    TextMeshProUGUI interaction_text;

    private void Start()
    {
        onTarget = false;
        interaction_text = interaction_Info_UI.GetComponent<TextMeshProUGUI>();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;

            InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();

            if (interactable && interactable.playerInRange)
            {
                onTarget = true;
                selectedObject = interactable.gameObject;

                interaction_text.text = interactable.GetItemName();
                interaction_Info_UI.SetActive(true);
            }
            else
            {
                onTarget = false;
                interaction_Info_UI.SetActive(false);
            }
        } else {
            onTarget = false;
            interaction_Info_UI.SetActive(false);
        }
    } 
}
