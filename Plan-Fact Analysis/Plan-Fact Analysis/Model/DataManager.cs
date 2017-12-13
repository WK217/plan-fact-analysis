using System.Windows;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using Xceed.Wpf.Toolkit;

namespace PlanFactAnalysis.Model
{
    public sealed class DataManager : IModel
    {
        #region Collections
        readonly IList<User> _users = new List<User> ( );
        public IList<User> Users => _users;

        readonly IList<MeasurementUnit> _measurementUnits = new List<MeasurementUnit> ( )
        {
            MeasurementUnit.Default
        };
        public IList<MeasurementUnit> MeasurementUnits => _measurementUnits;

        readonly IList<BudgetItem> _budgetItems = new List<BudgetItem> ( )
        {
            BudgetItem.Default
        };
        public IList<BudgetItem> BudgetItems => _budgetItems;

        readonly IList<ResponsibilityCenter> _responsibilityCenters = new List<ResponsibilityCenter> ( )
        {
            ResponsibilityCenter.Default
        };
        public IList<ResponsibilityCenter> ResponsibilityCenters => _responsibilityCenters;

        readonly IList<Scenario> _scenarios = new List<Scenario> ( )
        {
            Scenario.Default
        };
        public IList<Scenario> Scenarios => _scenarios;

        readonly IList<PlannedOperation> _plannedOperations = new List<PlannedOperation> ( );
        public IList<PlannedOperation> PlannedOperations => _plannedOperations;

        readonly IList<ActualOperation> _actualOperations = new List<ActualOperation> ( );
        public IList<ActualOperation> ActualOperations => _actualOperations;
        #endregion

        public DataManager ( )
        {

        }

        public DataManager (string DBfileName)
        {
            ImportConfiguration (DBfileName);
        }

        public SQLiteConnection EstablishDBConnection (string DBfileName)
        {
            if (ApproveFileName (DBfileName))
            {
                SQLiteConnection connection = new SQLiteConnection ("Data Source=" + DBfileName + ";Version=3;");
                return connection;
            }

            throw new FileFormatException (message: "", sourceUri: new Uri (Path.GetFullPath (DBfileName), UriKind.Absolute));
        }

        public void ImportConfiguration (string DBfileName)
        {
            if (ApproveFileName (DBfileName))
                ImportConfiguration (EstablishDBConnection (DBfileName));
        }

