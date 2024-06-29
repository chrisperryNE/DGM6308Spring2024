using System;
using System.Collections.Generic;
using System.Reflection;

namespace ZooManager
{
    public static class Game
    {
        static public int numCellsX = 8;
        static public int numCellsY = 8;
        //static private int maxCellsX = 8;
        //static private int maxCellsY = 8;
        static public int cheeseScore = 0;
        static public bool isGameOver = false;
        static public string gameStatus = "Game Active";
        static public string fullDirections = "Navigate the mouse across the board, avoiding cats and dogs.  Pick up cheese to score points prior to escaping through the hole.  Score at least three points to win.";
        static public string turnDirections = "Place the mouse on the board.";
        static public int turnCounter = 0;

        static public bool mouseisMovingLeft = false;
        static public bool mouseisMovingRight = false;
        static public bool mouseisMovingUp = false;
        static public bool mouseisMovingDown = false;

        static public bool dogisMovingLeft = false;
        static public bool dogisMovingRight = false;
        static public bool dogisMovingUp = false;
        static public bool dogisMovingDown = false;

        static public bool meatisMovingLeft = false;
        static public bool meatisMovingRight = false;
        static public bool meatisMovingUp = false;
        static public bool meatisMovingDown = false;



        static public List<List<Zone>> animalZones = new List<List<Zone>>();
        static public Zone holdingPen = new Zone(-1, -1, null);

        static public void SetUpGame()
        {
            for (var y = 0; y < numCellsY; y++)
            {
                List<Zone> rowList = new List<Zone>();
                
                for (var x = 0; x < numCellsX; x++) rowList.Add(new Zone(x, y, null));
                animalZones.Add(rowList);
            }
        }


        static public void ZoneClick(Zone clickedZone)
        {
            //removed code that allowed animals from the zone to be removed from board
      
            if (holdingPen.occupant != null && clickedZone.occupant == null)
            {
                // put animal in zone from holding pen
                Console.WriteLine("Placing " + holdingPen.emoji);
                clickedZone.occupant = holdingPen.occupant;
                clickedZone.occupant.location = clickedZone.location;
                holdingPen.occupant = null;
                Console.WriteLine("Empty spot now holds: " + clickedZone.emoji);
                ProcessTurn();

            }
            else if (holdingPen.occupant != null && clickedZone.occupant != null)
            {
                Console.WriteLine("Could not place animal.");
                // Don't activate animals since user didn't get to do anything
            }
        }

        static public void AddToHolding(string occupantType)
        {
            if (holdingPen.occupant != null) return;
            if (occupantType == "cat") holdingPen.occupant = new Cat("Fluffy");
            if (occupantType == "mouse") holdingPen.occupant = new Mouse("Squeaky");
            if (occupantType == "dog") holdingPen.occupant = new Dog("Woofy");
            if (occupantType == "cheese") holdingPen.occupant = new Cheese();
            if (occupantType == "hole") holdingPen.occupant = new Hole();
            if (occupantType == "meat") holdingPen.occupant = new Meat();
            if (occupantType == "rock") holdingPen.occupant = new Rock();

            Console.WriteLine($"Holding pen occupant at {holdingPen.occupant.location.x},{holdingPen.occupant.location.y}");
        }

        static public void ProcessTurn()
        {
            ResetAnimalMoves();
            Directions();
            ActivateObjects();
            ActivateAnimals();
            turnCounter++;
        }

        static public void ResetAnimalMoves()
        {
            for (var y = 0; y < numCellsY; y++)
            {
                for (var x = 0; x < numCellsX; x++)
                {
                    if (animalZones[y][x].occupant is Mouse mouse)
                    {
                        mouse.mouseMovedOneSpace = false;
                        //mouse.mouseHasMoved = false;
                    }
                    if (animalZones[y][x].occupant is Cat cat)
                    {
                        cat.cathasMoved = false;
                    }
                    if (animalZones[y][x].occupant is Dog dog)
                    {
                        dog.doghasMoved = false;
                    }
                    if (animalZones[y][x].occupant is Meat meat)
                    {
                        meat.meatTryMove = false;
                    }
                }
            }
        }

        static public void ActivateAnimals()
        {
            for (var r = 1; r < 11; r++) // reaction times from 1 to 10
            {
                for (var y = 0; y < numCellsY; y++)
                {
                    for (var x = 0; x < numCellsX; x++)
                    {
                        var zone = animalZones[y][x];
                        if (zone.occupant as Animal != null && ((Animal)zone.occupant).reactionTime == r)
                        {
                            ((Animal)zone.occupant).Activate();
                        }
                    }
                }
            }
        }

