using TMPro;
using UnityEngine;

public class PickUpable : MonoBehaviour, IInteractable
{
    [SerializeField] ItemSO _item;
    [SerializeField] TextMeshPro _infoText;
    [SerializeField] Transform _itemSpawnPos;
    private Camera _camMain;

    public void SetItem(ItemSO item)
    {
        _item = item;
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _infoText.gameObject.SetActive(false);
        _camMain = Camera.main;
        transform.position += new Vector3(0, 0.5f, 0); 
        GameObject currentGameObj = Instantiate(_item.ItemPrefab, _itemSpawnPos);
    }

    public void Interact(GameObject interacter)
    {
        PlayerInventory inventory = interacter.GetComponent<PlayerInventory>();
        inventory.AddItem(_item);

        Debug.Log(interacter.name + "interact with " + _item.Name);
        Destroy(gameObject);
    }

    public void GetInfo(bool isEnable)
    {
        if (_infoText != null && _camMain != null)
        {
            _infoText.transform.rotation = _camMain.transform.rotation;
            _infoText.transform.gameObject.SetActive(isEnable);
        }
    }
}
