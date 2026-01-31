using Godot;
using System;



public partial class Hud : CanvasLayer
{
	Label redLabel;
	Label blueLabel;
	GameManager gamemanger;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gamemanger = GetParent() as GameManager;
		redLabel = GetNode<Label>("red");
		blueLabel = GetNode<Label>("blue");
		redLabel.Text = "HP: " + gamemanger.red_teame_hp + " Money: " + gamemanger.red_teame_money;

		blueLabel.Text = "HP: " + gamemanger.blue_teame_hp + " Money: " + gamemanger.blue_teame_money;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		redLabel.Text = "HP: " + gamemanger.red_teame_hp + " Money: " + gamemanger.red_teame_money;

		blueLabel.Text = "HP: " + gamemanger.blue_teame_hp + " Money: " + gamemanger.blue_teame_money;
	}
}
