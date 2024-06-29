using System;

namespace ZooManager
{
    public class Cat : Animal
    {
        public bool catmoveUp = false;
        public bool catmoveDown = false;
        public bool catmoveLeft = false;
        public bool catmoveRight = false;
        public bool cathasMoved = false;
        public bool catisSitting = false;
        private int sittingTurns = 0;

        public Cat(string name)
        {
            emoji = "🐱";
            species = "cat";
            this.name = name;
            reactionTime = 1;
            pointDirection = "";
            CatStartDirection();
        }

        private void CatStartDirection()
        {
            Random random = new Random();
            int catstartDirection = random.Next(1, 5);
            switch (catstartDirection)
            {
                case 1:
                    catmoveUp = true;
                    pointDirection = "↑";
                    break;
                case 2:
                    catmoveDown = true;
                    pointDirection = "↓";
                    break;
                case 3:
                    catmoveLeft = true;
                    pointDirection = "←";
                    break;
                case 4:
                    catmoveRight = true;
                    pointDirection = "→";
                    break;
            }
        }

        public override void Activate()
        {
            if (!cathasMoved)
            {
                base.Activate();
                Console.WriteLine("I am a cat. Meow.");

                if (CatSitCheese())
                {
                    cathasMoved = true;
                    return;
                }
                if (CatSitHole())
                {
                    cathasMoved = true;
                    return;
                }



                if (!Hunt() && !CatFleeDog() && !CatEatMeat() && !CatJumpRock())
                {
                    CatMove();
                }
                //ensures only one movement
                cathasMoved = true;
            }

   
        }

        public void CatMove()
        {
            //Console.WriteLine($"MoveLeft: {moveLeft}, MoveUp: {moveUp}, MoveRight: {moveRight}, MoveDown: {moveDown}");
            if (catmoveLeft && Game.CatMoving(this, Direction.left))
            {
                return;
            }

            else if (catmoveUp && Game.CatMoving(this, Direction.up))
            {
                return;
            }

            else if (catmoveRight && Game.CatMoving(this, Direction.right))
            {
                return;
            }

            else if (catmoveDown && Game.CatMoving(this, Direction.down))
            {
                return;
            }

            else
            {
                SwitchDirection();
            }

            return;
        }

        public void SwitchDirection()
        {
            if (catmoveLeft)
            {
                catmoveLeft = false;
                catmoveRight = true;
                pointDirection = "→";
                Game.CatMoving(this, Direction.right);
                return;
            }
            else if (catmoveRight)
            {
                catmoveRight = false;
                catmoveLeft = true;
                pointDirection = "←";
                Game.CatMoving(this, Direction.left);
                return;
            }
            else if (catmoveUp)
            {
                catmoveUp = false;
                catmoveDown = true;
                pointDirection = "↓";
                Game.CatMoving(this, Direction.down);
                return;
            }
            else if (catmoveDown)
            {
                catmoveDown = false;
                catmoveUp = true;
                pointDirection = "↑";
                Game.CatMoving(this, Direction.up);
                return;
            }
        }

        public bool Hunt()
        {
            if (Game.Seek(location.x, location.y, Direction.up, "mouse") && catmoveUp == true)
            {
                Game.Attack(this, Direction.up);
                Game.gameStatus = "Game Over";
                Game.isGameOver = true;
                return true;

            }
            else if (Game.Seek(location.x, location.y, Direction.down, "mouse") && catmoveDown == true)
            {
                Game.Attack(this, Direction.down);
                Game.gameStatus = "Game Over";
                Game.isGameOver = true;
                return true;
            }
            else if (Game.Seek(location.x, location.y, Direction.left, "mouse") && catmoveLeft == true)
            {
                Game.Attack(this, Direction.left);
                Game.gameStatus = "Game Over";
                Game.isGameOver = true;
                return true;
            }
            else if (Game.Seek(location.x, location.y, Direction.right, "mouse") && catmoveRight == true)
            {
                Game.Attack(this, Direction.right);
                Game.gameStatus = "Game Over";
                Game.isGameOver = true;
                return true;
            }
            return false;
        }

        public bool CatSitCheese()
        {
            if (sittingTurns == 1)
            {
                sittingTurns--;
                return false;
            }


            if (Game.Seek(location.x, location.y, Direction.left, "cheese") ||
              Game.Seek(location.x, location.y, Direction.up, "cheese") ||
              Game.Seek(location.x, location.y, Direction.right, "cheese") ||
              Game.Seek(location.x, location.y, Direction.down, "cheese")) 
              {
               catisSitting = true;
               Game.CatMoving(this, Direction.none);
                sittingTurns = 1;
               return true;
               }
                return false;
        }

