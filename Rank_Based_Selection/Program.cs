
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rank_Based_Selection
{
    class Program
    {
        static int[,] neighborhood;
        static void Main(string[] args)
        {
            neighborhood = new int[,] { { 0, 5, 7, 4, 15 },
                                        { 5, 0, 3, 4, 10 },
                                        { 7, 3, 0, 2, 7 },
                                        { 4, 4, 2, 0, 9 },
                                        { 15, 10, 7, 9, 0 } };

            List<int> list = new List<int> { 0, 1, 2, 3, 4 };

            int popsize = 0;
            double alfa = 0, rank_shuma = 0;

            Console.WriteLine("Please select input of alfa parameter: ");
            alfa = double.Parse(Console.ReadLine());

            Console.WriteLine("Input popsize:");
            int.TryParse(Console.ReadLine(), out popsize);


            for (int i = 1; i <= popsize; i++)
            {
                rank_shuma += Math.Pow(i, alfa);
            }

            Console.WriteLine("Rank sum: " + rank_shuma.ToString());

            var solutions = new List<List<int>>();

            for (int i = 0; i < popsize; i++)
            {
                solutions.Add(list.OrderBy(a => Guid.NewGuid()).ToList());
            }

            print(solutions);

            var fitness = new List<RankSelection>();

            int sum;
            for (int i = 0; i < solutions.Count; i++)
            {
                sum = GetTotalDistance(solutions[i]);
                            
                fitness.Add(new RankSelection(sum, i, 0.0, i));
            }

            fitness = fitness.OrderBy(obj => obj.Fitness).ToList();

            for (int i = 0; i < fitness.Count; i++)
            {
                fitness[i].Rank = popsize - i;
                fitness[i].Probability = Math.Pow(fitness[i].Rank, alfa) / rank_shuma;
                Console.WriteLine(fitness[i].ToString());
            }

            Console.ReadLine();
        }

        static void print(List<List<int>> arr)
        {
            var rowCount = arr.Count;
            var colCount = arr.Max(l => l.Count);

            Console.WriteLine("Solutions");
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                    Console.Write(String.Format("{0}\t", arr[row][col]));
                Console.WriteLine();
            }
        }

        static int GetTotalDistance(List<int> solution)
        {
            var sum = 0;

            //Sum distances between two adjacent cities form 0 to n-1
            for (int i = 0; i < solution.Count - 1; i++)
            {
                sum += GetDistance(solution[i], solution[i + 1]);
            }

            // Add to that sum distance of last item and the  first one
            sum += GetDistance(solution[solution.Count - 1], solution[0]);

            return sum;
        }

        static int GetDistance(int origin, int destination)
        {
            int distance = neighborhood[origin, destination];
            return distance;
        }
    }

    public class RankSelection
    {
        public int Fitness { get; set; }
        public int Rank { get; set; }
        public double Probability { get; set; }
        public int Solution { get; set; }

        public RankSelection(int fitness, int rank, double probability, int solution)
        {
            this.Fitness = fitness;
            this.Rank = rank;
            this.Probability = probability;
            this.Solution = solution;
        }

        public override string ToString()
        {
            return "Fitness: " + this.Fitness.ToString() + " Rank: " + this.Rank.ToString() + " Probability: " + this.Probability.ToString() + " Solution: " + this.Solution.ToString();
        }

    }


}