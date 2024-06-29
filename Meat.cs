using System;
namespace ZooManager
{
    public class Meat : Occupant
    {
        public bool meatTryMove = false;
        public int stuckTurns = 0;
        public bool meatStuck = true;

        public Meat()
        {
            this.emoji = "🥩";
            this.species = "meat";
        }

        


        public override void OccupantActivate()
        {
            if (!meatTryMove)
            {
                base.OccupantActivate();
                Console.WriteLine("I am meat.");
                PushedByMouse();
                HuntForHole();
                StuckAtHole();
                CheeseCollision();
                RockCollision();
                meatTryMove = true;
            }
        }

        public bool PushedByMouse()
        {
            
            if (Game.Seek(location.x, location.y, Direction.up, "mouse") && (Game.mouseisMovingDown))
            {
                return Game.MeatPushed(this, Direction.down); 
            }
            if (Game.Seek(location.x, location.y, Direction.down, "mouse") && (Game.mouseisMovingUp))
            {
                return Game.MeatPushed(this, Direction.up); 
            }
            if (Game.Seek(location.x, location.y, Direction.left, "mouse") && (Game.mouseisMovingRight))
            {
                return Game.MeatPushed(this, Direction.right);
            }
            if (Game.Seek(location.x, location.y, Direction.right, "mouse") && (Game.mouseisMovingLeft))
            {
                return Game.MeatPushed(this, Direction.left);
            }
            return false;
        }

        public void HuntForHole()
        {
            if (Game.Seek(location.x, location.y, Direction.up, "hole"))
            {
                stuckTurns = 3;
                return;
                
            }
            else if (Game.Seek(location.x, location.y, Direction.down, "hole"))
            {
                stuckTurns = 3;
                return;
            }
            else if (Game.Seek(location.x, location.y, Direction.left, "hole"))
            {
                stuckTurns = 3;
                return;
            }
            else if (Game.Seek(location.x, location.y, Direction.right, "hole"))
            {
                stuckTurns = 3;
                return;
            }
        }

        public void StuckAtHole()
        {
            stuckTurns--;
            if(stuckTurns == 0)
            {
                this.emoji = null;
            }
            return;
            
        }

        public void CheeseCollision()
        {
            if (Game.Seek(location.x, location.y, Direction.up, "cheese") && (Game.meatisMovingUp))
            {
                Game.cheeseScore++;
                Game.cheeseScore++;
            }
            if (Game.Seek(location.x, location.y, Direction.down, "cheese") && (Game.meatisMovingDown))
            {
                Game.cheeseScore++;
                Game.cheeseScore++;
            }
            if (Game.Seek(location.x, location.y, Direction.left, "cheese") && (Game.meatisMovingLeft))
            {
                Game.cheeseScore++;
                Game.cheeseScore++;
            }
            if (Game.Seek(location.x, location.y, Direction.right, "cheese") && (Game.meatisMovingRight))
            {
                Game.cheeseScore++;
                Game.cheeseScore++;
            }

        }

        public void RockCollision()
        {
            if (meatStuck)
            {
                Game.MeatPushed(this, Direction.none);
            }

            if (Game.Seek(location.x, location.y, Direction.up, "rock") && (Game.meatisMovingUp))
            {
                meatStuck = true;
            }
            if (Game.Seek(location.x, location.y, Direction.down, "rock") && (Game.meatisMovingDown))
            {
                meatStuck = true;
            }
            if (Game.Seek(location.x, location.y, Direction.left, "rock") && (Game.meatisMovingLeft))
            {
                meatStuck = true;
            }
            if (Game.Seek(location.x, location.y, Direction.right, "rock") && (Game.meatisMovingRight))
            {
                meatStuck = true;
            }

        }




    }

	
}

