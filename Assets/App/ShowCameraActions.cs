using HoloToolkitExtensions.Utilities;
using UnityEngine;

public class ShowCameraActions : MonoBehaviour
{
    private TextMesh _mesh;

    [SerializeField]
    private float _rotationThreshold = 10f;

    [SerializeField]
    private float _moveTreshold = 0.4f;

    [SerializeField]
    private float _moveTime = 0.2f;

    private bool _isBusy;

    void Start()
    {
        _mesh = GetComponentInChildren<TextMesh>();
        MoveText();
    }

    void Update()
    {
        SetText();
        if ((CameraMovementTracker.Instance.RotationDelta > _rotationThreshold ||
            CameraMovementTracker.Instance.Distance > _moveTreshold ) && !_isBusy)
        {
            MoveText();
        }
    }

    private void MoveText()
    {
        _isBusy = true;
        LeanTween.move(gameObject, 
                        LookingDirectionHelpers.CalculatePositionDeadAhead(), _moveTime).
                  setEaseInOutSine().setOnComplete(() => _isBusy = false);
    }

    private void SetText()
    {
        var text = 
            string.Format("Speed: {0:00.00} km/h - Rotation: {1:000.0}° - Moved {2:00.0}m",
            CameraMovementTracker.Instance.Speed,
            CameraMovementTracker.Instance.RotationDelta,
            CameraMovementTracker.Instance.Distance);
        if (_mesh.text != text)
        {
            _mesh.text = text;
            Debug.Log(text);
        }
    }
}
