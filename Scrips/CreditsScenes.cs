using Godot;
using System;

public partial class CreditsScenes : Node
{
    public override void _Ready()
    {
        ChangeToScene();
    }

    public async void ChangeToScene()
    {
        await ToSignal(GetTree().CreateTimer(5), "timeout");
         GetTree().ChangeSceneToFile("res://Scenes/Scenarios/menu.tscn");
    }
}