        static public void ActivateObjects()
        {
                for (var y = 0; y < numCellsY; y++)
                {
                    for (var x = 0; x < numCellsX; x++)
                    {
                        var zone = animalZones[y][x];
                        if (zone.occupant as Occupant != null)
                        {
                        ((Occupant)zone.occupant).OccupantActivate();
                        }
                    }
                }
        }


        

        static public bool Seek(int x, int y, Direction d, string target)
        {
            switch (d)
            {
                case Direction.up:
                    y--;
                    break;
                case Direction.down:
                    y++;
                    break;
                case Direction.left:
                    x--;
                    break;
                case Direction.right:
                    x++;
                    break;
            }
            if (y < 0 || x < 0 || y > numCellsY - 1 || x > numCellsX - 1) return false;
            if (animalZones[y][x].occupant == null) return false;
            if (animalZones[y][x].occupant.species == target)
            {
                return true;
            }
            return false;
        }

        static public bool SeekTwoSpaces(int x, int y, Direction d, string target)
        {
            switch (d)
            {
                case Direction.up:
                    y--;
                    y--;
                    break;
                case Direction.down:
                    y++;
                    y++;
                    break;
                case Direction.left:
                    x--;
                    x--;
                    break;
                case Direction.right:
                    x++;
                    x++;
                    break;
            }
            if (y < 0 || x < 0 || y > numCellsY - 1 || x > numCellsX - 1) return false;
            if (animalZones[y][x].occupant == null) return false;
            if (animalZones[y][x].occupant.species == target)
            {
                return true;
            }
            return false;
        }





        static public void Attack(Animal attacker, Direction d)
        {
            Console.WriteLine($"{attacker.name} is attacking {d.ToString()}");
            int x = attacker.location.x;
            int y = attacker.location.y;

            switch (d)
            {
                case Direction.up:
                    animalZones[y - 1][x].occupant = attacker;
                    break;
                case Direction.down:
                    animalZones[y + 1][x].occupant = attacker;
                    break;
                case Direction.left:
                    animalZones[y][x - 1].occupant = attacker;
                    break;
                case Direction.right:
                    animalZones[y][x + 1].occupant = attacker;
                    break;
            }
            animalZones[y][x].occupant = null;
        }

        //may add directions for each turn
        static public void Directions()
        {
            if (turnCounter == 0)
            {
                turnDirections = "Place the mouse on the board.";
            }
            if (turnCounter == 1)
            {
                turnDirections = "Place the hole on the board.";
            }

            //2, 4, 8, 10, 14, etc...
            if (turnCounter % 2 == 0 && turnCounter % 3 != 0)
            {
                turnDirections = "Place cheese on the board.";
            }

            //3, 6, 9, etc...
            if (turnCounter % 3 == 0)
            {
                turnDirections = "Place an object on the board.";
            }
            //5, 7, 11
            if (turnCounter > 0 && turnCounter % 2 != 0 && turnCounter % 3 != 0)
            {
                turnDirections = "Place a cat or dog on the board.";
            }

            

        }

        static public bool MouseMoving(Mouse mouse, Direction d)
        {
            Console.WriteLine($"{mouse.name} is moving {d.ToString()}");
            int x = mouse.location.x;
            int y = mouse.location.y;

            switch (d)
            {
                case Direction.left:
                    if (x > 0 && animalZones[y][x - 1].occupant == null)
                    {
                        animalZones[y][x - 1].occupant = mouse;
                        animalZones[y][x].occupant = null;
                        mouseisMovingLeft = true;
                        mouseisMovingRight = false;
                        mouseisMovingUp = false;
                        mouseisMovingDown = false;

                        //mouse.mouseHasMoved = true;
                        return true;
                    }
                    break;

                case Direction.up:
                    if (y > 0 && animalZones[y - 1][x].occupant == null)
                    {
                        animalZones[y - 1][x].occupant = mouse;
                        animalZones[y][x].occupant = null;
                        mouseisMovingLeft = false;
                        mouseisMovingRight = false;
                        mouseisMovingUp = true;
                        mouseisMovingDown = false;
                        //mouse.mouseHasMoved = true;
                        return true;
                    }
                    break;


                case Direction.right:
                    if (x < numCellsX - 1 && animalZones[y][x + 1].occupant == null)
                    {
                        animalZones[y][x + 1].occupant = mouse;
                        animalZones[y][x].occupant = null;
                        mouseisMovingLeft = false;
                        mouseisMovingRight = true;
                        mouseisMovingUp = false;
                        mouseisMovingDown = false;
                        //mouse.mouseHasMoved = true;
                        return true;
                    }
                    break;

                case Direction.down:
                    if (y < numCellsY - 1 && animalZones[y + 1][x].occupant == null)
                    {
                        animalZones[y + 1][x].occupant = mouse;
                        animalZones[y][x].occupant = null;
                        mouseisMovingLeft = false;
                        mouseisMovingRight = false;
                        mouseisMovingUp = false;
                        mouseisMovingDown = true;
                        //mouse.mouseHasMoved = true;
                        return true;
                    }
                    break;
            }
            return false; // fallback
        }

