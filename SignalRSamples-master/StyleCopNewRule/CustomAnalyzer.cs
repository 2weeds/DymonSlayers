using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleCopNewRule
{
    using StyleCop;
    using StyleCop.CSharp;
    [SourceAnalyzer(typeof(CsParser))]
    public class CustomAnalyzer : SourceAnalyzer
    {
        public override void AnalyzeDocument(CodeDocument document)
        {
            var csharpDocument = document as CsDocument;
            if (csharpDocument != null)
            {
                csharpDocument.WalkDocument(
                    new CodeWalkerElementVisitor<CustomAnalyzer>(this.VisitElement),
                    new CodeWalkerStatementVisitor<CustomAnalyzer>(this.VisitStatement),
                    new CodeWalkerExpressionVisitor<CustomAnalyzer>(this.VisitExpression),
                    this);
            }
        }
        private bool VisitElement(CsElement element, CsElement parentElement, CustomAnalyzer context)
        {
            if (element.ElementType == ElementType.Method)
            {
                int firstLineNumber = element.LineNumber;
                int lastLineNumer = firstLineNumber;

                foreach (var statement in element.ChildStatements)
                {
                    lastLineNumer = statement.LineNumber;
                }

                int numberOfLinesInMethod = lastLineNumer - firstLineNumber + 1;
                if (numberOfLinesInMethod > 10)
                {
                    context.AddViolation(element, "TooLongMethod", element.Declaration.Name,
                        numberOfLinesInMethod, 10);
                }
            }
            return true;
        }

        private bool VisitStatement(Statement statement, Expression parentExpression, Statement parentStatement, CsElement parentElement, CustomAnalyzer context)
        {
            // Add your code here.
            return true;
        }

        private bool VisitExpression(Expression expression, Expression parentExpression, Statement parentStatement, CsElement parentElement, CustomAnalyzer context)
        {
            // Add your code here.
            return true;
        }
    }
}
