using System;

using Microsoft.Owin.Hosting;

namespace Inversion.Demo.Katana {
	public class Program {
		static void Main(string[] args) {
			string url = "http://localhost:9000";
			string factoryName = "Microsoft.Owin.Host.HttpListener";
			using (Microsoft.Owin.Hosting.WebApp.Start<InversionStartup>(new StartOptions(url){ServerFactory = factoryName})) {
				Console.WriteLine("Now serving from: {0}", url);
				Console.WriteLine("Press [enter] to quit...");
				Console.ReadLine();

			}
		}
	}
}
