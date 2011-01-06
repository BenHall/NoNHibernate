using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NoNHibernate
{
    public class Query<T>
    {
        private int _count;
        private string _tableName;
        List<string> where = new List<string>();

        public Query() {
            _tableName = typeof (T).Name;
        }

        public Query<T> Where(Expression<Func<T, bool>> expression) {
            var parser = new BinaryExpressionParser((BinaryExpression)expression.Body);

            string left = parser.GetLeft();
            string right = parser.GetRight();
            string op = parser.GetOperation();

            string sqlOp = TranslateOperationToSql(op);
            where.Add(string.Format("{0} {1} '{2}'", left, sqlOp, right));

            return this;
        }

        private string TranslateOperationToSql(string op) {
            if (op.Equals("Equal"))
                return "=";

            return string.Empty;
        }

        public Query<T> SetMaxResults(int count) {
            _count = count;
            return this;
        }

        public string SqlQuery() {
            string top = string.Empty;
            string whereClause = string.Empty;

            if (_count > 0)
                top = "TOP " + _count;

            if (where.Count > 0)
                whereClause = "WHERE " + where.First();


            return string.Format("SELECT {0} * FROM {1} {2}", top, _tableName, whereClause);
        }

        public static implicit operator string(Query<T> r)
        {
            return r.SqlQuery();
        }
    }
}