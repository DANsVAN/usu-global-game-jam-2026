using Godot;
using System;



public partial class Hud : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Label redLabel = GetNode<Label>("red");
		Label blueLabel = GetNode<Label>("blue");
		redLabel.Text = "HP: 10 Money: 5";

		blueLabel.Text = "HP: 10 Money: 5";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
