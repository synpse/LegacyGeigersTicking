using UnityEngine;

public class Drawer : MonoBehaviour, IIndirectInteractible
{
    public void Interact()
    {
        GetComponent<Animator>().SetTrigger("OpenDrawer");
    }
}