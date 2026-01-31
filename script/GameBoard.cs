using Godot;
using System;

public partial class GameBoard : TileMapLayer
{
	Piece[,] gameState;
	// Pawn pawn;
	// Called when the node enters the scene tree for the first time.
	[Export] public PackedScene pice; // Drag your .tscn here in the Inspector

	public Piece SpawnChild()
	{
    	// 1. Instantiate (create a copy of the scene)
    	Piece instance = pice.Instantiate<Piece>();

    	// 2. Add it to the tree
    	AddChild(instance);
		return(instance);
	}
	public override void _Ready()
	{
		// pawn = GetNode<Pawn>("pawn");
		gameState = new Piece[16,8];
		gameState[0,0] = SpawnChild();
		gameState[0,0].init_data(true);
		gameState[1,1] = SpawnChild();
		gameState[1,1].init_data(true);
		gameState[4,7] = SpawnChild();
		gameState[4,7].init_data(false);
		gameState[2,2] = SpawnChild();
		gameState[2,2].init_data(false);

		update_pos();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void NextStep()
	{
		update_pos();
	}
	public void update_pos(){
				for ( int col = 0; col < 8;  col ++){
			for (int row = 0; row < 16; row ++)
			{
				GD.Print(" --------" );
				GD.Print(row + " " + col);
				Piece pice = gameState[row,col];
				// pice.SetPos(row * 32f,col * 32f);
				GD.Print("out");
				if (pice is Piece)
				{
					GD.Print("in");
					pice.SetPos(32.0f * row + 16,32.0f * col + 16);
				}
			}
		}
	}
}
