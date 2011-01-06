using System.Linq.Expressions;

namespace NoNHibernate
{
    public class BinaryExpressionParser
    {
        private readonly BinaryExpression _binaryExpression;

        public BinaryExpressionParser(BinaryExpression binaryExpression) {
            _binaryExpression = binaryExpression;
        }

        public string GetOperation()
        {
            return _binaryExpression.NodeType.ToString();
        }

        public string GetRight() {
            var value = _binaryExpression.Right.ToString();
            return RemoveQuotes(value);
        }

        private string RemoveQuotes(string value) {
            return value.Replace("\"", "");
        }

        public string GetLeft()
        {
            var value = _binaryExpression.Left.ToString();

            string variableName = value.Split('.')[0];
            return RemoveVariableName(value, variableName);
        }

        private string RemoveVariableName(string value, string variableName) {
            return value.Replace(variableName + ".", "");
        }
    }
}