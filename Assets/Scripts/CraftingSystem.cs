using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingSystem : MonoBehaviour
{
    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI;

    public List<string> InventoryItemList = new List<string>();

    public Button toolsBTN;
    public Button craftKnifeBTN;

    public TextMeshProUGUI KnifeReq1; // Use TextMeshProUGUI for TextMeshPro Text
    public TextMeshProUGUI KnifeReq2; // Use TextMeshProUGUI for TextMeshPro Text

    public bool isOpen = false;

    public static CraftingSystem Instance { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(false);

        toolsBTN.onClick.AddListener(OpenToolsScreen);
        craftKnifeBTN.onClick.AddListener(CraftKnife);

        KnifeReq1 = toolsScreenUI.transform.Find("Knife").Find("Req1").GetComponent<TextMeshProUGUI>();
        KnifeReq2 = toolsScreenUI.transform.Find("Knife").Find("Req2").GetComponent<TextMeshProUGUI>();

        // Remove the following line as it's unnecessary
        // craftKnifeBTN = toolsScreenUI.transform.Find("Knife").Find("Button").GetComponent<Button>();
    }

    private void OpenToolsScreen()
    {
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(true);
    }

    private void CraftKnife()
    {
        // Handle knife crafting logic here
    }

    void CraftAnyItem()
    {
        // Add item into inventory
        // Remove resources from inventory
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
    }
}
