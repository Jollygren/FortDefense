using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class Game : Node2D
{

    private TileMapLayer BuildLayer;
    private Player player;
    public override void _Ready()
    {
        BuildLayer = GetNode<TileMapLayer>("World/BuildLayer");
        player = GetNode<Player>("Player");
        player.BuildFoundation += OnBuildFoundation;
    }

    public override void _Process(double delta)
    {

    }

    private void OnBuildFoundation(Node2D foundationInstance)
    {
        BuildLayer.AddChild(foundationInstance);
        if (Input.IsActionPressed("build"))
        {
            return;
        }
        foundationInstance.QueueFree();
    }
}
