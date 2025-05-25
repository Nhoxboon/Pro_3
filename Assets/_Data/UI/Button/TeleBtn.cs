
using UnityEngine;
using UnityEngine.EventSystems;

public class TeleBtn : BtnBaseAbstract
{
    public Vector3 lastPosition;
    public Vector3 teleportPosition;
    public bool isClick;
    
    protected override void OnClick()
    {
        Teleport();
    }

    protected void Teleport()
    {
        if (isClick)
        {
            PlayerCtrl.Instance.transform.position = lastPosition;
            lastPosition = Vector3.zero;
            isClick = false;
        }
        else
        {
            lastPosition = PlayerCtrl.Instance.transform.position;
            PlayerCtrl.Instance.transform.position = teleportPosition;
            isClick = true;
        }
        EventSystem.current.SetSelectedGameObject(null);
    }
}
