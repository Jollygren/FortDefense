using Godot;
using System;

public partial class Player : CharacterBody2D
{
    public int Speed = 300;
    private AnimatedSprite2D animatedSprite;
    private string keyHeld;

    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        keyHeld = "";
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        Vector2 velocity = Velocity;
        velocity = Input.GetVector("moveLeft", "moveRight", "moveUp", "moveDown");
        velocity = velocity.Normalized() * Speed;
        MoveAndCollide(velocity * (float)delta);

        if (keyHeld == "")
        {
            if (Input.IsActionPressed("moveRight"))
            {
                keyHeld = "Right";
                GD.Print("Right key held");
            }
            else if (Input.IsActionPressed("moveLeft"))
            {
                keyHeld = "Left";
                GD.Print("Left key held");
            }
            else if (Input.IsActionPressed("moveUp"))
            {
                keyHeld = "Up";
                GD.Print("Up key held");
            }
            else if (Input.IsActionPressed("moveDown"))
            {
                keyHeld = "Down";
                GD.Print("Down key held");
            }
        }
        else if (Input.IsActionJustReleased("move" + keyHeld))
        {
            keyHeld = "";
        }
        if (velocity != Vector2.Zero)
        {
            animatedSprite.Animation = "walk" + keyHeld;
            animatedSprite.Play();
        }
        else
        {
            string direction = animatedSprite.GetAnimation();
            if (!direction.Contains("idle"))
            {
                animatedSprite.Animation = direction.Replace("walk", "idle");
            }
            
        }
    }
}