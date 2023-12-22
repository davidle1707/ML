using Microsoft.JScript;
using Microsoft.JScript.Vsa;
using System;
using System.CodeDom.Compiler;
using System.Reflection;
using ML.Common.Log;

namespace ML.Common
{
	/// <summary>
	/// http://www.softwarerockstar.com/2007/02/jscript-eval-method-in-c/
	/// </summary>
	public class JScriptEvaluator
	{
		#region Singleton

		private readonly static Lazy<JScriptEvaluator> LazyInstance = new Lazy<JScriptEvaluator>(() => new JScriptEvaluator());

		public static JScriptEvaluator Instance
		{
			get { return LazyInstance.Value; }
		}

		#endregion

		private ILogger _log = LogManager.GetLogger(typeof (JScriptEvaluator));

		private MethodInfo _jsMethodEval;

		public object Eval(string script, bool throwExceptionIfError = false)
		{
			try
			{
				if (_jsMethodEval == null)
				{
					_jsMethodEval = GetJsMethod("Eval");
				}

				return _jsMethodEval.Invoke(null, new object[] { script });
			}
			catch (Exception ex)
			{
				_log.Error(ex);

				if (throwExceptionIfError)
				{
					throw;	
				}
			}

			return null;
		}

		private MethodInfo GetJsMethod(string methodName)
		{
			const string jscriptEvalClass = @"
				import System;
				class JScriptEvaluator
				{
					public static function Eval(expression : String) : String
					{
						return eval(expression);
					}
				}";

			var compilerParams = new CompilerParameters
			{
				GenerateExecutable = false,
				GenerateInMemory = true
			};

			compilerParams.ReferencedAssemblies.Add("system.dll");

			var compiler = new JScriptCodeProvider();
			var compilerResults = compiler.CompileAssemblyFromSource(compilerParams, jscriptEvalClass);

			var evaluatorType = compilerResults.CompiledAssembly.GetType("JScriptEvaluator");

			return evaluatorType.GetMethod(methodName);
		}

		#region VsaEngine

		private VsaEngine _vsaEngine;

		[ObsoleteAttribute("Microsoft: Use of this VsaEngine is not recommended because it is being deprecated in Visual Studio 2005; there will be no replacement for this feature. Please see the ICodeCompiler documentation for additional help.")]
		public object EvalByVsaEngine(string script, bool throwExceptionIfError = false)
		{
			try
			{
				if (_vsaEngine == null)
				{
					_vsaEngine = VsaEngine.CreateEngine();	
				}
				
				return Microsoft.JScript.Eval.JScriptEvaluate(script, _vsaEngine);
			}
			catch (Exception ex)
			{
				_log.Error(ex);

				if (throwExceptionIfError)
				{
					throw;
				}
			}
		
			return null;
		}

		#endregion
	}
}
