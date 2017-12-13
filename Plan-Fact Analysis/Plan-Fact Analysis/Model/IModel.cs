using System.Collections.Generic;
using System.Data.SQLite;

namespace PlanFactAnalysis.Model
{
    public interface IModel
    {
        IList<ActualOperation> ActualOperations { get; }
        IList<BudgetItem> BudgetItems { get; }
        IList<MeasurementUnit> MeasurementUnits { get; }
        IList<PlannedOperation> PlannedOperations { get; }
        IList<ResponsibilityCenter> ResponsibilityCenters { get; }
        IList<Scenario> Scenarios { get; }
        IList<User> Users { get; }

        SQLiteConnection EstablishDBConnection (string fileName);
        void ExportConfiguration (SQLiteConnection DBconnection);
        void ImportConfiguration (SQLiteConnection DBconnection);
        void ImportConfiguration (string fileName);
        void ExportConfiguration (string fileName);
    }
}