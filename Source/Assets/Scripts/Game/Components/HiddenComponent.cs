using UnityEngine;

public class HiddenComponent : MonoBehaviour
{
  void Start()
  {
    gameObject.GetComponent<MeshRenderer>().enabled = false;
  }

  void Update()
  {

  }
}