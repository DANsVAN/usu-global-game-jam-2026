using Godot;
using System;

public partial class ShopStall : Control

{
	// Called when the node enters the scene tree for the first time.
public bool isToggled = false;
public TextureButton myButton;
public GameManager gameManager;
public Shop shop;
public int id;

bool MouseEntered = false;
		



	private void OnMouseEntered()
	{
		MouseEntered = true;
	}

	private void OnMouseExited()
	{
		MouseEntered = false;
	}

	public void updateBtn()
	{
		// GD.Print("Button was clicked! Id " + id);
		shop.ShopingStallTogler(isToggled, id);
	}
	public override void _Ready()
	{
		myButton = GetNode<TextureButton>("TextureButton");
		
		shop = GetParent() as Shop;
		gameManager = shop.GetParent() as GameManager;
		
		// Connect the signals to local functions
		myButton.MouseEntered += OnMouseEntered;
		myButton.MouseExited += OnMouseExited;
		myButton.SelfModulate = Colors.Black;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		// GD.Print("is_blue_teame_turn");
		if(MouseEntered == true)
		{
			myButton.SelfModulate = Colors.Black;
		}

		if(isToggled)
		{
			myButton.SelfModulate = Colors.Black;
		}
		else{
			if (gameManager.is_blue_teame_turn)
			{
				myButton.SelfModulate = Colors.Blue;
			}
			else if (gameManager.is_red_teame_turn)
			{
				myButton.SelfModulate = Colors.Red;
			}
			else
			{
				myButton.SelfModulate = Colors.Black;
			}
		}					
	}
}
