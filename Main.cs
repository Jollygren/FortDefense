using Godot;
using System;

public partial class Main : Node2D
{

    public override void _Ready()
    {
        var mainMenu = GetNodeOrNull<MainMenu>("MainMenu");

        if (mainMenu == null)
        {
            GD.PrintErr("Couldn't find the Main Menu, quiting.");
            this.GetTree().Quit();
        }
        mainMenu.OnStartPressed += StartPressed;
        mainMenu.OnExitPressed += ExitPressed;
    }

    private void StartPressed()
    {
        var gamescene = GD.Load<PackedScene>("res://game.tscn");
        var instance = gamescene.Instantiate();
        if (instance != null)
        {
            AddChild(instance);
        }
        else
        {
            GD.PrintErr("Failed to start game");
        }
    }

    private void ExitPressed()
    {
        this.GetTree().Quit();
    }

}
