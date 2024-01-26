using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DiceManager : MonoBehaviour
{
    private static DiceManager instance;

    [SerializeField] private int diceValue = 0;
    [SerializeField] private int doubleInRow = 0;
    [SerializeField] private float waitForResult = 1.5f;
    [SerializeField] private bool diceRolled = false;

    [SerializeField] private Transform dice1SpawnPoint;
    [SerializeField] private Transform dice2SpawnPoint;

    public static DiceManager Instance { get => instance; }
    public int DiceValue { get => diceValue;}

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        this.DoneRolling();
    }

    public void ThrowTheDice()
    {
        CameraManager.Instance.CameraFocusOut();
        UIManager.Instance.OnAction();

        diceRolled = true;
        Dice.Clear();
        Dice.Roll("1d6", "d6-" + RandomColor, dice1SpawnPoint.position, Force());
        Dice.Roll("1d6", "d6-" + RandomColor, dice2SpawnPoint.position, Force());
    }

    protected virtual void DoneRolling()
    {
        if (!Dice.rolling && diceRolled)
        {
            StartCoroutine(TakeDiceResult());
            diceRolled = false;
        }
    }
    
    private Vector3 Force()
    {
        Vector3 rollTarget = Vector3.zero + new Vector3(2 + 7 * Random.value, .5F + 4 * Random.value, -2 - 3 * Random.value);
        return Vector3.Lerp(transform.position, rollTarget, 1).normalized * (-50 - Random.value * 60);
    }

    private void OnGUI()
    {
        if (Dice.Count("") > 0)
        {
            GUI.Box(new Rect(10, Screen.height - 75, Screen.width - 20, 30), "");
            GUI.Label(new Rect(20, Screen.height - 70, Screen.width, 20), Dice.AsString(""));
        }
    }

    private string RandomColor
    {
        get
        {
            string _color = "blue";
            int c = System.Convert.ToInt32(Random.value * 6);
            switch (c)
            {
                case 0: _color = "red"; break;
                case 1: _color = "green"; break;
                case 2: _color = "blue"; break;
                case 3: _color = "yellow"; break;
                case 4: _color = "white"; break;
                case 5: _color = "black"; break;
            }
            return _color;
        }
    }

    private IEnumerator TakeDiceResult()
    {
        yield return new WaitForSeconds(waitForResult);
        diceValue = Dice.Value("d6");

        if (GameManager.Instance.IsCurrentPlayerInJail())
        {
            if (Dice.TwoDiceSameValue("d6"))
            {
                GameManager.Instance.SetIsInJail(false);
                MovingManager.Instance.StartMove(DiceValue);
            }

            else {
                UIManager.Instance.EndAction();
            }
        }

        else
        {
            if (doubleInRow >= 2 && Dice.TwoDiceSameValue("d6"))
            {
                doubleInRow = 0;

                MovingManager.Instance.MoveToJail();
                UIManager.Instance.EndAction();
            }
            else
            {
                if (Dice.TwoDiceSameValue("d6"))
                    doubleInRow++;
                else
                    doubleInRow = 0;
                MovingManager.Instance.StartMove(DiceValue);
            }
        }
    }
}