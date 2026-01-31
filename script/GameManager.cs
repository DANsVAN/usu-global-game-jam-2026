using Godot;
using System;

public partial class GameManager : Node2D
{
	public GameBoard Board;
	
	public bool is_blue_teame_turn = true;
	public int blue_teame_money = 5;
	public int blue_teame_hp = 10;
	
	public bool is_red_teame_turn = false;
	public int red_teame_money = 5;
	public int red_teame_hp = 10;
	
	public bool is_atack_veiw = false;
	public double atack_time_left = 1;
	
	public bool game_is_over=false;
	public bool red_win=false;
	public bool blue_win=false;
	
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
        restart_game();
		// board = GetNode<TileMapLayer>("gameBoard");
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		if (game_is_over){
			if (blue_win&&!red_win){ // blue win
				GD.Print("blue win");
			}
			if (red_win&&!blue_win){ // red win
				GD.Print("red win");
			}
			if (red_win&&blue_win){ // game was a tie
				GD.Print("game was a tie");
			}
			
			if(Input.IsActionJustPressed("next turn")||(is_red_teame_turn && red_teame_money <= 0)||(is_blue_teame_turn && blue_teame_money <= 0)){
				restart_game();
			}
		}else{
			if (atack_time_left > 0){
				atack_time_left -= delta;
			}else if (!is_red_teame_turn && !is_blue_teame_turn){
				Board.kill_0_hp();
				is_red_teame_turn = false;
				is_blue_teame_turn = true;
				blue_teame_money = 5;
				red_teame_money = 5;
			}
			if (blue_teame_hp <= 0 ){
				red_win = true;
				game_is_over = true;
			}
			if (red_teame_hp <= 0 ){
				blue_win = true;
				game_is_over = true;
			}
			if(Input.IsActionJustPressed("next turn")||(is_red_teame_turn && red_teame_money <= 0)||(is_blue_teame_turn && blue_teame_money <= 0)){
				// GD.Print("pressed");
				
				if (is_blue_teame_turn) {
					is_red_teame_turn = true;
					is_blue_teame_turn = false;
				}else if (is_red_teame_turn){
					is_blue_teame_turn = false;
					is_red_teame_turn = false;
					atack_time_left = 1;
					Board.NextStep();
				}
			}
			int[] possibleColAndRow = FindPossibleRowAndCall();
			// GD.Print("row " + possibleColAndRow[0]+ " col " + possibleColAndRow[1]);
			if(Input.IsActionJustPressed("left_click")){
				if (is_blue_teame_turn) {
					int[] next_spot = FindPossibleRowAndCall();
					if (next_spot[0] < 8 && next_spot[0] >= 0 && next_spot[1] < 8 && next_spot[1] >= 0 ){
						if (!(Board.gameState[next_spot[0],next_spot[1]] is Piece)){
							if (blue_teame_money >= 1){
								Board.gameState[next_spot[0],next_spot[1]] = Board.SpawnChild();
								Board.gameState[next_spot[0],next_spot[1]].init_data(1);
								Board.update_pos();
								blue_teame_money -= Board.gameState[next_spot[0],next_spot[1]].cost;
							}
						}
					}
				}
			
			
				if (is_red_teame_turn) {
					int[] next_spot = FindPossibleRowAndCall();
					if (next_spot[0] < 16 && next_spot[0] >= 8 && next_spot[1] < 8 && next_spot[1] >= 0 ){
						if (!(Board.gameState[next_spot[0],next_spot[1]] is Piece)){
							if (red_teame_money >= 1){
								Board.gameState[next_spot[0],next_spot[1]] = Board.SpawnChild();
								Board.gameState[next_spot[0],next_spot[1]].init_data(-1);
								Board.update_pos();
								red_teame_money -= Board.gameState[next_spot[0],next_spot[1]].cost;
							}
						}
					}
				}
			}
		}
	}
	public void restart_game(){
		is_blue_teame_turn = true;
		blue_teame_money = 5;
		blue_teame_hp = 10;
	
		is_red_teame_turn = false;
		red_teame_money = 5;
		red_teame_hp = 10;
	
		is_atack_veiw = false;
		atack_time_left = 1;
		
		game_is_over=false;
		red_win=false;
		blue_win=false;
		
		Board.kill_all();
	}
}
