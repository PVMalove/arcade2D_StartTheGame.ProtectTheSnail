using UnityEngine;

namespace CodeBase.Character
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private GameObject _moveRightTop;
        [SerializeField] private GameObject _moveRightDown;
        [SerializeField] private GameObject _moveLeftTop;
        [SerializeField] private GameObject _moveLeftDown;

        public Position Position;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
                MoveRightTop();

            if (Input.GetKeyDown(KeyCode.D)) 
                MoveRightDown();

            if (Input.GetKeyDown(KeyCode.Q)) 
                MoveLeftTop();

            if (Input.GetKeyDown(KeyCode.A)) 
                MoveLeftDown();
        }

        private void MoveRightTop()
        {
            Position = Position.RightTop;
            _moveRightTop.SetActive(true);
            
            _moveRightDown.SetActive(false);
            _moveLeftTop.SetActive(false);
            _moveLeftDown.SetActive(false);
        }

        private void MoveRightDown()
        {
            _moveRightTop.SetActive(false);

            Position = Position.RightDown;
            _moveRightDown.SetActive(true);
            
            _moveLeftTop.SetActive(false);
            _moveLeftDown.SetActive(false);
        }

        private void MoveLeftTop()
        {
            _moveRightTop.SetActive(false);
            _moveRightDown.SetActive(false);

            Position = Position.LeftTop;
            
            _moveLeftTop.SetActive(true);
            _moveLeftDown.SetActive(false);
        }

        private void MoveLeftDown()
        {
            _moveRightTop.SetActive(false);
            _moveRightDown.SetActive(false);
            _moveLeftTop.SetActive(false);

            Position = Position.LeftDown;
            _moveLeftDown.SetActive(true);
        }
    }
}