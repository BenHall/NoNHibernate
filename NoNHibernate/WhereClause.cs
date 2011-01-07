namespace NoNHibernate
{
    internal class WhereClause {
        public string Left { get; set; }
        public string Right { get; set; }
        public string Op { get; set; }

        public override string ToString() {
            return string.Format("{0} {1} @{0}", Left, Op);
        }
    }
}