        public void ImportConfiguration (SQLiteConnection DBconnection)
        {            
            DBconnection.Open ( );

            #region Пользователи
            Users.Clear ( );
            using (SQLiteCommand command = new SQLiteCommand (@"SELECT * FROM user ORDER BY id", DBconnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader ( ))
                {
                    while (reader.Read ( ))
                    {
                        User newItem = new User (
                            name:       reader.GetString (4),
                            login:      reader.GetString (1),
                            passHashed: reader.GetString (2),
                            hashSalt:   reader.GetString (3),
                            role:       (UserRole)reader.GetInt32 (5));

                        Users.Add (newItem);
                    }
                }
            }
            #endregion

            #region Единицы измерения
            MeasurementUnits.Clear ( );
            MeasurementUnits.Add (MeasurementUnit.Default);

            using (SQLiteCommand command = new SQLiteCommand (@"SELECT * FROM measurement_unit WHERE id > 0 ORDER BY id", DBconnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader ( ))
                {
                    while (reader.Read ( ))
                    {
                        if (reader.GetInt32 (0) != 0)
                        {
                            MeasurementUnit newItem = new MeasurementUnit (
                            name: reader.GetString (1),
                            designation: reader.GetString (2));

                            MeasurementUnits.Add (newItem);
                        }
                    }
                }
            }

            #endregion

            #region Статьи бюджета
            BudgetItems.Clear ( );
            BudgetItems.Add (BudgetItem.Default);

            using (SQLiteCommand command = new SQLiteCommand (@"SELECT * FROM budget_item WHERE id > 0 ORDER BY id", DBconnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader ( ))
                {
                    while (reader.Read ( ))
                    {
                        if (reader.GetInt32 (0) != 0)
                        {
                            BudgetItem newItem = new BudgetItem (
                            name: reader.GetString (1),
                            measurementUnit: MeasurementUnits[reader.GetInt32 (2)]);

                            BudgetItems.Add (newItem);
                        }
                    }
                }
            }
            #endregion

            #region ЦФО
            ResponsibilityCenters.Clear ( );
            ResponsibilityCenters.Add (ResponsibilityCenter.Default);

            using (SQLiteCommand command = new SQLiteCommand (@"SELECT * FROM responsibility_center WHERE id > 0 ORDER BY id", DBconnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader ( ))
                {
                    while (reader.Read ( ))
                    {
                        if (reader.GetInt32 (0) != 0)
                        {
                            ResponsibilityCenter newItem = new ResponsibilityCenter (
                                name: reader.GetString (1));

                            ResponsibilityCenters.Add (newItem);
                        }
                    }
                }
            }
            #endregion

            #region Сценарии
            Scenarios.Clear ( );
            Scenarios.Add (Scenario.Default);

            using (SQLiteCommand command = new SQLiteCommand (@"SELECT * FROM scenario WHERE id > 0 ORDER BY id", DBconnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader ( ))
                {
                    while (reader.Read ( ))
                    {
                        if (reader.GetInt32 (0) != 0)
                        {
                            Scenario newItem = new Scenario (
                            name: reader.GetString (1));

                            Scenarios.Add (newItem);
                        }
                    }
                }
            }
            #endregion

            #region Плановые операции
            PlannedOperations.Clear ( );
            using (SQLiteCommand command = new SQLiteCommand (@"SELECT * FROM planned_operation ORDER BY id", DBconnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader ( ))
                {
                    while (reader.Read ( ))
                    {
                        IList<PlannedOperationScenario> plannedOperationScenarios = new List<PlannedOperationScenario> ( );

                        using (SQLiteCommand scenariosCommand = new SQLiteCommand (@"SELECT * FROM planned_operation_scenario WHERE planned_operation_id = " + reader.GetInt32 (0), DBconnection))
                        {
                            using (SQLiteDataReader scenariosReader = scenariosCommand.ExecuteReader ( ))
                            {
                                while (scenariosReader.Read ( ))
                                {
                                    PlannedOperationScenario plannedOperationScenario = new PlannedOperationScenario (
                                        scenario:           Scenarios[scenariosReader.GetInt32 (1)],
                                        value:              scenariosReader.GetDouble (2),
                                        labourIntensity:    scenariosReader.GetDouble (3));

                                    plannedOperationScenarios.Add (plannedOperationScenario);
                                }
                            }
                        }

                        PlannedOperation newItem = new PlannedOperation (
                            name: reader.GetString (1),
                            beginDate: new DateTime (reader.GetInt64 (2)),
                            endDate: new DateTime (reader.GetInt64 (3)),
                            budgetItem: BudgetItems[reader.GetInt32 (4)],
                            responsibilityCenter: ResponsibilityCenters[reader.GetInt32 (5)],
                            plannedOperationScenarios: plannedOperationScenarios);

                        PlannedOperations.Add (newItem);
                    }
                }
            }
            #endregion

            #region Фактические операции
            ActualOperations.Clear ( );
            using (SQLiteCommand command = new SQLiteCommand (@"SELECT * FROM actual_operation ORDER BY id", DBconnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader ( ))
                {
                    while (reader.Read ( ))
                    {
                        ActualOperation newItem = new ActualOperation (
                            name:               reader.GetString (1),
                            plannedOperation:   PlannedOperations[reader.GetInt32 (2) - 1],
                            date:               new DateTime(reader.GetInt64 (3)),
                            value:              reader.GetInt32 (4),
                            labourIntensity:    reader.GetInt32 (5));

                        ActualOperations.Add (newItem);
                    }
                }
            }
            #endregion

            DBconnection.Close ( );

            Xceed.Wpf.Toolkit.MessageBox.Show ("Импорт данных из файла успешно завершён.", "Импорт из файла", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
        }
        
        public void ExportConfiguration (string DBfileName)
        {
            if (!ApproveFileName (DBfileName))
                SQLiteConnection.CreateFile (DBfileName);

            ExportConfiguration (EstablishDBConnection (DBfileName));
        }

        public void ExportConfiguration (SQLiteConnection DBconnection)
        {
            DBconnection.Open ( );

            #region Пользователи
            new SQLiteCommand (@"DROP TABLE IF EXISTS user", DBconnection).ExecuteNonQuery ( );
            new SQLiteCommand (@"CREATE TABLE user (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, login TEXT NOT NULL UNIQUE, password_hash TEXT NOT NULL, salt TEXT NOT NULL, name TEXT NOT NULL, role_id INTEGER DEFAULT (1) NOT NULL)",
                DBconnection).ExecuteNonQuery ( );

            foreach (var user in Users)
                new SQLiteCommand (user.GenerateSQLInsertQuery ( ), DBconnection).ExecuteNonQuery ( );
            #endregion

            #region Единицы измерения
            new SQLiteCommand (@"DROP TABLE IF EXISTS measurement_unit", DBconnection).ExecuteNonQuery ( );
            new SQLiteCommand (@"CREATE TABLE measurement_unit (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, name TEXT NOT NULL UNIQUE, designation TEXT NOT NULL UNIQUE)",
                DBconnection).ExecuteNonQuery ( );

            foreach (var measurementUnit in MeasurementUnits)
                new SQLiteCommand (measurementUnit.GenerateSQLInsertQuery ( ), DBconnection).ExecuteNonQuery ( );
            #endregion

            #region Статьи бюджета
            new SQLiteCommand (@"DROP TABLE IF EXISTS budget_item", DBconnection).ExecuteNonQuery ( );
            new SQLiteCommand (@"CREATE TABLE budget_item (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, name TEXT NOT NULL, measurement_unit_id INTEGER DEFAULT (0), FOREIGN KEY (measurement_unit_id) REFERENCES measurement_unit (id) ON INSERT SET DEFAULT)",
                DBconnection).ExecuteNonQuery ( );

            foreach (var budgetItem in BudgetItems)
            {
                if (!budgetItem.IsDefault)
                    new SQLiteCommand (string.Format (@"INSERT INTO budget_item (name, measurement_unit_id)
                        VALUES ('{0}', '{1}')", budgetItem.Name, MeasurementUnits.IndexOf (budgetItem.MeasurementUnit)), DBconnection).ExecuteNonQuery ( );
                else
                    new SQLiteCommand (string.Format (@"INSERT INTO budget_item (id, name, measurement_unit_id)
                        VALUES ('0', '{0}', '{1}')", budgetItem.Name, MeasurementUnits.IndexOf (budgetItem.MeasurementUnit)), DBconnection).ExecuteNonQuery ( );
            }
            #endregion

            #region ЦФО
            new SQLiteCommand (@"DROP TABLE IF EXISTS responsibility_center", DBconnection).ExecuteNonQuery ( );
            new SQLiteCommand (@"CREATE TABLE responsibility_center (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL UNIQUE)",
                DBconnection).ExecuteNonQuery ( );

            foreach (var responsibilityCenter in ResponsibilityCenters)
                new SQLiteCommand (responsibilityCenter.GenerateSQLInsertQuery ( ), DBconnection).ExecuteNonQuery ( );
            #endregion

            #region Сценарии
            new SQLiteCommand (@"DROP TABLE IF EXISTS scenario", DBconnection).ExecuteNonQuery ( );
            new SQLiteCommand (@"CREATE TABLE scenario (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL UNIQUE)",
                DBconnection).ExecuteNonQuery ( );

            foreach (var scenario in Scenarios)
                new SQLiteCommand (scenario.GenerateSQLInsertQuery ( ), DBconnection).ExecuteNonQuery ( );
            #endregion

            #region Плановые операции
            new SQLiteCommand (@"DROP TABLE IF EXISTS planned_operation", DBconnection).ExecuteNonQuery ( );
            new SQLiteCommand (@"CREATE TABLE planned_operation (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL, begin_date_ticks BIGINT NOT NULL DEFAULT (0), end_date_ticks BIGINT NOT NULL DEFAULT (0), budget_item_id INTEGER NOT NULL DEFAULT (0), responsibility_center_id INTEGER NOT NULL DEFAULT (0), comment TEXT, FOREIGN KEY (budget_item_id) REFERENCES budget_item (id), FOREIGN KEY (responsibility_center_id) REFERENCES responsibility_center (id))",
                DBconnection).ExecuteNonQuery ( );

            new SQLiteCommand (@"DROP TABLE IF EXISTS planned_operation_scenario", DBconnection).ExecuteNonQuery ( );
            new SQLiteCommand (@"CREATE TABLE planned_operation_scenario (planned_operation_id INTEGER NOT NULL, scenario_id INTEGER NOT NULL, value DOUBLE NOT NULL DEFAULT (0), labour_intensity DOUBLE NOT NULL DEFAULT (0), FOREIGN KEY (scenario_id) REFERENCES scenario (id) ON DELETE CASCADE, FOREIGN KEY (planned_operation_id) REFERENCES planned_operation (id) ON DELETE CASCADE, PRIMARY KEY (planned_operation_id, scenario_id))",
                DBconnection).ExecuteNonQuery ( );

            foreach (var plannedOperation in PlannedOperations)
            {
                new SQLiteCommand (string.Format (@"INSERT INTO planned_operation (name, begin_date_ticks, end_date_ticks, budget_item_id, responsibility_center_id, comment)
                    VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                    plannedOperation.Name, plannedOperation.BeginDate.Ticks, plannedOperation.EndDate.Ticks,
                    BudgetItems.IndexOf (plannedOperation.BudgetItem), ResponsibilityCenters.IndexOf (plannedOperation.ResponsibilityCenter), string.Empty),
                    DBconnection).ExecuteNonQuery ( );

                foreach (var scenario in plannedOperation.Scenarios)
                {
                    new SQLiteCommand (string.Format (@"INSERT INTO planned_operation_scenario (planned_operation_id, scenario_id, value, labour_intensity)
                        VALUES ('{0}', '{1}', '{2}', '{3}')",
                        PlannedOperations.IndexOf (plannedOperation), Scenarios.IndexOf (scenario.Scenario), scenario.Value, scenario.LabourIntensity),
                        DBconnection).ExecuteNonQuery ( );
                }
            }
            #endregion

            #region Фактические операции
            new SQLiteCommand (@"DROP TABLE IF EXISTS actual_operation", DBconnection).ExecuteNonQuery ( );
            new SQLiteCommand (@"CREATE TABLE actual_operation (id INTEGER NOT NULL UNIQUE, name TEXT NOT NULL, planned_operation_id INTEGER NOT NULL, date_ticks BIGINT NOT NULL DEFAULT (0), value INTEGER NOT NULL DEFAULT (0), labour_intensity INTEGER NOT NULL DEFAULT (0), budget_item_id INTEGER NOT NULL DEFAULT (0), responsibility_center_id INTEGER NOT NULL DEFAULT (0), FOREIGN KEY (planned_operation_id) REFERENCES planned_operation (id) ON DELETE CASCADE, PRIMARY KEY (id), FOREIGN KEY (budget_item_id) REFERENCES budget_item (id), FOREIGN KEY (responsibility_center_id) REFERENCES responsibility_center (id))",
                DBconnection).ExecuteNonQuery ( );

            foreach (var actualOperation in ActualOperations)
            {
                new SQLiteCommand (string.Format (@"INSERT INTO actual_operation (name, planned_operation_id, date_ticks, value, labour_intensity, budget_item_id, responsibility_center_id)
                    VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
                    actualOperation.Name, PlannedOperations.IndexOf (actualOperation.PlannedOperation), actualOperation.Date.Ticks,
                    actualOperation.Value, actualOperation.LabourIntensity, BudgetItems.IndexOf (actualOperation.BudgetItem), ResponsibilityCenters.IndexOf (actualOperation.ResponsibilityCenter)),
                    DBconnection).ExecuteNonQuery ( );
            }
            
            #endregion

            DBconnection.Close ( );

            Xceed.Wpf.Toolkit.MessageBox.Show ("Экспорт данных в файл успешно завершён.", "Экспорт в файл", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
        }

        bool ApproveFileName(string DBfileName)
        {
            return File.Exists (Path.GetFullPath (DBfileName)) && Path.GetExtension (DBfileName) == ".pfa";
        }
    }
}
