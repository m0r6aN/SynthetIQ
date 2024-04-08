namespace SynthetIQ.Utility.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class BicepAttribute : Attribute
    {
        public string Route { get; set; }

        public BicepAttribute(string route)
        {
            Route = route;
        }
    }

    public class MethodSignatureInformation
    {
        public string MethodName { get; set; }
        public string ReturnType { get; set; }
        public List<(string parameterType, string parameterName)> Parameters { get; set; }
        public string Route { get; set; }
    }

    public class MethodSignatureParser
    {
        public MethodSignatureInformation ExtractMethodSignatureInformation(string code)
        {
            var tree = CSharpSyntaxTree.ParseText(code);
            var root = tree.GetRoot();
            var methodDeclaration = root.DescendantNodes().OfType<MethodDeclarationSyntax>().FirstOrDefault();
            if (methodDeclaration == null)
            {
                throw new System.Exception("No method declaration found in the code.");
            }

            var methodName = methodDeclaration.Identifier.ValueText;
            var returnType = methodDeclaration.ReturnType.ToString();
            var parameters = methodDeclaration.ParameterList.Parameters.Select(p => (p.Type.ToString(), p.Identifier.ValueText)).ToList();

            var bicepAttribute = methodDeclaration.AttributeLists.SelectMany(al => al.Attributes)
                .Where(a => a.Name.ToString() == nameof(BicepAttribute))
                .FirstOrDefault();

            if (bicepAttribute == null)
            {
                throw new System.Exception("No BicepAttribute found on the method.");
            }

            var route = bicepAttribute.ArgumentList.Arguments.First().Expression.ToString();

            return new MethodSignatureInformation
            {
                MethodName = methodName,
                ReturnType = returnType,
                Parameters = parameters,
                Route = route
            };
        }
    }

    public class BicepGenerator
    {
        public string GenerateBicepFile(MethodSignatureInformation methodSignatureInformation)
        {
            var bicep = new StringBuilder();

            bicep.Append($"parameter {methodSignatureInformation.MethodName}Name string = '{methodSignatureInformation.MethodName}'\n");
            bicep.Append("resource function 'Microsoft.Azure.WebJobs.Extensions.Http' = {\n");
            bicep.Append($"  name: {methodSignatureInformation.MethodName}Name\n");
            bicep.Append("  type: 'Microsoft.Web/functions'\n");
            bicep.Append("  properties:\n");
            bicep.Append("    bindings:\n");
            bicep.Append("    - name: req\n");
            bicep.Append($"      type: httpTrigger\n");
            bicep.Append($"      direction: in\n");
            bicep.Append($"      route: '{methodSignatureInformation.Route}'\n");
            bicep.Append("    - name: executionContext\n");
            bicep.Append("      type: functionContext\n");
            bicep.Append("      direction: in\n");
            foreach (var param in methodSignatureInformation.Parameters)
            {
                bicep.Append($"    - name: {param.parameterName}\n");
                bicep.Append($"      type: {param.parameterType}\n");
                bicep.Append("      direction: in\n");
            }
            bicep.Append("}\n");

            return bicep.ToString();
        }
    }
}