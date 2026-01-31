using Godot;
using System;

public partial class GameManager : Node2D
{
	public GameBoard Board;
	public EndScreen endScreen;
	public ColorRect endScreenColor;
	public Label endScreenText;
	public PackedScene selectedPiceType = null;
	public Shop shop;
	
	
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
	
	public PackedScene[] next_5_cards = new PackedScene[5];
	public PackedScene[] all_cards = new PackedScene[5];
	
	[Export] public PackedScene WorldScene;
	[Export] public PackedScene pice; // Drag your .tscn here in the Inspector
	[Export] public PackedScene pawn; // Drag your .tscn here in the Inspector
	[Export] public PackedScene castle; // Drag your .tscn here in the Inspector
	[Export] public PackedScene mine; // Drag your .tscn here in the Inspector
	// Called when the node enters the scene tree for the first time.
	// public void LoadWorld()
	// {
	// 	Node worldInstance = WorldScene.Instantiate();
	// 	AddChild(worldInstance);
	// }
	
	
	
	public void ShowEndScreen(String text)
	{
		endScreen.Visible = true;
		
		endScreenText.Text = text;
		if(text == "Blue Win Press Space To Play Again")
		{
			endScreenColor.Color =new Color(0.0f, 0.0f, 1.0f, 1.0f);
		}
		else if (text == "Red Win Press Space To Play Again")
		{
			endScreenColor.Color =new Color(1.0f, 0.0f, 0.0f, 1.0f);
		}
		else if (text == "Game Was A Tie Press Space To Play Again")
		{
			endScreenColor.Color =new Color(0.0f, 0.0f, 0.0f, 1.0f);
		}
		endScreenText.SelfModulate = Colors.Black;
	}
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
		endScreen = GetNode<EndScreen>("EndScreen");
        Board = GetNode<GameBoard>("gameBoard");
		endScreenText = endScreen.GetNode<Label>("Label");
		endScreenColor = endScreen.GetNode<ColorRect>("ColorRect");
		// shop = GetNode<Shop>("Shop");
		// 
		shop = GetChild(2) as Shop;
		// GD.Print(shop.currentTogledId);
        restart_game();
		// board = GetNode<TileMapLayer>("gameBoard");
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		if (game_is_over){
			if (blue_win&&!red_win){ // blue win
				GD.Print("Blue Win Press Space To Play Again");
				ShowEndScreen("Blue Win Press Space To Play Again");
			}
			if (red_win&&!blue_win){ // red win
				GD.Print("Red Win Press Space To Play Again");
				ShowEndScreen("Red Win Press Space To Play Again");
			}
			if (red_win&&blue_win){ // game was a tie
				GD.Print("Game Was A Tie Press Space To Play Again");
				ShowEndScreen("Game Was A Tie Press Space To Play Again");
			}
			
			if(Input.IsActionJustPressed("next turn")||(is_red_teame_turn && red_teame_money <= 0)||(is_blue_teame_turn && blue_teame_money <= 0)){
				restart_game();
			}
		}else{
			if (atack_time_left > 0){
				atack_time_left -= delta;
			}else if (!is_red_teame_turn && !is_blue_teame_turn){
				Board.kill_0_hp();
				Board.add_value();
				is_red_teame_turn = false;
				is_blue_teame_turn = true;
				blue_teame_money += 5;
				red_teame_money += 5;
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
							if (selectedPiceType != null){
								if (blue_teame_money >= selectedPiceType.Instantiate<Piece>().cost ){
									Board.gameState[next_spot[0],next_spot[1]] = Board.SpawnChild(selectedPiceType);
									Board.gameState[next_spot[0],next_spot[1]].init_data(1);
									Board.update_pos();
									blue_teame_money -= Board.gameState[next_spot[0],next_spot[1]].cost;
								}
							}
						}
					}
				}
			
			
				if (is_red_teame_turn) {
					int[] next_spot = FindPossibleRowAndCall();
					if (next_spot[0] < 16 && next_spot[0] >= 8 && next_spot[1] < 8 && next_spot[1] >= 0 ){
						if (!(Board.gameState[next_spot[0],next_spot[1]] is Piece)){
							if (selectedPiceType != null){
								if (red_teame_money >= selectedPiceType.Instantiate<Piece>().cost){
									Board.gameState[next_spot[0],next_spot[1]] = Board.SpawnChild(selectedPiceType);
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
		endScreen.Visible = false;
		
		selectedPiceType = castle;
		
		all_cards[0] =  pawn;
		all_cards[1] =  pawn;
		all_cards[2] =  pawn;
		all_cards[3] =  castle;
		all_cards[4] =  mine;
		get_5_rand_cards();
		
		Board.kill_all();
	}
	public PackedScene get_rand_card(){
		
		Random random = new Random();
		int randomIndex = random.Next(5);
		PackedScene ran = all_cards[randomIndex];
		return ran;
	}
	public void get_5_rand_cards(){
		next_5_cards[0] = get_rand_card();
		next_5_cards[1] = get_rand_card();
		next_5_cards[2] = get_rand_card();
		next_5_cards[3] = get_rand_card();
		next_5_cards[4] = get_rand_card();
	}
}
