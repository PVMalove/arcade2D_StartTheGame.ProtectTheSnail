using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PauseService;
using UnityEngine;

namespace CodeBase.Gameplay.Player
{
    public class PlayerMovement : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private GameObject _moveRightTop;
        [SerializeField] private GameObject _moveRightDown;
        [SerializeField] private GameObject _moveLeftTop;
        [SerializeField] private GameObject _moveLeftDown;

        public Position Position;

        private IInputService _input;
        private bool _isPaused;

        private void Awake() =>
            _input = AllServices.Container.Single<IInputService>();

        private void Update()
        {
            if(_isPaused)
                return;
            
            ControlButtonQEAD();

            ControlButtonArrows();
        }

        private void ControlButtonArrows()
        {
            if (_input.Top)
                MoveTop();

            if (_input.Down)
                MoveDown();

            if (_input.Left)
                MoveLeft();

            if (_input.Right)
                MoveRight();
        }

        private void ControlButtonQEAD()
        {
            if (_input.RightTop)
                MoveRightTop();

            if (_input.RightDown)
                MoveRightDown();

            if (_input.LeftTop)
                MoveLeftTop();

            if (_input.LeftDown)
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

        private void MoveRight()
        {
            if (Position == Position.LeftTop)
                MoveRightTop();
            else if (Position == Position.LeftDown)
                MoveRightDown();
        }

        private void MoveLeft()
        {
            if (Position == Position.RightTop)
                MoveLeftTop();
            else if (Position == Position.RightDown)
                MoveLeftDown();
        }

        private void MoveDown()
        {
            if (Position == Position.LeftTop)
                MoveLeftDown();
            else if (Position == Position.RightTop)
                MoveRightDown();
        }

        private void MoveTop()
        {
            if (Position == Position.LeftDown)
                MoveLeftTop();
            else if (Position == Position.RightDown)
                MoveRightTop();
        }

        public void OnPauseChanged(bool isPaused) => 
            _isPaused = isPaused;
    }
}