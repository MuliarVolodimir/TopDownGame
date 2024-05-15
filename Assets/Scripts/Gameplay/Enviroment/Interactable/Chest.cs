using System.Linq;
using TMPro;
using UnityEngine;
using VContainer;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject _pickUpable;
    [SerializeField] Transform _itemSpawnPos;
    [SerializeField] TextMeshPro _infoText;
    [SerializeField] ItemsDataSO _items;

    private ItemSO _currItem;
    private Camera _camMain;

    private bool _isInteractable;

    [Inject]
    public void Construct(ItemsDataSO itemsToSelectData) 
    {
        _items = itemsToSelectData;
    }

    private void Start()
    {  
        InitCurrentItem();
        _camMain = Camera.main;
        _infoText.gameObject.SetActive(false);
        _isInteractable = true;
    }

    private void InitCurrentItem()
    {
        var items = _items.Weapons;
        var query = items.Where(item => item.Rarity == ItemSO.ItemRarity.Legendary).ToList();

        int index = Random.Range(0, query.Count - 1);

        _currItem = query[index];
    }

    public void Interact(GameObject interacter)
    {
        if (_isInteractable)
        {
            var item = Instantiate(_pickUpable, _itemSpawnPos.position, _itemSpawnPos.rotation);
            item.GetComponent<PickUpable>().SetItem(_currItem);

            _infoText.transform.gameObject.SetActive(false);
            _isInteractable = false;
            Destroy(gameObject);
        }  
    }

    public void GetInfo(bool isEnable)
    {
        if (_infoText != null && _camMain != null && _isInteractable)
        {
            _infoText.transform.rotation = _camMain.transform.rotation;
            _infoText.transform.gameObject.SetActive(isEnable);
        }
    }
}
