using Game.Scripts.Utils;

namespace Game.Scripts.DTO.Actor
{
    public class SurvivorDTO : ActorDTO
    {
        public string lastName { get; set; }

        public SurvivorDTO()
        {
            type = Enums.Actor.ActorType.HUMANOID;
            gender = RandomUtils.RandomEnum<Enums.Actor.ActorGender>();
            isAlive = true;
            currentHealth = maxHealth;
        }  
    }
}