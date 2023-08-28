using UnityEngine;


public class BaseInterrogateButtonSanksi : BaseInterrogateButton {
    protected bool isSelected;

    protected override void OnMouseExit() {
        base.OnMouseExit();
        if (isSelected) OnMouseOver();
    }

    public void ToggleState() {
        isSelected = !isSelected;
    }
}