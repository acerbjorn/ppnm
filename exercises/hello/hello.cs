class hello{
	static void Main(string[] args) {
		if (args.Length > 0) {
			foreach(string arg in args) {
				System.Console.Write($"Hello {arg}\n");
			}
		} else {
		System.Console.Write("Hello\n");
		}
	}
}
