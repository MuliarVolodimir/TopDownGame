using UnityEngine;

public class InteractSystem : MonoBehaviour
{
    [SerializeField] LayerMask _interactableLayer;
    [SerializeField] float _interactionRadius;

    private IInteractable _lastInteractGameObject;

    private void Update()
    {
        GetInteractItemInfo();
    }

    private void GetInteractItemInfo()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _interactionRadius, _interactableLayer);
        if (_lastInteractGameObject != null) _lastInteractGameObject.GetInfo(false);

        if (colliders.Length > 0)
        {
            _lastInteractGameObject = colliders[0].gameObject.GetComponent<IInteractable>();
            _lastInteractGameObject.GetInfo(true);
        }
        else
        {
            _lastInteractGameObject = null;
        }
    }

    public void TryInteract()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _interactionRadius, _interactableLayer);

        if (colliders.Length > 0)
        {
            _lastInteractGameObject = null;
            
            IInteractable interactable = colliders[0].gameObject.GetComponent<IInteractable>();
            
            if (interactable != null)
            {
                interactable.Interact(gameObject);
            }

            Debug.Log(colliders[0].gameObject.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _interactionRadius);
    }
}
