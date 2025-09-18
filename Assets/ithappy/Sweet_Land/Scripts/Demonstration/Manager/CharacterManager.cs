using UnityEngine;

namespace ithappy
{
    public class CharacterManager : MonoBehaviour
    {
        [Header("Character Settings")] [SerializeField]
        private PlayerCharacterInput characterPrefab;

        [SerializeField] private Transform characterSpawnPoint;

        [Header("Camera Settings")] [SerializeField]
        private EditorLikeCameraController camera;

        private PlayerCharacterInput _character;
        private Vector3 _targetCameraPosition;
        private Quaternion _targetCameraRotation;
        private Vector3 _smoothCameraVelocity;
        private Transform _defaultParentCamera;
        private Vector3 _defaultPositionCamera;

        public void StartCharacterInput()
        {
            if (_character != null)
            {
                return;
            }

            camera.enabled = false;
            _character = Instantiate(characterPrefab, characterSpawnPoint.position, Quaternion.identity,
                characterSpawnPoint);

            _defaultParentCamera = camera.transform.parent;
            _defaultPositionCamera = camera.transform.localPosition;

            camera.transform.parent = _character.CameraParent;
            camera.transform.localPosition = Vector3.zero;
            camera.transform.localRotation = Quaternion.identity;
        }

        private void Update()
        {
            if (_character != null && Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(_character.gameObject);
                _character = null;

                camera.transform.parent = _defaultParentCamera;
                camera.transform.localPosition = _defaultPositionCamera;
                camera.transform.localRotation = Quaternion.identity;
                camera.enabled = true;
            }
        }
    }
}
