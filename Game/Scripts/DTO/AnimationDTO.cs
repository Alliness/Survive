
namespace Game.Scripts.DTO
{
    public class AnimationDTO
    {
        public string idle { get; set; }
        public string move { get; set; }
        public string work { get; set; }
        public string attack { get; set; } //todo depends on weapon
        public string dying { get; set; }
    }
}