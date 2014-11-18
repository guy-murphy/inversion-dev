using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ClariusLabs.NuDoc;

namespace Inversion.Documentation.Generator {
	public class Program {
		public static void Main(string[] args) {
			Console.WriteLine("Press [ENTER] to run harness code...");
			Console.ReadLine();
			Console.WriteLine("==================================================");

			string solution = args.Length > 0
				? args[0]
				: Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

			StringBuilder solutionReadme = new StringBuilder();
			StringBuilder solutionProjects = new StringBuilder();
			List<string> contents = new List<string>();
			string solutionMd = Path.Combine(solution, "solution.md");
			if (File.Exists(solutionMd)) solutionReadme.AppendLine(File.ReadAllText(solutionMd));

			foreach (string project in Directory.GetDirectories(solution)) {
				string projectName = project.Replace(solution, "").Substring(1);
				string apiXml = String.Format("{0}.xml", Path.Combine(solution, project, "bin", "debug", projectName));

				string apiMd = Path.Combine(solution, project, "api.md");
				string projectMd = Path.Combine(solution, project, "project.md");
				string projectReadme = Path.Combine(solution, project, "readme.md");

				string projectNotes = String.Empty;
				if (File.Exists(projectMd)) {
					projectNotes = File.ReadAllText(projectMd);
					solutionProjects.AppendLine();
					solutionProjects.AppendFormat(@"<a name=""{0}""></a>", projectName);
					solutionProjects.AppendLine();
					solutionProjects.AppendLine(projectNotes);
					solutionProjects.AppendLine();
					solutionProjects.AppendFormat(@"* [{0} project folder](./{0}/)", projectName);
					solutionProjects.AppendLine();

					contents.Add(projectName);
				}

				if (File.Exists(apiXml)) {
					DocumentMembers members = DocReader.Read(apiXml);
					MarkdownVisitor visitor = new MarkdownVisitor();
					visitor.VisitDocument(members);
					Console.WriteLine(apiMd);
					File.WriteAllText(apiMd, visitor.Markdown);
					File.WriteAllText(projectReadme, projectNotes + visitor.Markdown);
				}
			}

			// build table of contents for projects
			if (contents.Count > 0) {
				solutionReadme.AppendLine("\n\n## Project Docs");
				foreach (string project in contents) {
					solutionReadme.AppendLine(String.Format(@"* [{0}](#{0})", project));
				}
				solutionReadme.AppendLine();
			}

			solutionReadme.Append(solutionProjects);
			File.WriteAllText(Path.Combine(solution, "readme.md"), solutionReadme.ToString());

			Console.WriteLine("==================================================");
			Console.WriteLine("\nPress [ENTER] to close the harness console.");
			Console.ReadLine();
		}
	}
}
