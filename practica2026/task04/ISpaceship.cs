using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task04;

namespace task04
{
    public interface ISpaceship
    {
        void MoveForward();
        void Rotate(int angle);
        void Fire();
        int Speed { get; }
        int FirePower { get; }
    }

    public class Cruiser : ISpaceship
    {
        public Cruiser() {
            Speed = 50;
            FirePower = 100;
        }
        public void MoveForward() { }
        public void Rotate(int angle) { }
        public void Fire() { }
        public int Speed { get; }
        public int FirePower { get; }
    }

public class Fighter : ISpaceship
{
        public Fighter() {
            Speed = 100;
            FirePower = 50;
        }
    public void MoveForward() { }
    public void Rotate(int angle) { }
    public void Fire() { }
    public int Speed { get; }
    public int FirePower { get; }
    }
}
