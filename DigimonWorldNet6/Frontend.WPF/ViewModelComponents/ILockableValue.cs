namespace DigimonWorld.Frontend.WPF.ViewModelComponents;

public interface ILockableValue
{
    bool IsLocked { get; set; }

    void PushLockedValueToMemory();
}
