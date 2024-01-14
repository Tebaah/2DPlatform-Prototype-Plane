using Godot;
using System;

public partial class PlayerController : CharacterBody2D
{
    // Variables
    [Export] public int speed;
    public Vector2 screenSize;
    public static PackedScene bullet {get;} = GD.Load<PackedScene>("res://Scenes/Objects/bullet.tscn");
    // Methods

    public override void _Ready()
    {
        screenSize = GetViewportRect().Size;
    }

    public override void _PhysicsProcess(double delta)
    {
        GetInput();
        MoveAndSlide();

        if(Input.IsActionJustPressed("space"))
        {
            Shoot();
        }
    }

    public void GetInput()
    {
        // Obtenemos la direccion del movimiento
        Vector2 inputDirection = Input.GetVector("left","right","up","down"); 

        // Aplicamos la velocidad
        Velocity = inputDirection * speed; 

        // Nos movemos al nuevo vector 
        Position = new Vector2(
            x: Mathf.Clamp(Position.X, 0, screenSize.X),
            y: Mathf.Clamp(Position.Y, 0, screenSize.Y));

    }

    public void Shoot()
    {
        GD.Print("Shooting!!!");
    }   
}
