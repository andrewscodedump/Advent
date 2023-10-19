using System.Numerics;

namespace Advent2017;

public partial class Day20 : Advent.Day
{
    public override void DoWork()
    {
        int closest = 0, iterations = 0;
        long time = 1000000;
        // This is a bodge - run the simulation until we haven't had any collisions for a while and hope it's long enough to represent the final state.  (But turns out it only actually needed 10!)
        int maxIterations = 1000;
        Dictionary<int, Particle> particles = new();
        for (int i = 0; i < InputSplit.Length; i++)
        {
            string[] digits = InputSplit[i].Split(new char[] { 'p', 'v', 'a', ' ', '<', '>', '=', ',' }, StringSplitOptions.RemoveEmptyEntries);
            Particle particle = new(i, new Vector3(float.Parse(digits[0]), float.Parse(digits[1]), float.Parse(digits[2])), new Vector3(float.Parse(digits[3]), float.Parse(digits[4]), float.Parse(digits[5])), new Vector3(float.Parse(digits[6]), float.Parse(digits[7]), float.Parse(digits[8])));
            particles.Add(i, particle);
        }
        if (Part1)
        {
            foreach (Particle particle in particles.Values)
                if (particle.DistanceAtTime(time) < particles[closest].DistanceAtTime(time))
                    closest = particle.Number;
        }
        else
            do
            {
                Dictionary<Vector3, int> points = new();
                List<int> toDelete = new();
                foreach (Particle particle in particles.Values)
                {
                    if (toDelete.Contains(particle.Number)) continue;
                    particle.Move();
                    if (points.ContainsKey(particle.Position))
                    {
                        iterations = 0;
                        if (!toDelete.Contains(particle.Number)) toDelete.Add(particle.Number);
                        if (!toDelete.Contains(points[particle.Position])) toDelete.Add(points[particle.Position]);
                    }
                    else
                        points.Add(particle.Position, particle.Number);
                }
                foreach (int delete in toDelete) particles.Remove(delete);
                if (toDelete.Count == 0) iterations++;
            } while (particles.Count > 1 && iterations < maxIterations);

        Output = (Part1 ? closest : particles.Count).ToString();
    }

    private class Particle
    {

        public Particle(int number, Vector3 position, Vector3 speed, Vector3 acceleration)
        {
            Number = number;
            Position = position;
            Speed = speed;
            Acceleration = acceleration;
        }
        public int Number { get; private set; }
        public Vector3 Position { get; private set; }
        public Vector3 Speed { get; private set; }
        public Vector3 Acceleration { get; private set; }

        public void Move()
        {
            Speed = Vector3.Add(Speed, Acceleration);
            Position = Vector3.Add(Position, Speed);
        }

        public long DistanceAtTime(long time)
        {
            // d(t) = d(0) + ut + 1/2at^2
            return Math.Abs((long)Position.X + (long)(Speed.X * time) + ((long)Acceleration.X * time * time / 2))
                + Math.Abs((long)Position.X + (long)(Speed.Y * time) + ((long)Acceleration.Y * time * time / 2))
                + Math.Abs((long)Position.X + (long)(Speed.Z * time) + ((long)Acceleration.Z * time * time / 2));
        }
    }
}
