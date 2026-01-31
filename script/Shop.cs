using Godot;
using System;
using System.Security.Cryptography.X509Certificates;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;
public partial class Shop : CanvasLayer
{
	public GameManager gameManager;
	public int waf = 500;
	public int currentTogledId;
	public List<ShopStall> Children => GetChildren().OfType<ShopStall>().ToList();
	public Label childCost;

	public void ShopingStallTogler(bool toggle, int id)
	{
		currentTogledId = id;
		Children[id].isToggled = !Children[id].isToggled;
	}
	// Called when the node enters the scene tree for the first time.


	public void updateShop(PackedScene[] packedScenes)
	{	
		gameManager.selectedPiceType = null;
		foreach (ShopStall child in Children)
		{		
			PackedScene item = packedScenes[child.id];
			GD.Print(item);
			GD.Print(child.id);
			GD.Print(item.Instantiate<Piece>().cost);
			childCost = child.GetChild(0) as Label;
			childCost.Text = "Cost: " + item.Instantiate<Piece>().cost;
			if (child.id == currentTogledId){
				gameManager.selectedPiceType = item;
			}
			// 1. Get the node
			Piece newItem =  item.Instantiate<Piece>();
			Sprite2D sourceSprite = newItem.GetNode<Sprite2D>("Sprite2D");
			
			// // 2. Pull the texture into a variable
			Texture2D grabbedTexture = sourceSprite.Texture;
			Sprite2D ShopingStallSprite = child.GetChild(2) as Sprite2D;
			ShopingStallSprite.Texture = grabbedTexture;
			// // 3. Apply it to something else
			// GetNode<Sprite2D>("AnotherSprite").Texture = grabbedTexture;

		}
	}

	public override void _Ready()
	{
		int idCounter = 0;
        foreach (ShopStall child in Children)
        {
            child.id = idCounter;
			idCounter ++;
        }
        gameManager = GetParent() as GameManager;
        
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		foreach (ShopStall child in Children)
		{
			if(child.isToggled)
			{
				if(child.id != currentTogledId)
				{
					child.isToggled = !child.isToggled ;
				}
			}
		}
        updateShop(gameManager.next_5_cards);
	}
}
