using Godot;
using System;

public partial class GameManager : Node2D
{
	public GameBoard Board;
	[Export] public PackedScene WorldScene;
	// Called when the node enters the scene tree for the first time.
	// public void LoadWorld()
	// {
	// 	Node worldInstance = WorldScene.Instantiate();
	// 	AddChild(worldInstance);
	// }
	public int[] FindPossibleRowAndCall()
	{
		Vector2 mousePos = GetGlobalMousePosition();
		int possibleRow =  (int) mousePos.X / 32;
		int possibleCol =  (int) mousePos.Y / 32;
		int [] possibleColAndRow = {possibleRow,possibleCol};
		return(possibleColAndRow);
	}

	public override void _Ready()
	{
        // LoadWorld();
        Board = GetNode<GameBoard>("gameBoard");
		// board = GetNode<TileMapLayer>("gameBoard");
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("next turn"))
		{
			GD.Print("pressed");
			Board.NextStep();
		}
		int[] possibleColAndRow = FindPossibleRowAndCall();
		GD.Print("row " + possibleColAndRow[0]+ " col " + possibleColAndRow[1]);
		;
	}
}
