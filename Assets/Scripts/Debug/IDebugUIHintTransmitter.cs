namespace WinterTrap.Debug
{
    public delegate void UIShowHint(object owner, string localizedMessage, float timeSeconds = 0f);
    public delegate void UIHideHint(object owner);

    public interface IDebugUIHintTransmitter
    {
        event UIShowHint onShowHint;
        event UIHideHint onHideHint;
    }
}