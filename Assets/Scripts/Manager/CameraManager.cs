using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static float ZOOM_IN_OTHOGRAPHICSIZE = 30f;
    public static float ZOOM_OUT_OTHOGRAPHICSIZE = 121.5f;

    private static CameraManager instance;

    public static CameraManager Instance { get => instance; }

    private Vector3 cameraOriginalPosition;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SetLocalPosition();
    }

    protected virtual void SetLocalPosition()
    {
        cameraOriginalPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
    }

    public virtual void CameraFocusIn(Transform targetTile)
    {
        Camera.main.orthographicSize = ZOOM_IN_OTHOGRAPHICSIZE;
        Camera.main.transform.DOMove(targetTile.position, MovingManager.DOTWEEN_DOMOVE_DURATION).SetEase(Ease.InSine);
        UIManager.Instance.ToggleInfoPanel(true);
    }

    public virtual void CameraFocusOut()
    {
        Camera.main.orthographicSize = ZOOM_OUT_OTHOGRAPHICSIZE;
        Camera.main.transform.DOMove(cameraOriginalPosition, MovingManager.DOTWEEN_DOMOVE_DURATION).SetEase(Ease.OutSine);
        UIManager.Instance.ToggleInfoPanel(false);
    }
}
