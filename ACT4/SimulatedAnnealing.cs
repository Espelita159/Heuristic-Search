using System;
using System.Collections.Generic;

namespace ACT4
{
    class SimulatedAnnealing
    {
        private const double InitialTemperature = 1000;
        private const double CoolingRate = 0.99;
        private const int MaxIterations = 1000;

        public FiveState Optimize(FiveState initialState)
        {
            Console.WriteLine("Simulated annealing is being used.");

            double temperature = InitialTemperature;
            FiveState currentState = new FiveState(initialState);
            FiveState bestState = new FiveState(currentState);
            double bestScore = ObjectiveFunction(bestState);

            Random rand = new Random();

            while (temperature > 1)
            {
                for (int i = 0; i < MaxIterations; i++)
                {
                    FiveState neighborState = GenerateNeighbor(currentState, rand);
                    double currentScore = ObjectiveFunction(currentState);
                    double neighborScore = ObjectiveFunction(neighborState);

                    if (neighborScore > currentScore || AcceptWorseSolution(currentScore, neighborScore, temperature, rand))
                    {
                        currentState = neighborState;

                        if (neighborScore > bestScore)
                        {
                            bestScore = neighborScore;
                            bestState = neighborState;
                        }
                    }
                }

                temperature *= CoolingRate;
            }

            return bestState;
        }

        private double ObjectiveFunction(FiveState state)
        {
            return state.Y[0] + state.Y[1] + state.Y[2] + state.Y[3] + state.Y[4];
        }

        private FiveState GenerateNeighbor(FiveState state, Random rand)
        {
            FiveState neighbor = new FiveState(state);
            int index = rand.Next(0, 5);
            neighbor.Y[index] += rand.Next(-1, 2);
            return neighbor;
        }

        private bool AcceptWorseSolution(double currentScore, double neighborScore, double temperature, Random rand)
        {
            double probability = Math.Exp((neighborScore - currentScore) / temperature);
            return rand.NextDouble() < probability;
        }

        internal SixState Optimize(SixState startState)
        {
            throw new NotImplementedException();
        }
    }
}