        static public bool MouseRunning(Mouse mouse, Direction d)
        {
            Console.WriteLine($"{mouse.name} is running {d.ToString()}");
            int x = mouse.location.x;
            int y = mouse.location.y;

            bool ran = false;
            bool spaceToRun = true;
            while (spaceToRun)
            {
                switch (d)
                {
                    case Direction.left:
                        if (x > 0 && animalZones[y][x - 1].occupant == null)
                        {
                            animalZones[y][x - 1].occupant = mouse;
                            animalZones[y][x].occupant = null;
                            x--;
                            ran = true;
                            mouse.mouseHasMoved = true;
                        }
                        else
                        {
                            spaceToRun = false;
                        }
                        break;

                    case Direction.up:
                        if (y > 0 && animalZones[y - 1][x].occupant == null)
                        {
                            animalZones[y - 1][x].occupant = mouse;
                            animalZones[y][x].occupant = null;
                            y--;
                            ran = true;
                            mouse.mouseHasMoved = true;
                        }
                        else
                        {
                            spaceToRun = false;
                        }
                        break;

                    case Direction.right:
                        if (x < numCellsX - 1 && animalZones[y][x + 1].occupant == null)
                        {
                            animalZones[y][x + 1].occupant = mouse;
                            animalZones[y][x].occupant = null;
                            x++;
                            ran = true;
                            mouse.mouseHasMoved = true;
                        }
                        else
                        {
                            spaceToRun = false;
                        }
                        break;

                    case Direction.down:
                        if (y < numCellsY - 1 && animalZones[y + 1][x].occupant == null)
                        {
                            animalZones[y + 1][x].occupant = mouse;
                            animalZones[y][x].occupant = null;
                            y++;
                            ran = true;
                            mouse.mouseHasMoved = true;
                        }
                        else
                        {
                            spaceToRun = false;

                        }
                        break;


                }
            }
            if (ran == true)
            {
                mouse.location.x = x;
                mouse.location.y = y;

            }

            return true; // fallback
        }

