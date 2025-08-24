using Godot;
using System;

public partial class MainMenu : Node2D
{
    [Signal]
    public delegate void OnStartPressedEventHandler();

    [Signal]
    public delegate void OnExitPressedEventHandler();
    private void StartPressed()
    {
        EmitSignal(SignalName.OnStartPressed);
        this.QueueFree();
    }

    private void ExitPressed()
    {
        EmitSignal(SignalName.OnExitPressed);
        this.QueueFree();
    }

    public override void _Ready()
    {
        //Connects Start and Exit to StartPressed and Exit Pressed, checks to see if they exist
        var startButton = GetNodeOrNull<Button>("StartButton");
        var exitButton = GetNodeOrNull<Button>("ExitButton");

        if (startButton != null)
        {
            startButton.Pressed += StartPressed;
        }
        else
        {
            GD.PrintErr("StartButton node not found.");
        }


        if (exitButton != null)
        {
            exitButton.Pressed += ExitPressed;
        }
        else
        {
            GD.PrintErr("ExitButton node not found.");
        }
        
    }
}
