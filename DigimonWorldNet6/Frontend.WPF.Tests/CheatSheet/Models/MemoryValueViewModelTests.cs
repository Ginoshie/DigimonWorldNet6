using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.CheatSheet.Models;
using NUnit.Framework;
using Shouldly;

namespace Frontend.WPF.Tests.CheatSheet.Models;

[TestFixture]
public sealed class MemoryValueViewModelTests
{
    [Test]
    public void ReassertLock_ShouldWriteHeldValue_WhenLocked()
    {
        // Arrange
        bool ram = false;
        MemoryValueViewModel sut = new("Flag", () => ram, v => ram = v) { Value = true };
        ram = false;
        sut.IsLocked = true;

        // Act
        sut.PushLockedValueToMemory();

        // Assert
        ram.ShouldBeTrue();
    }

    [Test]
    public void ReassertLock_ShouldNotWrite_WhenUnlocked()
    {
        // Arrange
        bool ram = false;
        MemoryValueViewModel sut = new("Flag", () => ram, v => ram = v) { Value = true };
        ram = false;

        // Act
        sut.PushLockedValueToMemory();

        // Assert
        ram.ShouldBeFalse();
    }

    [Test]
    public void Refresh_ShouldNotAdoptGameValueAsHeld_WhenLocked()
    {
        // Arrange
        bool ram = true;
        MemoryValueViewModel sut = new("Flag", () => ram, v => ram = v) { Value = true, IsLocked = true };

        // Act
        ram = false;
        sut.Refresh();
        sut.PushLockedValueToMemory();

        // Assert
        ram.ShouldBeTrue();
    }
}