        static public bool CatMoving(Animal cat, Direction d)
        {
            Console.WriteLine($"{cat.name} is moving {d.ToString()}");
            int x = cat.location.x;
            int y = cat.location.y;

            switch (d)
            {
                case Direction.left:
                    if (x > 0 && animalZones[y][x - 1].occupant == null)
                    {
                        animalZones[y][x - 1].occupant = cat;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    break;

                case Direction.up:
                    if (y > 0 && animalZones[y - 1][x].occupant == null)
                    {
                        animalZones[y - 1][x].occupant = cat;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    break;


                case Direction.right:
                    if (x < numCellsX - 1 && animalZones[y][x + 1].occupant == null)
                    {
                        animalZones[y][x + 1].occupant = cat;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    break;

                case Direction.down:
                    if (y < numCellsY - 1 && animalZones[y + 1][x].occupant == null)
                    {
                        animalZones[y + 1][x].occupant = cat;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    break;
                case Direction.none:
                    
                    return true;
            }
            return false; // fallback
        }

        static public bool CatMovingTwoSpaces(Animal cat, Direction d)
        {
            Console.WriteLine($"{cat.name} is moving {d.ToString()}");
            int x = cat.location.x;
            int y = cat.location.y;

            switch (d)
            {
                case Direction.left:
                    if (x > 1 && animalZones[y][x - 2].occupant == null)
                    {
                        animalZones[y][x - 2].occupant = cat;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    break;

                case Direction.up:
                    if (y > 1 && animalZones[y - 2][x].occupant == null)
                    {
                        animalZones[y - 2][x].occupant = cat;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    break;


                case Direction.right:
                    if (x < numCellsX - 2 && animalZones[y][x + 2].occupant == null)
                    {
                        animalZones[y][x + 2].occupant = cat;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    break;

                case Direction.down:
                    if (y < numCellsY - 2 && animalZones[y + 2][x].occupant == null)
                    {
                        animalZones[y + 2][x].occupant = cat;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    break;
                case Direction.none:

                    return true;
            }
            return false; // fallback
        }

        

        static public bool MeatPushed(Occupant meat, Direction d)
        {
            Console.WriteLine($"{meat} is moving {d.ToString()}");
            int x = meat.location.x;
            int y = meat.location.y;

            switch (d)
            {
                case Direction.left:
                    if (x > 0 && animalZones[y][x - 1].occupant == null)
                    {
                        animalZones[y][x - 1].occupant = meat;
                        animalZones[y][x].occupant = null;
                        meatisMovingLeft = true;
                        return true;
                    }
                    break;

                case Direction.up:
                    if (y > 0 && animalZones[y - 1][x].occupant == null)
                    {
                        animalZones[y - 1][x].occupant = meat;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    break;


                case Direction.right:
                    if (x < numCellsX - 1 && animalZones[y][x + 1].occupant == null)
                    {
                        animalZones[y][x + 1].occupant = meat;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    break;

                case Direction.down:
                    if (y < numCellsY - 1 && animalZones[y + 1][x].occupant == null)
                    {
                        animalZones[y + 1][x].occupant = meat;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    break;
                case Direction.none:

                    return true;
            }
            return false; // fallback
        }

        static public bool DogMoving(Animal dog, Direction d)
        {
            Console.WriteLine($"{dog.name} is moving {d.ToString()}");
            int x = dog.location.x;
            int y = dog.location.y;

            switch (d)
            {
                case Direction.left:
                    if (x > 0 && animalZones[y][x - 1].occupant == null)
                    {
                        animalZones[y][x - 1].occupant = dog;
                        animalZones[y][x].occupant = null;
                        dogisMovingLeft = true;
                        dogisMovingRight = false;
                        dogisMovingUp = false;
                        dogisMovingDown = false;
                        return true;
                    }
                    break;

                case Direction.up:
                    if (y > 0 && animalZones[y - 1][x].occupant == null)
                    {
                        animalZones[y - 1][x].occupant = dog;
                        animalZones[y][x].occupant = null;
                        dogisMovingLeft = false;
                        dogisMovingRight = false;
                        dogisMovingUp = true;
                        dogisMovingDown = false;
                        return true;
                    }
                    break;


                case Direction.right:
                    if (x < numCellsX - 1 && animalZones[y][x + 1].occupant == null)
                    {
                        animalZones[y][x + 1].occupant = dog;
                        animalZones[y][x].occupant = null;
                        dogisMovingLeft = false;
                        dogisMovingRight = true;
                        dogisMovingUp = false;
                        dogisMovingDown = false;
                        return true;
                    }
                    break;

                case Direction.down:
                    if (y < numCellsY - 1 && animalZones[y + 1][x].occupant == null)
                    {
                        animalZones[y + 1][x].occupant = dog;
                        animalZones[y][x].occupant = null;
                        dogisMovingLeft = false;
                        dogisMovingRight = false;
                        dogisMovingUp = false;
                        dogisMovingDown = true;
                        return true;
                    }
                    break;
                case Direction.none:
                        
                        return true;
                    
                    
            }
            return false; // fallback
        }

        static public bool RockPushed(Occupant rock, Direction d)
        {
            Console.WriteLine($"{rock} is moving {d.ToString()}");
            int x = rock.location.x;
            int y = rock.location.y;

            switch (d)
            {
                case Direction.left:
                    if (x > 0 && animalZones[y][x - 1].occupant == null)
                    {
                        animalZones[y][x - 1].occupant = rock;
                        animalZones[y][x].occupant = null;
                        
                        return true;
                    }
                    break;

                case Direction.up:
                    if (y > 0 && animalZones[y - 1][x].occupant == null)
                    {
                        animalZones[y - 1][x].occupant = rock;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    break;


                case Direction.right:
                    if (x < numCellsX - 1 && animalZones[y][x + 1].occupant == null)
                    {
                        animalZones[y][x + 1].occupant = rock;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    break;

                case Direction.down:
                    if (y < numCellsY - 1 && animalZones[y + 1][x].occupant == null)
                    {
                        animalZones[y + 1][x].occupant = rock;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    break;
            }
            return false; // fallback
        }

        



        static public string mouseInteractions = "\nMouse to Cat: If the cat enters any adjacent space to the mouse, the player loses all points and the game is over." +
            "\nMouse to Dog: If the dog enters any adjacent space to the mouse, the is sent running as far as it can go across the board in the opposite direction.  If a dog catches a mouse, the player loses all points and the game is over." +
            "\nMouse to Cheese: If the mouse lands on the same space as cheese, cheese is eaten and score increases.  \n" +
            "\nMouse to Meat:  The mouse pushes the meat across the board\nMouse to Rock: When the mouse runs into a rock, they change direction, pivoting to the right.  For instance, if the mouse was traveling left across the screen and hit a rock, it would being moving up\nMouse to Hole: When the mouse runs into the hole, the player wins and the game is over\n";


    }
}

