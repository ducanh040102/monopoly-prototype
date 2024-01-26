using UnityEngine;
using DG.Tweening;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    

    public static InputManager Instance { get => instance;}

    private void Awake()
    {
        instance = this;
    }

    

    private void Update()
    {
        this.ClickOnActions();
    }

    

    protected virtual void ClickOnActions()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseRayHit;

            if (Physics.Raycast(mouseRay, out mouseRayHit))
            {
                if (mouseRayHit.collider.tag == "Tile")
                    RayHitTile(mouseRayHit);

                else
                    CameraManager.Instance.CameraFocusOut();
            }
        }
    }
    
    protected virtual void RayHitTile(RaycastHit mouseRayHit)
    {
        GameObject hitTileObject = mouseRayHit.collider.gameObject;

        Tile hitTile = hitTileObject.GetComponent<Tile>();

        bool isRailroadOrWorldTravel = GameManager.Instance.UsingRailroad || GameManager.Instance.UsingWorldTravel;

        if (isRailroadOrWorldTravel)
            TravelWithRailroadOrWorld (hitTileObject, hitTile);

        else
        {
            Transform hitTileFocusPoint = hitTileObject.transform.Find("FocusPoint");

            if (hitTileFocusPoint == null) return;

            CameraManager.Instance.CameraFocusIn(hitTileFocusPoint);
        }
    }

    protected virtual void TravelWithRailroadOrWorld(GameObject hitTileObject, Tile hitTile)
    {
        bool isIgnoreByRailroad = GameManager.Instance.UsingRailroad && (!hitTile.isRailRoad || hitTile.ownerPlayerNumber == -1);
        bool isIgnoreByWorldTravel = GameManager.Instance.UsingWorldTravel && (!hitTile.isProperty || (hitTile.ownerPlayerNumber != -1 || hitTile.ownerPlayerNumber == GameManager.Instance.CurrentTurn));

        if (isIgnoreByRailroad || isIgnoreByWorldTravel) return;

        UIManager.Instance.OnAction();

        int hitTileIndex = TileManager.Instance.Tiles.IndexOf(hitTileObject);
        int playerPositionIndex = GameManager.Instance.GetCurrentPlayerTileIndex();

        if (hitTileIndex != playerPositionIndex)
        {
            int stepToGo = hitTileIndex - playerPositionIndex;

            if (hitTileIndex < playerPositionIndex)
                stepToGo = (TileManager.NUMBER_OF_TILES - playerPositionIndex) + hitTileIndex;

            MovingManager.Instance.StartMove(stepToGo);
        }
    }

    
}
