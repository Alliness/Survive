namespace Game.Scripts
{
    public class Enums
    {
        
        public enum GameLayer
        {
            Game = 8,
            Gui = 9,
            Room = 10
            
        }
        
        public enum RoomSize
        {
            Room0 = 0,
            Room1 = 1,
            Room2 = 2,
            Room3 = 3,
            Room4 = 4
        }
        
        public enum RoomType
        {
            Hangar = 0,
            Elevator = 1,
            LivingRoom = 2,
            Canteen = 3,
            Engineering = 4,
            Storage = 5,
            RadioStation = 6,
            Clinic = 7,
            Library = 8
        }
        
        public enum GuiMode
        {
            Base = 1,
            Room = 2,
            Item = 3
        }
        
        public class Actor
        {
            public enum ActorGender
            {
                MALE, FEMALE
            }

            public enum ActorType
            {
                HUMANOID, CREATURE, BEAST
            }

            public enum ActorState
            {
                IDLE,BUSY,WORK,MOVE,ATTACK
            }
        }
        
    }
}