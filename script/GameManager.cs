using Godot;
using System;

public partial class GameManager : Node
{
	[Export] public PackedScene WorldScene;
	// Called when the node enters the scene tree for the first time.
	public void LoadWorld()
	{
		Node worldInstance = WorldScene.Instantiate();
		AddChild(worldInstance);
	}
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
