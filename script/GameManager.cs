using Godot;
using System;

public partial class GameManager : Node2D
{
	public GameBoard Board;
	public bool is_blue_teame_turn = true;
	public int blue_teame_money = 5;
	public bool is_red_teame_turn = false;
	public int red_teame_money = 5;
	public bool is_atack_veiw = false;
	public double atack_time_left = 1;
	
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
		if (atack_time_left > 0){
			atack_time_left -= delta;
		}else if (!is_red_teame_turn && !is_blue_teame_turn){
			is_red_teame_turn = false;
			is_blue_teame_turn = true;
			blue_teame_money = 5;
			red_teame_money = 5;
		}
		if(Input.IsActionJustPressed("next turn")||(is_red_teame_turn && red_teame_money <= 0)||(is_blue_teame_turn && blue_teame_money <= 0))
		{
			GD.Print("pressed");
			
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
		GD.Print("row " + possibleColAndRow[0]+ " col " + possibleColAndRow[1]);
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
