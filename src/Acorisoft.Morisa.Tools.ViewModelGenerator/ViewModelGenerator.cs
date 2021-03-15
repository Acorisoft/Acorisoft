using Microsoft.CodeAnalysis;
using System;

namespace Acorisoft.Morisa.Tool.ViewModelBinder
{
    [Generator]
    public class ViewModelGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            //
            // 定位
            var syntaxTrees = context.Compilation.SyntaxTrees;
            foreach(var syntaxTree in syntaxTrees)
            {
                if (syntaxTree.FilePath.Contains("App.cs"))
                {
                    ProcessSyntaxTree(syntaxTree);
                }
            }
        }

        protected virtual void ProcessSyntaxTree(SyntaxTree syntaxTree)
        {
            
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
