using UnityEngine;

namespace CodeBase.Character
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private GameObject _moveRightTop;
        [SerializeField] private GameObject _moveRightDown;
        [SerializeField] private GameObject _moveLeftTop;
        [SerializeField] private GameObject _moveLeftDown;

        public Position Position;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Position = Position.RightTop;
                MoveRightTop();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                Position = Position.RightDown;
                MoveRightDown();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Position = Position.LeftTop;
                MoveLeftTop();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                Position = Position.LeftDown;
                MoveLeftDown();
            }
        }

        private void MoveRightTop()
        {
            _moveRightTop.SetActive(true);
            _moveRightDown.SetActive(false);
            _moveLeftTop.SetActive(false);
            _moveLeftDown.SetActive(false);
        }

        private void MoveRightDown()
        {
            _moveRightTop.SetActive(false);
            _moveRightDown.SetActive(true);
            _moveLeftTop.SetActive(false);
            _moveLeftDown.SetActive(false);
        }

        private void MoveLeftTop()
        {
            _moveRightTop.SetActive(false);
            _moveRightDown.SetActive(false);
            _moveLeftTop.SetActive(true);
            _moveLeftDown.SetActive(false);
        }

        private void MoveLeftDown()
        {
            _moveRightTop.SetActive(false);
            _moveRightDown.SetActive(false);
            _moveLeftTop.SetActive(false);
            _moveLeftDown.SetActive(true);
        }
    }
}