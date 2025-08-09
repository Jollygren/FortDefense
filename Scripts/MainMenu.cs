using Godot;
using System;

public partial class MainMenu : Node2D
{
    PackedScene _gameScene;
    private void OnStartPressed()
    {
        var startButton = GetNodeOrNull<Button>("StartButton");
        var exitButton = GetNodeOrNull<Button>("ExitButton");
        var background = GetNodeOrNull<MarginContainer>("MarginContainer");

        startButton.QueueFree();
        exitButton.QueueFree();
        background.QueueFree();


        _gameScene = GD.Load<PackedScene>("res://main.tscn");
        var instance = _gameScene.Instantiate();
        AddChild(instance);
    }

    private void OnExitPressed()
    {
        this.GetTree().Quit(); // Exit the game
    }

    public override void _Ready()
    {
        // This method is called when the node is added to the scene.
        // You can use it to initialize variables, connect signals, or set up the node's properties.

        // Example: Connect a signal to a method
        var startButton = GetNodeOrNull<Button>("StartButton");
        var exitButton = GetNodeOrNull<Button>("ExitButton");

        if (startButton != null)
        {
            startButton.Pressed += OnStartPressed;
        }
        else
        {
            GD.PrintErr("StartButton node not found.");
        }


        if (exitButton != null)
        {
            exitButton.Pressed += OnExitPressed;
        }
        else
        {
            GD.PrintErr("ExitButton node not found.");
        }
        
    }
}
