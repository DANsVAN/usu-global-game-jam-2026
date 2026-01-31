using Godot;
using System;

public partial class GameBoard : TileMapLayer
{
	Node2D[,] gameState;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gameState = new Node2D[16,8];
		GD.Print(gameState);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void NextStep()
	{
		GD.Print("step");
		foreach (Node2D val in gameState)
		{
    	GD.Print(val + " ------ ") ; // Prints 1, 2, 3, 4, 5, 6
		}
	}
}
