using UnityEngine;

/// <summary>
/// 상호작용 가능한 오브젝트가 구현해야 하는 인터페이스입니다.
/// </summary>
public interface IInteractable
{
    void Interact(GameObject interactor);
}