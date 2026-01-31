using Godot;
using System;

public partial class GameBoard : TileMapLayer
{
	Pawn[,] gameState;
	// Pawn pawn;
	// Called when the node enters the scene tree for the first time.
	[Export] public PackedScene pice; // Drag your .tscn here in the Inspector

	public Pawn SpawnChild()
	{
    	// 1. Instantiate (create a copy of the scene)
    	Pawn instance = pice.Instantiate<Pawn>();

    	// 2. Add it to the tree
    	AddChild(instance);
		return(instance);
	}
	public override void _Ready()
	{
		// pawn = GetNode<Pawn>("pawn");
		gameState = new Pawn[16,8];
		gameState[0,0] = SpawnChild();
		gameState[1,1] = SpawnChild();
		gameState[4,7] = SpawnChild();
		gameState[2,2] = SpawnChild();

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void NextStep()
	{
		// int row = 0;
		// int col = 0;
		// foreach (Pawn val in gameState)
		// {
		// 	val.SetPos(50.0f,50.0f);
    	// 	GD.Print(val + " ------ ") ; // Prints 1, 2, 3, 4, 5, 6
		// 	row ++;
		// 	if(row > 16)
		// 	{
		// 		col ++;
		// 		row = 0;
		// 	}
		// 	GD.Print(row + " " + col);
		// }
		for ( int col = 0; col < 8;  col ++){
			for (int row = 0; row < 16; row ++)
			{
				GD.Print(" --------" );
				GD.Print(row + " " + col);
				Pawn pice = gameState[row,col];
				// pice.SetPos(row * 32f,col * 32f);
				GD.Print("out");
				if (pice is Pawn)
				{
					GD.Print("in");
					pice.SetPos(32.0f * row + 16,32.0f * col + 16);
				}
			}
		}
	}
}
