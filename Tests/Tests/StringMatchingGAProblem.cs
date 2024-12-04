

using System.Text;

namespace Tests;
internal class StringMatchingGAProblem
{
	private readonly int _startRange;
	private readonly int _endRange;

	public StringMatchingGAProblem(int startRange, int endRange)
	{
		this._startRange = startRange;
		this._endRange = endRange;
	}
	public IEnumerable<string> InitialPopulation(int popSize, int stringSize)
	{
		IList<string> population = new List<string>();
		for (int i = 0; i < popSize; i++)
		{
			StringBuilder s = new StringBuilder();
			for (int j = 0; j < stringSize; j++)
			{
				char c = (char)Random.Shared.Next(_startRange, _endRange);
				s.Append(c);
			}
			population.Add(s.ToString());
		}
		return population;
	}

	public IEnumerable<int> Evaluation(IEnumerable<string> strings, string targetString)
	{
		IList<int> counts = new List<int>();
		foreach (var str in strings)
		{
			int count = 0;
			for (int i = 0; i < targetString.Length; i++)
				if (str[i] == targetString[i])
					count++;
			counts.Add(count);
		}
		return counts;
	}

	public int Evaluation(string str, string targetString)
	{
		int count = 0;
		for (int i = 0; i < targetString.Length; i++)
		{
			if (targetString[i] == str[i])
				count++;
		}
		return count;
	}

	public IList<KeyValuePair<int, string>> Sort(IList<KeyValuePair<int, string>> population_with_fitness, bool reverse)
	{
		var arr = population_with_fitness.ToArray();
		Array.Sort(arr, new Compare());
		if (reverse)
			return population_with_fitness.Reverse().ToList();
		return arr.ToList();
	}

	public IEnumerable<string> Selection(KeyValuePair<int, string>[] population_with_fitness, int size, bool reverse)
	{

		Array.Sort(population_with_fitness, new Compare());
		if (reverse)
			return population_with_fitness.Reverse().Take(size).Select(p => p.Value);
		return population_with_fitness.Take(size).Select(p => p.Value);
	}

	public string[] CrossOver(string chromosome1, string chromosome2)
	{
		int point = Random.Shared.Next(0, chromosome1.Length);
		string child1 = chromosome1[0..(point + 1)] + chromosome2[(point + 1)..];
		string child2 = chromosome2[0..(point + 1)] + chromosome1[(point + 1)..];
		Console.WriteLine($"Point:{point + 1}");
		Console.WriteLine($"Child1:{child1}");
		Console.WriteLine($"Child2:{child2}");
		return new string[]
		{
			child1,
			child2
		};
	}

	public string Mutation(string chromosome)
	{
		char c = (char)Random.Shared.Next(_startRange, _endRange);
		int gene = Random.Shared.Next(0, chromosome.Length);//HELL
		string mutated = chromosome[0..gene] + c + chromosome[(gene + 1)..];
		Console.WriteLine($"Choosen Gene Number:{gene}");
		Console.WriteLine($"Choosen Character:{c}");
		Console.WriteLine($"Mutated Chromosome:{mutated}");
		return mutated;
	}

	private class Compare : IComparer<KeyValuePair<int, string>>
	{
		int IComparer<KeyValuePair<int, string>>.Compare(KeyValuePair<int, string> x, KeyValuePair<int, string> y)
		{
			if (x.Key < y.Key)
				return -1;
			else if (x.Key > y.Key)
				return 1;
			return 0;
		}
	}
}