using UnityEngine;
using VContainer;

[RequireComponent(typeof(CharacterController))]

public class TopDownController : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _rotSpeed;
    [SerializeField] PlayerState _playerState;
    
    [SerializeField] Transform _armTransform;

    private CharacterController _cc;
    private Quaternion _lastRotation = Quaternion.identity;
    
    private Item _curEquipItem;
    private GameObject _curItemObj;
    private PlayerAction _action;
    private PlayerInventory _inventory;
    private PlayerCharacter _character;
    private CharacterGraphics _characterGraphics;

    private bool _isSprinting;
    

    public enum PlayerState
    {
        Walk,
        Run,
        Atack,
        UseSuper,
        Idle
    }

    [Inject]
    public void Construct(ApplicationData appData)
    {
        _speed = appData.SelectedCharacterInfo.GameCharacter.CharacterCharacteristics.Speed;
    }

    private void Start()
    {
        if (_action == null)
        {
            _action = GetComponent<PlayerAction>();
        }
        if (_inventory == null)
        {
            _inventory = GetComponent<PlayerInventory>();
            _inventory.OnInventoryAction += _inventory_OnInventoryAction;
        }
        if (_cc == null)
        {
            _cc = GetComponent<CharacterController>();
        }
        if (_character == null)
        {
            _character = GetComponent<PlayerCharacter>();
        }
        if (_characterGraphics == null)
        {
            _characterGraphics = GetComponent<CharacterGraphics>();
        }
    }

    public void HandleInput(Vector3 movedir, bool isAiming, bool isSprinting)
    {
        _isSprinting = isSprinting;

        if (isAiming)
        {
            Aiming();
        }
        else
        {
            if (movedir.magnitude > 0)
            {
                _lastRotation = Quaternion.LookRotation(movedir);
            }
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, _lastRotation, _rotSpeed * Time.deltaTime);

        MovePlayer(movedir);
    }

    private void Aiming()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 mousePos = ray.GetPoint(rayDistance);
            Vector3 lookDir = mousePos - transform.position;
            lookDir.y = 0f;

            _lastRotation = Quaternion.LookRotation(lookDir);
        }
    }

    private void MovePlayer(Vector3 moveDir)
    {
        var speed = CheckSpeed();
        _cc.Move(moveDir * speed * Time.fixedDeltaTime);
    }

    private float CheckSpeed()
    {
        float speed = _speed;

        if (_isSprinting)
        {
            speed = _speed * 1.75f;
        }

        return speed;
    }

    public void UseItem()
    {
        if (_curEquipItem != null)
        {
            _curEquipItem.DoAction(_character);
        }
    }

    private void _inventory_OnInventoryAction(ItemSO item)
    {
        if (_curItemObj != null)
        {
            Destroy(_curItemObj);
        }
        if (item != null)
        {
            _curItemObj = Instantiate(item.ItemPrefab, _armTransform.transform);
            _curEquipItem = _curItemObj.GetComponent<Item>();
            Debug.Log("graphics updated, cur item: " + _curItemObj.name);
        }
    }
}