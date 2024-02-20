using Godot;
using System;

public partial class MenuController : Node
{
    // Variable global
    private Global _global;

    public override void _Ready()
    {
        // Se inicializa la variable
        _global = GetNode<Global>("/root/Global");
    }
    public void OnPressed()
    {
        // Al precionar boton se almacenan los valores y se cambia de escena
        _global.pointsPlayer = 0;
        _global.lifePlayer = 3;

        GetTree().ChangeSceneToFile("res://Scenes/Scenarios/main.tscn");
    }
}
