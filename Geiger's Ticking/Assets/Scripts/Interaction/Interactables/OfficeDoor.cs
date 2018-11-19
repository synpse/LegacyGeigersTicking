using UnityEngine;

public class OfficeDoor : MonoBehaviour, IIndirectInteractible
{
    public void Interact()
    {
        GetComponent<Animator>().SetTrigger("OpenOfficeDoor");
    }
}
