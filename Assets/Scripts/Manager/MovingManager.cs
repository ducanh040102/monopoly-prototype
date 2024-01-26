using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingManager : MonoBehaviour
{
    public static float MOVING_DELAY = 0.5F;
    public static float MOVING_INTERVAL = 0.2F;
    public static float DOTWEEN_DOMOVE_DURATION = 0.3F;

    private static MovingManager instance;

    [SerializeField] private int step = 0;

    public static MovingManager Instance { get => instance; }

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        this.ResetPlayerIndex();
    }

    public void StartMove(int step)
    {
        InvokeRepeating("MoveTileToTile", MOVING_DELAY, MOVING_INTERVAL);
        this.step = step;
    }

    public void MoveToTile(int number)
    {
        PieceManager.Instance.Pieces[GameManager.Instance.CurrentTurn].transform.DOMove(TileManager.Instance.Tiles[number].transform.position, DOTWEEN_DOMOVE_DURATION).SetEase(Ease.InOutSine);
        GameManager.Instance.SetPlayerTileIndex(number);
    }

    public void MoveToJail()
    {
        MoveToTile(10);
    }

    private void MoveTileToTile()
    {
        PieceManager.Instance.Pieces[GameManager.Instance.CurrentTurn].transform.DOMove(TileManager.Instance.Tiles[GameManager.Instance.GetCurrentPlayerTileIndex() + 1].transform.position, 0.3f).SetEase(Ease.InOutSine);
        GameManager.Instance.SetPlayerTileIndex(GameManager.Instance.GetCurrentPlayerTileIndex() + 1);

        step--;
        if (step == 0)
        {
            CancelInvoke();
            StartCoroutine(WaitForFinishMove(1f));
        }
    }

    public virtual void ResetPlayerIndex()
    {
        if (GameManager.Instance.GetCurrentPlayerTileIndex() >= TileManager.Instance.Tiles.Capacity - 1 && step != 0)
        {
            GameManager.Instance.AddMoneyToCurrentPlayer(GameManager.Instance.StartMoney);
            GameManager.Instance.SetPlayerTileIndex(-1);
        }
    }

    IEnumerator WaitForFinishMove(float sec)
    {
        yield return new WaitForSeconds(sec);

        GameManager.Instance.GameLogic();

        UIManager.Instance.ButtonBehavior();

    }
}
