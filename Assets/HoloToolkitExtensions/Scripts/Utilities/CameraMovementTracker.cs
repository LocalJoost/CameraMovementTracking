using HoloToolkit.Unity;
using UnityEngine;

namespace HoloToolkitExtensions.Utilities
{
    public class CameraMovementTracker : Singleton<CameraMovementTracker>
    {
        [SerializeField]
        private readonly float _sampleTime = 1.0f;

        private Vector3 _lastSampleLocation;
        private Quaternion _lastSampleRotation;
        private float _lastSampleTime;

        public float Speed { get; private set; }
        public float RotationDelta { get; private set; }
        public float Distance { get; private set; }

        void Start()
        {
            _lastSampleTime = Time.time;
            _lastSampleLocation = CameraCache.Main.transform.position;
            _lastSampleRotation = CameraCache.Main.transform.rotation;
        }

        void Update()
        {
            if (Time.time - _lastSampleTime > _sampleTime)
            {
                Speed = CalculateSpeed();
                RotationDelta = CalculateRotation();
                Distance = CalculateDistanceCovered();
                _lastSampleTime = Time.time;
                _lastSampleLocation = CameraCache.Main.transform.position;
                _lastSampleRotation = CameraCache.Main.transform.rotation;
            }
        }

        private float CalculateDistanceCovered()
        {
            return Vector3.Distance(_lastSampleLocation, CameraCache.Main.transform.position);
        }

        private float CalculateSpeed()
        {
            // return speed in km/h
            return CalculateDistanceCovered() / (Time.time - _lastSampleTime) * 3.6f;
        }

        private float CalculateRotation()
        {
            return Mathf.Abs(Quaternion.Angle(_lastSampleRotation, CameraCache.Main.transform.rotation));
        }
    }
}