        public bool CatSitHole()
        {
            if (sittingTurns == 3)
            {
                catisSitting = true;
                Game.CatMoving(this, Direction.none);
                sittingTurns = 2;
                return true;
            }
            if (sittingTurns == 2)
            {
                catisSitting = true;
                Game.CatMoving(this, Direction.none);
                sittingTurns = 1;
                return true;
            }

            if (sittingTurns == 1)
            {
                sittingTurns--;
                return false;
            }


            if (Game.Seek(location.x, location.y, Direction.left, "hole") ||
              Game.Seek(location.x, location.y, Direction.up, "hole") ||
              Game.Seek(location.x, location.y, Direction.right, "hole") ||
              Game.Seek(location.x, location.y, Direction.down, "hole"))
            {
                catisSitting = true;
                Game.CatMoving(this, Direction.none);
                sittingTurns = 3;
                return true;
            }
            return false;
        }

        public bool CatEatMeat()
        {
            if (Game.Seek(location.x, location.y, Direction.up, "meat") && catmoveUp == true)
            {
                Game.Attack(this, Direction.up);
                Game.CatMovingTwoSpaces(this, Direction.up);
                return true;

            }
            else if (Game.Seek(location.x, location.y, Direction.down, "meat") && catmoveDown == true)
            {
                Game.Attack(this, Direction.down);
                Game.CatMovingTwoSpaces(this, Direction.down);
                return true;
            }
            else if (Game.Seek(location.x, location.y, Direction.left, "meat") && catmoveLeft == true)
            {
                Game.Attack(this, Direction.left);
                Game.CatMovingTwoSpaces(this, Direction.left);
                return true;
            }
            else if (Game.Seek(location.x, location.y, Direction.right, "meat") && catmoveRight == true)
            {
                Game.Attack(this, Direction.right);
                Game.CatMovingTwoSpaces(this, Direction.right);
                return true;
            }
            return false;
        }


        public bool CatFleeDog()
        {
            //look for dog
            if (catmoveUp && Game.Seek(location.x, location.y, Direction.up, "dog"))
            {
                Game.CatMoving(this, Direction.down);
                SwitchDirection();
                return true;
            }
            if (catmoveUp && Game.Seek(location.x, location.y - 1, Direction.up, "dog"))
            {
                Game.CatMoving(this, Direction.down);
                SwitchDirection();
                return true;
            }
            if (catmoveUp && Game.Seek(location.x, location.y - 2, Direction.up, "dog"))
            {
                Game.CatMoving(this, Direction.down);
                SwitchDirection();
                return true;
            }

            if (catmoveDown && Game.Seek(location.x, location.y, Direction.down, "dog"))
            {
                Game.CatMoving(this, Direction.up);
                SwitchDirection();
                return true;
            }
            if (catmoveDown && Game.Seek(location.x, location.y + 1, Direction.down, "dog"))
            {
                Game.CatMoving(this, Direction.up);
                SwitchDirection();
                return true;
            }
            if (catmoveDown && Game.Seek(location.x, location.y + 2, Direction.down, "dog"))
            {
                Game.CatMoving(this, Direction.up);
                SwitchDirection();
                return true;
            }


            if (catmoveLeft && Game.Seek(location.x, location.y, Direction.left, "dog"))
            {
                Game.CatMoving(this, Direction.right);
                SwitchDirection();
                return true;
            }

            if (catmoveLeft && Game.Seek(location.x - 1, location.y, Direction.left, "dog"))
            {
                Game.CatMoving(this, Direction.right);
                SwitchDirection();
                return true;
            }
            if (catmoveLeft && Game.Seek(location.x - 2, location.y, Direction.left, "dog"))
            {
                Game.CatMoving(this, Direction.right);
                SwitchDirection();
                return true;
            }


            if (catmoveRight && Game.Seek(location.x, location.y, Direction.right, "dog"))
            {
                Game.CatMoving(this, Direction.up);
                SwitchDirection();
                return true;
            }
            if (catmoveRight && Game.Seek(location.x + 1, location.y, Direction.right, "dog"))
            {
                Game.CatMoving(this, Direction.up);
                SwitchDirection();
                return true;
            }
            if (catmoveRight && Game.Seek(location.x + 2, location.y, Direction.right, "dog"))
            {
                Game.CatMoving(this, Direction.up);
                SwitchDirection();
                return true;
            }

            return false;
        }

        public bool CatJumpRock()
        {
            
            if (catmoveUp && Game.Seek(location.x, location.y, Direction.up, "rock"))
            {
                Game.CatMovingTwoSpaces(this, Direction.up);
                
                return true;
            }
         

            if (catmoveDown && Game.Seek(location.x, location.y, Direction.down, "rock"))
            {
                Game.CatMovingTwoSpaces(this, Direction.down);
                return true;
            }
            


            if (catmoveLeft && Game.Seek(location.x, location.y, Direction.left, "dog"))
            {
                Game.CatMovingTwoSpaces(this, Direction.left);
                return true;
            }

            


            if (catmoveRight && Game.Seek(location.x, location.y, Direction.right, "dog"))
            {
                Game.CatMovingTwoSpaces(this, Direction.right);
                return true;
            }
            

            return false;
        }


    }
}

