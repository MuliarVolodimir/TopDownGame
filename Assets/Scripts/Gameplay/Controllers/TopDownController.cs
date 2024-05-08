using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class TopDownController : MonoBehaviour
{
    public float Speed;
    [SerializeField] float _rotSpeed;
    [SerializeField] PlayerState _playerState;

    [SerializeField] Transform _armTransform;
    private CharacterController _cc;
    private Quaternion _lastRotation = Quaternion.identity;
    
    [SerializeField]  GameObject _curEquipItem;
    private PlayerAction _action;
    private PlayerInventory _inventory;

    private bool _isSprinting;

    public enum PlayerState
    {
        Walk,
        Run,
        Atack,
        UseSuper,
        Idle
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
            _inventory.OnInventoryAction += UpdateGraphics;
        }
        _cc = GetComponent<CharacterController>();
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
        float speed = Speed;

        if (_isSprinting)
        {
            speed = Speed * 1.75f;
        }

        return speed;
    }

    public void UseItem()
    {
        if (_curEquipItem != null)
        {
            IItem item = _curEquipItem.GetComponent<IItem>();
            item.DoAction();
        }
    }

    private void UpdateGraphics(Item item, int ammoCount)
    {
        if (_curEquipItem != null)
        {
            Destroy(_curEquipItem);
        }
        if (item != null)
        {
            _curEquipItem = Instantiate(item.ItemPrefab, _armTransform.transform);
            Debug.Log("graphics updated, cur item: " + item.Name);
        }     
    }
}