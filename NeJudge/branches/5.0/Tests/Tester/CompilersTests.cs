using System;
using System.IO;
using System.Text;
using Castle.Windsor;
using Tester.Compilers;
using Xunit;
using System.Linq;

namespace Tester.Tests
{
	public class CompilersTests
	{
		public CompilersTests()
		{
			compilers = new CompilerFactory(new WindsorContainer());
			TestUtils.ClearExeDirectory();
		}

		[Fact]
		public void can_compile_cpp()
		{
			CompileFile("Spawn.cpp", "MSVC90", "sln.exe", Encoding.ASCII.GetBytes("MZ"));
		}

		[Fact]
		public void can_compile_cpp_with_non_cpp_extension()
		{
			CompileFile("Spawn.txt", "MSVC90", "sln.exe", Encoding.ASCII.GetBytes("MZ"));
		}

		[Fact]
		public void can_compile_java()
		{
			byte[] javaMagic = BitConverter.GetBytes(0xCAFEBABE);
			Array.Reverse(javaMagic);
			CompileFile(@"Java\Main.java", "Java", "Main.class", javaMagic);
		}

		[Fact]
		public void can_show_compilation_errors()
		{
			var compiler = compilers.Find("Microsoft Visual C++ 10.0");
			string testProgramPath = Path.Combine(TestUtils.TestProgramsDirectory, "CE.cpp");
			CompilationResult result = compiler.Compile(testProgramPath, TestUtils.ExeDirectory);
			Assert.False(result.Success);
			Assert.Contains("2124", result.CompilerOutput);
			Assert.Contains("3861", result.CompilerOutput);
			Console.WriteLine(result.CompilerOutput);
		}

		[Fact(Name = "ВНИМАНИЕ!!! Длинный тест, занимает 10 секунд.")]
		public void can_terminate_timing_out_compilation()
		{
			var compiler = new TimingOut();
			string testProgramPath = Path.Combine(TestUtils.TestProgramsDirectory, "Gcd.cs");
			var e = Assert.Throws<FailedActionException>(() => compiler.Compile(testProgramPath, TestUtils.ExeDirectory));
			Assert.Contains("TimeLimit", e.Message);
			Console.WriteLine(e.Message);
		}

		[Fact]
		public void can_compile_checker()
		{
			CompileFile(@"Checkers\hcmp.cpp", "MSVC90Testlib", "sln.exe", Encoding.ASCII.GetBytes("MZ"));
		}

		private void CompileFile(string fileName, string languageId, string outputFileName, byte[] magic)
		{
			var compiler = compilers.Find(languageId);
			string testProgramPath = Path.Combine(TestUtils.TestProgramsDirectory, fileName);
			CompilationResult result = compiler.Compile(testProgramPath, TestUtils.ExeDirectory);
			Console.WriteLine(result.CompilerOutput);
			Assert.True(result.Success);
			string exeFile = Path.Combine(TestUtils.ExeDirectory, outputFileName);
			AssertOutputHasMagic(exeFile, magic);
		}

		private static void AssertOutputHasMagic(string outputFile, byte[] magic)
		{
			Assert.True(File.Exists(outputFile));
			byte[] outputFileBytes = File.ReadAllBytes(outputFile);
			Assert.True(magic.SequenceEqual(outputFileBytes.Take(magic.Length)));
		}

		readonly CompilerFactory compilers;

		private class TimingOut : CompilerBase
		{
			protected override string SourceFileName
			{
				get { return "Something.txt"; }
			}

			protected override string OutputFileName
			{
				get { return "Something.txt"; }
			}

			protected override string CompilerExecutable
			{
				get { return "cmd.exe"; }
			}

			protected override string CompilerArguments
			{
				get { return ""; }
			}

			public override string Name
			{
				get { return "Timing-out compiler"; }
			}

			public override bool ShowToContestants
			{
				get { return false; }
			}

			protected override string[] BeforeCompilationCommands
			{
				get
				{
					return new[]
						{
							":loop",
							"goto loop",
						};
				}
			}
		}
	}
}