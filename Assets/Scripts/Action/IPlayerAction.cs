namespace WinterTrap.Action
{
    using System;
    using View;

    public interface IPlayerAction
    {
        string localizedMessage { get; }
        void DoIt(IPlayerView view, Action onComplete = null);
    }
}