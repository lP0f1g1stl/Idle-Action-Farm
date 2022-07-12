using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Joystick _js;
    [SerializeField] private Button _cutButton;
    [Space]
    [SerializeField] private Transform _character;
    [SerializeField] private GameObject _sickle;
    [Space]
    [SerializeField] private BorderData _borderData;


    private Animator _characterAnimator;

    bool _isWalking;

    private void Awake()
    {
        _characterAnimator = _character.GetComponent<Animator>();
    }
    private void OnEnable()
    {
        _js.OnJSDrag += CheckJS;
        _cutButton.onClick.AddListener(Cut);
    }
    private void OnDisable()
    {
        _js.OnJSDrag -= CheckJS;
        _cutButton.onClick.RemoveListener(Cut);
    }

    private void Update()
    {

        if (_isWalking)
        {
            RotatePlayerCharacter(_js.InputDirection);
            CheckPosition();
            _characterAnimator.SetFloat("Speed", _js.InputDirection.magnitude);
        }
    }
    private void RotatePlayerCharacter(Vector3 direction) 
    {
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        _character.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }
    private void CheckPosition() 
    {
        if (_character.position.x < _borderData.MinPosition.x)
        {
            ChangePosition( new Vector3(_borderData.MinPosition.x, _character.position.y, _character.position.z));
        }
        if (_character.position.z < _borderData.MinPosition.z)
        {
            ChangePosition( new Vector3(_character.position.x, _character.position.y, _borderData.MinPosition.z));
        }
        if (_character.position.x > _borderData.MaxPosition.x) 
        {
            ChangePosition( new Vector3(_borderData.MaxPosition.x, _character.position.y, _character.position.z)); 
        }
        if (_character.position.z > _borderData.MaxPosition.z) 
        {
            ChangePosition( new Vector3(_character.position.x, _character.position.y, _borderData.MaxPosition.z));
        }
        _character.position = new Vector3(_character.position.x, 0.5f, _character.position.z);
    }
    private void ChangePosition(Vector3 position) 
    {
        _character.position = position;
    }
    private void CheckJS(bool isWalking) 
    {
        _isWalking = isWalking;
        _characterAnimator.SetBool("Walking", _isWalking);
        if (!isWalking) 
        {
            _characterAnimator.SetFloat("Speed", 0);
        }
    }
    private void Cut() 
    {
        _sickle.SetActive(true);
        _characterAnimator.SetTrigger("Attack");
        DOVirtual.DelayedCall(2.5f, () => _sickle.SetActive(false));
    }
}
