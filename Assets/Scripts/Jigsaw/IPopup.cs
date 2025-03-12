
public interface IPopup
{
    void Show(object data = null);
    void Hide();
    bool IsActive { get; }
}
