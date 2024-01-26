using System.Collections.Generic;
using UnityEngine;

public class ChanceManager : MonoBehaviour
{
    private static ChanceManager instance;

    [SerializeField] private int chanceNumber = 0;
    [SerializeField] private int chestNumber = 0;

    [SerializeField] private string drawCard;

    [SerializeField] private string[] chances;
    [SerializeField] private string[] chests;

    private Queue<int> chanceQueue = new Queue<int>();
    private Queue<int> chestQueue = new Queue<int>();

    public static ChanceManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CreateQueue(chances, chanceQueue);
        CreateQueue(chests, chestQueue);
    }

    public virtual void CreateQueue(string[] array, Queue<int> queue)
    {
        // Create a list of numbers from 0 to 15
        List<int> numbers = new List<int>();
        for (int i = 0; i < array.Length; i++)
        {
            numbers.Add(i);
        }

        // Shuffle the list of numbers
        for (int i = 0; i < numbers.Count; i++)
        {
            int temp = numbers[i];
            int randomIndex = Random.Range(i, numbers.Count);
            numbers[i] = numbers[randomIndex];
            numbers[randomIndex] = temp;
        }

        // Add the shuffled numbers to the queue
        foreach (int number in numbers)
        {
            queue.Enqueue(number);
        }
    }

    public void Draw(string draw)
    {

        UIManager.Instance.TogglePanelOn("chance");
        drawCard = draw;
        if(draw == "chance")
        {
            chanceNumber = chanceQueue.Dequeue();
            chanceQueue.Enqueue(chanceNumber);


            UIManager.Instance.DisplayChanceInfo(chances[chanceNumber]);
        }

        else if(draw == "chest")
        {
            chestNumber = chestQueue.Dequeue();
            chestQueue.Enqueue(chanceNumber);


            UIManager.Instance.DisplayChanceInfo(chests[chestNumber]);
        }
    }

    public void ExecuteEffect()
    {

        if(drawCard == "chance")
        {
            switch (chanceNumber)
            {
                case 0:
                    AdvanceToGo();
                    break;
                case 1:
                    AdvanceToGo();
                    break;
                case 2:
                    Debug.Log("two");
                    AdvanceToGo();
                    break;
                default:
                    AdvanceToGo();
                    break;
            }
        }

        else if(drawCard == "chest")
        {
            switch (chestNumber)
            {
                case 0:
                    AdvanceToGo();
                    break;
                case 1:
                    AdvanceToGo();
                    break;
                case 2:
                    Debug.Log("two");
                    AdvanceToGo();
                    break;
                default:
                    AdvanceToGo();
                    break;
            }
        }
        
    }
    
    private void AdvanceToGo()
    {
        int step = 40 - GameManager.Instance.GetCurrentPlayerTileIndex();
        MovingManager.Instance.StartMove(step);
    }
    
    private void AdvanceTo39()
    {
        int step = 39 - GameManager.Instance.GetCurrentPlayerTileIndex();
        MovingManager.Instance.StartMove(step);
    }
    
    private void AdvanceTo37()
    {
        int step = 37 - GameManager.Instance.GetCurrentPlayerTileIndex();
        MovingManager.Instance.StartMove(step);
    }

    private void AdvanceToNearUltility()
    {
        int step = 0;
        if(GameManager.Instance.GetCurrentPlayerTileIndex() < 12)
        {
            step = 12 - GameManager.Instance.GetCurrentPlayerTileIndex();
        }
        else if(GameManager.Instance.GetCurrentPlayerTileIndex() < 27)
        {
            step = 27 - GameManager.Instance.GetCurrentPlayerTileIndex();
        }
        else
        {
            step = 40 - GameManager.Instance.GetCurrentPlayerTileIndex() + 12;
        }

        MovingManager.Instance.StartMove(step);
    }
    
    private void AdvanceToNearRailroad()
    {
        int step = 0;
        if (GameManager.Instance.GetCurrentPlayerTileIndex() < 12)
        {
            step = 12 - GameManager.Instance.GetCurrentPlayerTileIndex();
        }
        else if (GameManager.Instance.GetCurrentPlayerTileIndex() < 27)
        {
            step = 27 - GameManager.Instance.GetCurrentPlayerTileIndex();
        }
        else
        {
            step = 40 - GameManager.Instance.GetCurrentPlayerTileIndex() + 12;
        }

        MovingManager.Instance.StartMove(step);
    }
}
