﻿using System.Numerics;

namespace Advent2017;

public partial class Day20 : Advent.Day
{
    public override void DoWork()
    {
        int closest = 0, iterations = 0;
        long time = 1000000;
        char[] splitter = ['p', 'v', 'a', ' ', '<', '>', '=', ','];
        // This is a bodge - run the simulation until we haven't had any collisions for a while and hope it's long enough to represent the final state.  (But turns out it only actually needed 10!)
        int maxIterations = 1000;
        Dictionary<int, Particle> particles = [];
        for (int i = 0; i < Inputs.Length; i++)
        {
            string[] digits = Inputs[i].Split(splitter, StringSplitOptions.RemoveEmptyEntries);
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
                Dictionary<Vector3, int> points = [];
                List<int> toDelete = [];
                foreach (Particle particle in particles.Values)
                {
                    if (toDelete.Contains(particle.Number)) continue;
                    particle.Move();
                    if (points.TryGetValue(particle.Position, out int value))
                    {
                        iterations = 0;
                        if (!toDelete.Contains(particle.Number)) toDelete.Add(particle.Number);
                        if (!toDelete.Contains(value)) toDelete.Add(value);
                    }
                    else
                        points.Add(particle.Position, particle.Number);
                }
                foreach (int delete in toDelete) particles.Remove(delete);
                if (toDelete.Count == 0) iterations++;
            } while (particles.Count > 1 && iterations < maxIterations);

        Output = (Part1 ? closest : particles.Count).ToString();
    }

    private sealed class Particle(int number, Vector3 position, Vector3 speed, Vector3 acceleration)
    {
        public int Number { get; private set; } = number;
        public Vector3 Position { get; private set; } = position;
        public Vector3 Speed { get; private set; } = speed;
        public Vector3 Acceleration { get; private set; } = acceleration;

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
