using Godot;
using System;

public partial class GameBoard : TileMapLayer
{
	public GameManager gamemanger; 
	public Piece[,] gameState;
	// Pawn pawn;
	// Called when the node enters the scene tree for the first time.
	[Export] public PackedScene pice; // Drag your .tscn here in the Inspector

	public Piece SpawnChild(PackedScene pieceScene)
	{
    	// 1. Instantiate (create a copy of the scene)
    	Piece instance = pieceScene.Instantiate<Piece>();

    	// 2. Add it to the tree
    	AddChild(instance);
		return(instance);
	}
	public override void _Ready()
	{
		// pawn = GetNode<Pawn>("pawn");
		gameState = new Piece[16,8];
		// gameState[0,0] = SpawnChild();
		// gameState[0,0].init_data(1);
		// gameState[12,0] = SpawnChild();
		// gameState[12,0].init_data(-1);
		// gameState[4,7] = SpawnChild();
		// gameState[4,7].init_data(-1);
		// gameState[2,2] = SpawnChild();
		// gameState[2,2].init_data(1);
		// gameState[1,0] = SpawnChild();
		// gameState[1,0].init_data(1);

		update_pos();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void NextStep(){
		update_can_move();
		move_all_pices();
		do_attacks();
		add_value();
		update_pos();
		gamemanger.get_5_rand_cards();
		if (gamemanger.game_is_over)
		{
			gamemanger.restart_game();
		}
	}
	public void move_all_pices(){
		for ( int col = 0; col < 8;  col ++){
			for (int row = 0; row < 16; row ++){
				move_singl_pice(row, col,false);
			}
		}
	}
	public void move_singl_pice(int row, int col ,bool end_next){
		Piece pice = gameState[row,col];
		if (pice is Piece){	
			int[] next_spot = new int[2];
			next_spot[0] = (row+pice.get_move()[0]);
			next_spot[1] = (col+pice.get_move()[1]);
			if (row < 16 && row >= 0 && col < 8 && col >= 0 ){
				if (next_spot[0] < 16 && next_spot[0] >= 0 && next_spot[1] < 8 && next_spot[1] >= 0 ){
					if (pice.can_move){ 
						if (!(gameState[next_spot[0],next_spot[1]] is Piece)){
							gameState[next_spot[0],next_spot[1]] = pice;
							pice.can_move = false;
							gameState[row,col] = null;
						}else{
							if (gameState[next_spot[0],next_spot[1]]is Piece){	
								if (gameState[next_spot[0],next_spot[1]].can_move ){
									if (!pice.tryed_to_move){
										pice.tryed_to_move = true;
										move_singl_pice(next_spot[0], next_spot[1],false);
									}
									if (pice.can_move && !end_next) {
										move_singl_pice(row, col ,true);
									}
								}
							}
						}
					}
				}
			}
		}
	}
	public void update_pos(){
		for ( int col = 0; col < 8;  col ++){
			for (int row = 0; row < 16; row ++)
			{
				// GD.Print(" --------" );
				// GD.Print(row + " " + col);
				Piece pice = gameState[row,col];
				// pice.SetPos(row * 32f,col * 32f);
				if (pice is Piece)
				{	
					pice.SetPos(32.0f * row + 16,32.0f * col + 16);
				}
			}
		}
	}
	public void update_can_move(){
		for ( int col = 0; col < 8;  col ++){
			for (int row = 0; row < 16; row ++){
				Piece pice = gameState[row,col];
				// pice.SetPos(row * 32f,col * 32f);
				if (pice is Piece)
				{	
					pice.can_move = true;
					pice.tryed_to_move = false;
				}
			}
		}
	}
	public void do_attacks(){
		gamemanger = GetParent<GameManager>();
		GameManager parent = gamemanger;
		for ( int col = 0; col < 8;  col ++){
			for (int row = 0; row < 16; row ++){
				Piece pice = gameState[row,col];
				if (pice is Piece){	
					int[] atack_spot = new int[2];
					atack_spot[0] = (row+pice.get_atack()[0]);
					atack_spot[1] = (col+pice.get_atack()[1]);
					if (atack_spot[0] < 16 && atack_spot[0] >= 0 && atack_spot[1] < 8 && atack_spot[1] >= 0 ){
						if (gameState[atack_spot[0],atack_spot[1]] is Piece){
							if (gameState[atack_spot[0],atack_spot[1]].move_dir != pice.move_dir){
								gameState[atack_spot[0],atack_spot[1]].HP -= pice.damage;
								pice.HP -= gameState[atack_spot[0],atack_spot[1]].thorns;
							}
						}
					}else if(atack_spot[0] < 16 && atack_spot[0] >= -10 && atack_spot[1] < 8 && atack_spot[1] >= 0 ) { //blue is atacked
						parent.blue_teame_hp -= pice.damage;
						pice.HP -= 1;
					}else if(atack_spot[0] < 26 && atack_spot[0] >= 0 && atack_spot[1] < 8 && atack_spot[1] >= 0 ) { //red is atacked
						parent.red_teame_hp -= pice.damage;
						pice.HP -= 1;
					}
				}
			}
		}
	}
	public void kill_0_hp(){
		for ( int col = 0; col < 8;  col ++){
			for (int row = 0; row < 16; row ++){
				Piece pice = gameState[row,col];
				if (pice is Piece){
					if (pice.HP <= 0) {
						// GD.Print("0 HP");
						pice.QueueFree();
						gameState[row,col] = null;
					}
				}	
			}
		}
	}
	
	public void kill_all(){
		for ( int col = 0; col < 8;  col ++){
			for (int row = 0; row < 16; row ++){
				Piece pice = gameState[row,col];
				if (pice is Piece){
					pice.QueueFree();
					gameState[row,col] = null;
				}
			}
		}
	}
	
	public void add_value(){
		gamemanger = GetParent<GameManager>();
		GameManager parent = gamemanger;
		for ( int col = 0; col < 8;  col ++){
			for (int row = 0; row < 16; row ++){
				Piece pice = gameState[row,col];
				if (pice is Piece){
					if (pice.move_dir>0){
						parent.blue_teame_money += pice.add_value;
					}else{
						parent.red_teame_money += pice.add_value;
					}
				
				}
			}
		}
		GD.Print(parent.blue_teame_money);
	}
}
