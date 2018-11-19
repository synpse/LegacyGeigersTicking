using UnityEngine;

public class VaultDoor : MonoBehaviour, IIndirectInteractible
{
    public void Interact()
    {
        GetComponent<Animator>().SetTrigger("OpenVaultDoor");
    }
}