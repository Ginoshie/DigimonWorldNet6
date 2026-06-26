using DigimonWorld.Frontend.WPF.Windows.Main.UserControls.CheatSheet.Models;
using NUnit.Framework;
using Shouldly;

namespace Frontend.WPF.Tests.CheatSheet.Models;

[TestFixture]
public sealed class NumericMemoryValueViewModelTests
{
    [Test]
    public void ReassertLock_ShouldWriteHeldValue_WhenLocked()
    {
        // Arrange
        int ram = 5;
        NumericMemoryValueViewModel sut = new("Stat", () => ram, v => ram = v) { Value = 10 };
        ram = 99;
        sut.IsLocked = true;

        // Act
        sut.PushLockedValueToMemory();

        // Assert
        ram.ShouldBe(10);
    }

    [Test]
    public void ReassertLock_ShouldNotWrite_WhenUnlocked()
    {
        // Arrange
        int ram = 5;
        NumericMemoryValueViewModel sut = new("Stat", () => ram, v => ram = v) { Value = 10 };
        ram = 99;

        // Act
        sut.PushLockedValueToMemory();

        // Assert
        ram.ShouldBe(99);
    }

    [Test]
    public void ReassertLock_ShouldWriteHeldValue_WhenEditing()
    {
        // Arrange
        int ram = 5;
        NumericMemoryValueViewModel sut = new("Stat", () => ram, v => ram = v) { Value = 10, IsLocked = true, IsEditing = true };
        ram = 99;

        // Act
        sut.PushLockedValueToMemory();

        // Assert
        ram.ShouldBe(10);
    }

    [Test]
    public void Refresh_ShouldNotAdoptGameValueAsHeld_WhenLocked()
    {
        // Arrange
        int ram = 10;
        NumericMemoryValueViewModel sut = new("Stat", () => ram, v => ram = v) { Value = 10, IsLocked = true };

        // Act
        ram = 99;
        sut.Refresh();
        sut.PushLockedValueToMemory();

        // Assert
        ram.ShouldBe(10);
    }
}
