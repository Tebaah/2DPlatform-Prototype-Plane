using Godot;
using System;

public partial class PlayerController : CharacterBody2D
{
    // Variables
    [Export] public int speed;
    public Vector2 screenSize;

    // Methods

    public override void _Ready()
    {
        screenSize = GetViewportRect().Size;
    }

    public override void _PhysicsProcess(double delta)
    {
        GetInput();
        MoveAndSlide();
    }

    public void GetInput()
    {
        Vector2 inputDirection = Input.GetVector("left","right","up","down");
        Velocity = inputDirection * speed;
        Position = new Vector2(
            x: Mathf.Clamp(Position.X, 0, screenSize.X),
            y: Mathf.Clamp(Position.Y, 0, screenSize.Y));
    }   
}
