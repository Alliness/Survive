namespace Game.Scripts.DTO.Actor
{
    public class ActorDTO
    {
        public long uid { get; set; }
        public string name { get; set; }
        public ViewDTO view { get; set; }
        public Enums.Actor.ActorType type { get; set; }
        public Enums.Actor.ActorGender gender { get; set; }
        public Enums.Actor.ActorState state { get; set; }
        public bool isAlive { get; set; }
        public double maxHealth{ get; set; }
        public double currentHealth { get; set; }
    }
}