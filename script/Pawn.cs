using Godot;
using System;

public partial class Pawn : Node2D
{
	int movement = 1;
	bool isRed;
	int damage = 1;
	int HP = 1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
