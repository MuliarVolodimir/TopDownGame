using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _pickUptext;
    [SerializeField] InteractSystem _interactSystem;

    private void Start()
    {
        _pickUptext.gameObject.SetActive(false);
        _interactSystem = GetComponent<InteractSystem>();
    }
}
