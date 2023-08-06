using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD Ins;

    private void Awake()
    {
        Ins = this;
    }

    [SerializeField] private Canvas _cardOptionsCanvas;
    [SerializeField] private Button _takeCardButton;
    [SerializeField] private Canvas _actionCanvas;

    public void SetActionMenuVisibility(bool enable)
    {
        _actionCanvas.enabled = enable;
    }
    
    public void ShowCardOptions(bool isShowTakeButton)
    {
        _cardOptionsCanvas.enabled = true;
        _takeCardButton.interactable = isShowTakeButton;
    }

    public void HidCardOptions()
    {
        _cardOptionsCanvas.enabled = false;
    }
}
