namespace Tests
{

	public class Program
	{
		public static void Main()
		{
			Console.Write("Enter Target String:");
			string targetString = Console.ReadLine()?.ToUpper() ?? "";
			StringMatchingGAProblem gAProblem = new(' ', 'Z');
			var population = gAProblem.InitialPopulation(10, targetString.Length);
			IList<KeyValuePair<int, string>> keyValues = new List<KeyValuePair<int, string>>();
			var values = gAProblem.Evaluation(population, targetString);
			for (int i = 0; i < values.Count(); i++)
				keyValues.Add(new KeyValuePair<int, string>(values.ElementAt(i), population.ElementAt(i)));

			PrintKeyValues(keyValues);
			Console.WriteLine("===========================================");

			// start from here
			keyValues = Apply(gAProblem, targetString, keyValues, 1000);
			Console.WriteLine("Finall Result:-");
			keyValues = gAProblem.Sort(keyValues, true);
			Console.WriteLine(keyValues[0].Value + " " + keyValues[0].Key);
		}



		private static IList<KeyValuePair<int, string>> Apply(StringMatchingGAProblem gAProblem, string targetString, IList<KeyValuePair<int, string>> keyValues, int generations)
		{
			int generation = 0;
			while (generation++ < generations)
			{
				var selected = gAProblem.Selection(keyValues.ToArray(), 2, true);
				Console.WriteLine($"Selection G{generation}:-");
				Console.WriteLine(selected.ElementAt(0));
				Console.WriteLine(selected.ElementAt(1));
				Console.WriteLine("===========================================");
				Console.WriteLine($"Cross-Over G{generation}:-");
				var offSprings = gAProblem.CrossOver(selected.ElementAt(0), selected.ElementAt(1));
				Console.WriteLine("===========================================");
				Console.WriteLine($"Mutation G{generation}:-");
				var mutated = new string[]
				{
					gAProblem.Mutation(offSprings[0]),
					gAProblem.Mutation(offSprings[1])
				};
				var evaulations = new int[]
				{
					gAProblem.Evaluation(mutated[0],targetString),
					gAProblem.Evaluation(mutated[1],targetString)
				};
				var evaul1 = new KeyValuePair<int, string>(evaulations[0], mutated[0]);
				var evaul2 = new KeyValuePair<int, string>(evaulations[1], mutated[1]);
				keyValues.Add(evaul1);
				keyValues.Add(evaul2);
				keyValues = gAProblem.Sort(keyValues, false);
				keyValues.RemoveAt(0);
				keyValues.RemoveAt(0);
				Console.WriteLine("===========================================");
				Console.WriteLine($"Evaluation And Replacement G{generation}:-");
				PrintKeyValues(keyValues);
				Console.WriteLine("===========================================");
			}
			return keyValues;
		}

		private static void PrintKeyValues(IList<KeyValuePair<int, string>> keyValues)
		{
			for (int i = 0; i < keyValues.Count; i++)
				Console.WriteLine($"Solution{i}:{keyValues[i].Value} {keyValues[i].Key}");
		}
	}
}