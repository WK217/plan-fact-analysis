namespace PlanFactAnalysis.Model
{
    /// <summary>
    /// Центр финансовой ответственности (ЦФО).
    /// </summary>
    public sealed class ResponsibilityCenter
    {
        public static ResponsibilityCenter Default { get; } = new ResponsibilityCenter (name: "Не задано");

        /// <summary>
        /// Название ЦФО.
        /// </summary>
        public string Name { get; set; }

        public bool IsDefault
        {
            get => Default == this;
        }

        public ResponsibilityCenter ( )
        {

        }

        public ResponsibilityCenter (string name)
        {
            Name = name;
        }

        public string GenerateSQLInsertQuery ( )
        {
            if (!IsDefault)
                return string.Format (@"INSERT INTO responsibility_center (name)
                    VALUES ('{0}')", Name);
            else
                return string.Format (@"INSERT INTO responsibility_center (id, name)
                    VALUES ('0', '{0}')", Name);
        }

        public override string ToString ( )
        {
            return Name;
        }
    }
}