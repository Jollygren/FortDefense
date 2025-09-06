using Godot;
using System;

public partial class Player : CharacterBody2D
{
    const int Speed = 300;
    const int MarkerDistance = 200;
    private AnimatedSprite2D animatedSprite;
    private Marker2D buildMarker;
    private TileMapLayer BuildLayer;
    private PackedScene foundationTemplate;
    private string direction;
    private bool keyReleased;

    [Signal]
    public delegate void BuildFoundationEventHandler(Node2D foundationInstance);

    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        buildMarker = GetNode<Marker2D>("BuildMarker");
        BuildLayer = GetNode<TileMapLayer>("../World/BuildLayer");
        if (BuildLayer == null)
        {
            GD.Print("BuildLayer not found");
        }
        foundationTemplate = GD.Load<PackedScene>("res://Foundation.tscn");
        direction = "";
        keyReleased = true;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        Vector2 velocity = GetVector();
        MoveAndCollide(velocity * (float)delta);

        if (keyReleased)
        {
            string tempDirection = direction;
            direction = GetDirectionPressed();
            UpdateBuildMarkerPosition();
            // Only set keyReleased to false if the direction has changed
            if (direction != tempDirection)
            {
                keyReleased = false;
            }
        }
        else if (Input.IsActionJustReleased("move" + direction))
        {
            keyReleased = true;
        }
        if (velocity != Vector2.Zero)
        {
            animatedSprite.Animation = "walk" + direction;
            animatedSprite.Play();
        }
        else
        {
            string currentAnimation = animatedSprite.GetAnimation();
            if (!currentAnimation.Contains("idle"))
            {
                animatedSprite.Animation = currentAnimation.Replace("walk", "idle");
            }
        }
        if (foundationTemplate != null)
        {
            var foundationInstance = foundationTemplate.Instantiate() as Node2D;
            foundationInstance.Position = BuildLayer.LocalToMap(buildMarker.GlobalPosition);
            GD.Print("Build Position: " + foundationInstance.Position);
            EmitSignal("BuildFoundation", foundationInstance);
        }
    }

    private void UpdateBuildMarkerPosition()
    {
        buildMarker.Position = direction switch
        {
            "Right" => new Vector2(MarkerDistance, 0),
            "Left" => new Vector2(-MarkerDistance, 0),
            "Up" => new Vector2(0, -MarkerDistance),
            "Down" => new Vector2(0, MarkerDistance),
            _ => buildMarker.Position
        };
    }

    private Vector2 GetVector()
    {
        return Input.GetVector("moveLeft", "moveRight", "moveUp", "moveDown").Normalized() * Speed;
    }

    private string GetDirectionPressed()
    {
        if (Input.IsActionPressed("moveRight"))
            return "Right";
        else if (Input.IsActionPressed("moveLeft"))
            return "Left";
        else if (Input.IsActionPressed("moveUp"))
            return "Up";
        else if (Input.IsActionPressed("moveDown"))
            return "Down";
        return direction;
    }
}