using System;
using ButchersGames;
using Dreamteck.Splines;
using UI;
using UniRx;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Animator anim;
        [SerializeField] private SplineFollower follower;
        [SerializeField] private Transform visual;
        [SerializeField] private float speed;
        [SerializeField] private float sensitivity;
        [SerializeField] private float maxOffset;
        [SerializeField] private float rotationAngle;
        [SerializeField] private float rotationSpeed;

        private Vector2 _currentPos = Vector2.zero;
        private Vector2 _lastPos = Vector2.zero;
        
        private Quaternion _finalRotation;
        private Quaternion _startRotation;
        
        private static readonly int Walk = Animator.StringToHash("Walk");
        private readonly CompositeDisposable _disposable = new();
        private static readonly int Dance = Animator.StringToHash("Dance");

        private void Awake()
        {
            StartPanel.PlayerStarted += StartMove;
            PlayerRichChanger.Lost += StopMove;
            Finisher.Finished += Finish;

            follower.follow = false;
            follower.followSpeed = speed;
            _startRotation = visual.localRotation;
        }
        
        private void OnDestroy()
        {
            StartPanel.PlayerStarted -= StartMove;
            PlayerRichChanger.Lost -= StopMove;
            Finisher.Finished -= Finish;
        }
        
        private void Finish()
        {
            follower.follow = false;
            anim.SetTrigger(Dance);
        }

        private void StartMove()
        {
            follower.follow = true;
            anim.SetBool(Walk,true);
        }
        
        private void StopMove()
        {
            follower.follow = false;
            anim.SetBool(Walk, false);
        }
        
        private void Update()
        {
            visual.localRotation =
                Quaternion.Lerp(visual.localRotation, _finalRotation, Time.deltaTime * rotationSpeed);

            if (Input.GetMouseButtonDown(0)) _lastPos = Input.mousePosition;
            
            if (!Input.GetMouseButton(0))
            {
                _finalRotation = _startRotation;
                return;
            }
            
            _currentPos = Input.mousePosition;
            
            if ((_lastPos - _currentPos).sqrMagnitude < 0.1f)
            {
                if (_disposable.Count > 0)
                    return;
                
                Observable.Timer(TimeSpan.FromSeconds(0.3f)).Subscribe(x =>
                {
                    _disposable.Clear();
                    _finalRotation = _startRotation;
                }).AddTo(_disposable);
                return;
            }
            
            var offset = new Vector2((_currentPos - _lastPos).x * sensitivity, 0);
            follower.motion.offset += offset;
            follower.motion.offset = new Vector2(Mathf.Clamp(follower.motion.offset.x, -maxOffset, maxOffset), 0);

            float finalRotation = (Mathf.Sign(follower.motion.offset.x) < 0 ? 360 : 2 * rotationAngle) - rotationAngle;
            Rotate(finalRotation);

            _lastPos = _currentPos;
        }

        private void Rotate(float angle)
        {
            _finalRotation = Quaternion.Euler(0, angle, 0);
        }
    }
}
