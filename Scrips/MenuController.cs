using Godot;
using System;

public partial class MenuController : Node
{

    private Global _global;

    public override void _Ready()
    {
        _global = GetNode<Global>("/root/Global");
    }
    public void OnPressed()
    {
        _global.pointsPlayer = 0;
        _global.lifePlayer = 3;

        GetTree().ChangeSceneToFile("res://Scenes/Scenarios/main.tscn");
    }
}
