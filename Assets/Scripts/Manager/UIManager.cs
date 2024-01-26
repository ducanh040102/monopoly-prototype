using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    [Header("Current Active Panel")]
    [SerializeField] private string currentActivePanelName = "none";

    [Header("Raycast Bloacker")]
    [SerializeField] private GameObject rayCastBlocker;

    [Space(10)]
    [Header("Panels")]
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject buyPanel;
    [SerializeField] private GameObject payPanel;
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private GameObject travelPanel;
    [SerializeField] private GameObject railroadPanel;
    [SerializeField] private GameObject railroadUsePanel;
    [SerializeField] private GameObject ultilityBuyPanel;
    [SerializeField] private GameObject chancePanel;
    [SerializeField] private GameObject incomeTaxPanel;
    [SerializeField] private GameObject inJailPanel;
    [SerializeField] private GameObject superChancePanel;

    [Space(10)]
    [Header("Buttons")]
    [SerializeField] private Button sellButton;
    [SerializeField] private Button endTurnButton;
    [SerializeField] private Button rollDiceButton;
    [SerializeField] private Button pauseButton;

    [Space(10)]
    [SerializeField] private List<GameObject> playerInfoPanels;

    public static UIManager Instance { get => instance; }


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        RefreshPlayerPanel();
        SetEndTurnButtonStatus(false);
        SetSellButtonStatus(false);

        AddButtonListener();
    }

    

    protected void SetRollDiceButtonStatus(bool status)
    {
        rollDiceButton.interactable = status;
    }

    protected void SetEndTurnButtonStatus(bool status)
    {
        endTurnButton.interactable = status;
    }

    protected void SetSellButtonStatus(bool status)
    {
        sellButton.interactable = status;
    }

    protected virtual void AddButtonListener()
    {
        sellButton.onClick.AddListener(SellButtonFunction);
        endTurnButton.onClick.AddListener(EndTurnButtonFunction);
        rollDiceButton.onClick.AddListener(RollDiceButtonFunction);
        pauseButton.onClick.AddListener(PauseButtonFunction);
    }

    public virtual void ToggleInfoPanel(bool value)
    {
        infoPanel.SetActive(value);
    }

    public void TogglePanelOn(string name)
    {
        switch (name)
        {
            case "info":
                infoPanel.SetActive(true);
                break;
            case "buy":
                buyPanel.SetActive(true);
                break;
            case "pay":
                payPanel.SetActive(true);
                break;
            case "upgrade":
                upgradePanel.SetActive(true);
                break;
            case "travel":
                travelPanel.SetActive(true);
                break;
            case "railroad":
                railroadPanel.SetActive(true);
                break;
            case "railroaduse":
                railroadUsePanel.SetActive(true);
                break;
            case "ultilitybuy":
                ultilityBuyPanel.SetActive(true);
                break;
            case "chance":
                chancePanel.SetActive(true);
                break;
            case "incometax":
                incomeTaxPanel.SetActive(true);
                break;
            case "injail":
                inJailPanel.SetActive(true);
                break;
            case "superchance":
                superChancePanel.SetActive(true);
                break;

            default:
                break;
        }

        CameraManager.Instance.CameraFocusOut();
        
        currentActivePanelName = name;

        OnAction();
        RefreshPlayerPanel();
    }
    
    public void TogglePanelOff(string name)
    {
        switch (name)
        {
            case "info":
                infoPanel.SetActive(false);
                break;
            case "buy":
                buyPanel.SetActive(false);
                break;
            case "pay":
                payPanel.SetActive(false);
                break;
            case "upgrade":
                upgradePanel.SetActive(false);
                break;
            case "travel":
                travelPanel.SetActive(false);
                break;
            case "railroad":
                railroadPanel.SetActive(false);
                break;
            case "railroaduse":
                railroadUsePanel.SetActive(false);
                break;
            case "ultilitybuy":
                ultilityBuyPanel.SetActive(false);
                break;
            case "chance":
                chancePanel.SetActive(false);
                break;
            case "incometax":
                incomeTaxPanel.SetActive(false);
                break;
            case "injail":
                inJailPanel.SetActive(false);
                break;
            case "superchance":
                superChancePanel.SetActive(false);
                break;

            default:
                break;
        }

        CameraManager.Instance.CameraFocusOut();
        
        currentActivePanelName = "none";

        EndAction();
        RefreshPlayerPanel();
    }
    
    public void ToggleCurrentPanelOff()
    {
        TogglePanelOff(currentActivePanelName);
    }

    public void DisplayChanceInfo(string chance)
    {
        chancePanel.GetComponent<ChanceDisplay>().Display(chance);
    }

    private void RayCastBlockerStatus(bool status)
    {
        rayCastBlocker.SetActive(status);
    }

    public void ButtonBehavior()
    {
        if (Dice.TwoDiceSameValue("d6") && !GameManager.Instance.IsCurrentPlayerInJail())
            SetRollDiceButtonStatus(true);
    }

    public void OnAction()
    {
        SetRollDiceButtonStatus(false);
        SetEndTurnButtonStatus(false);
        SetSellButtonStatus(false);
        RayCastBlockerStatus(true);

        Debug.Log("OnAction");
    }
    
    public void SelectingTile()
    {
        SetRollDiceButtonStatus(false);
        SetEndTurnButtonStatus(false);
        SetSellButtonStatus(false);
        RayCastBlockerStatus(false);

        Debug.Log("SelectingTile");
    }

    public void EndAction()
    {
        SetRollDiceButtonStatus(false);
        SetEndTurnButtonStatus(true);
        SetSellButtonStatus(true);
        RayCastBlockerStatus(false);

        Debug.Log("EndAction");
    }
    
    public void NextTurn()
    {
        SetRollDiceButtonStatus(true);
        SetEndTurnButtonStatus(false);
        SetSellButtonStatus(false);
        RayCastBlockerStatus(false);

        Debug.Log("NextTurn");
    }

    public void RefreshPlayerPanel()
    {
        for (int i = 0; i < GameManager.Instance.NumberOfPlayer; i++)
        {
            playerInfoPanels[i].SetActive(true);
            playerInfoPanels[i].GetComponent<InfoPanel>().RefreshInfo(GameManager.Instance.GetPlayerMoney(i), 0);
        }    
    }

    protected virtual void SellButtonFunction()
    {
        
    }
    
    protected virtual void EndTurnButtonFunction()
    {
        GameManager.Instance.TurnLogic();
        NextTurn();
    }
    
    protected virtual void RollDiceButtonFunction()
    {
        DiceManager.Instance.ThrowTheDice();
        OnAction();
    }
    
    protected virtual void PauseButtonFunction()
    {

    }

    //private void Reset()
    //{
    //    LoadPlayerPanels();
    //}

    //protected virtual void LoadPlayerPanels()
    //{
    //    Transform canvasObj = transform.Find("Canvas");
    //    Transform playerPanelObj = canvasObj.transform.Find("PlayerInfoPanels");

    //    foreach (Transform panel in playerPanelObj)
    //    {
    //        playerInfoPanels.Add(panel.gameObject);
    //    }
    //}
}

