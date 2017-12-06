using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace PlanFactAnalysis.Model
{
    public sealed class DataManager
    {
        SQLiteConnection _DBconnection;

        public IList<User> Users { get; private set; }
        public IList<MeasurementUnit> MeasurementUnits { get; private set; }
        public IList<BudgetItem> BudgetItems { get; private set; }
        public IList<ResponsibilityCenter> ResponsibilityCenters { get; private set; }
        public IList<Scenario> Scenarios { get; private set; }
        public IList<PlannedOperation> PlannedOperations { get; private set; }
        public IList<ActualOperation> ActualOperations { get; private set; }

        public SQLiteConnection EstablishDBConnection (string fileName)
        {
            if (File.Exists (Path.GetFullPath (fileName)))
            {
                _DBconnection = new SQLiteConnection ("Data Source=" + fileName + ";Version=3;");

                return _DBconnection;
            }

            return null;
        }

        public Configuration GetDefaultConfiguration ( )
        {
            #region Пользователи
            Users = new List<User> ( )
            {
                new User("Имя планировщика", "planner", "1", UserRole.Planner),
                new User("Имя исполнителя", "executor", "2", UserRole.Executor),
                new User("Имя руководителя", "manager", "3", UserRole.Manager)
            };
            #endregion

            #region Единицы измерения
            MeasurementUnits = new List<MeasurementUnit> ( )
            {
                new MeasurementUnit("Рубли", "руб."),
                new MeasurementUnit("Штуки", "шт."),
                new MeasurementUnit("Литры", "л."),
                new MeasurementUnit("Тонны", "т."),
            };

            MeasurementUnit.Default = MeasurementUnits[0];
            #endregion

            #region Статьи бюджета
            BudgetItems = new List<BudgetItem> ( )
            {
                new BudgetItem("Не задано", MeasurementUnit.Default),
                new BudgetItem("Доходы от реализации продукции", MeasurementUnits[0]),
                new BudgetItem("Доходы от прочей деятельности", MeasurementUnits[0]),
                new BudgetItem("Прочее", MeasurementUnits[2]),
                new BudgetItem("Производство материала", MeasurementUnits[1]),
                new BudgetItem("Производство материала", MeasurementUnits[2]),
                new BudgetItem("Производство материала", MeasurementUnits[3]),
                new BudgetItem("Производственные расходы", MeasurementUnits[0]),
                new BudgetItem("Коммерческие расходы", MeasurementUnits[0]),
                new BudgetItem("Административные расходы", MeasurementUnits[0]),
                new BudgetItem("Налоги", MeasurementUnits[0])
            };

            BudgetItem.Default = BudgetItems[0];
            #endregion

            #region ЦФО
            ResponsibilityCenters = new List<ResponsibilityCenter> ( )
            {
                new ResponsibilityCenter("Не задано"),
                new ResponsibilityCenter("Центр затрат"),
                new ResponsibilityCenter("Центр дохода"),
                new ResponsibilityCenter("Центр валового дохода"),
                new ResponsibilityCenter("Центр прибыли"),
                new ResponsibilityCenter("Центр рентабельности инвестиций")
            };

            ResponsibilityCenter.Default = ResponsibilityCenters[0];
            #endregion

            #region Сценарии
            Scenarios = new List<Scenario> ( )
            {
                new Scenario("Пессимистичный"),
                new Scenario("Реалистичный"),
                new Scenario("Оптимистичный"),
            };

            Scenario.Default = Scenarios[1];
            #endregion

            #region Плановые операции
            PlannedOperations = new List<PlannedOperation> ( )
            {
                new PlannedOperation ("Пример операции #1", new DateTime(2016, 11, 27, 00, 00, 00), new DateTime(2017, 11, 27, 02, 05, 35),
                    BudgetItems[0], ResponsibilityCenters[1],
                    Scenarios[1], 200000),

                new PlannedOperation ("Пример операции #2", new DateTime(2016, 12, 11, 00, 00, 00), new DateTime(2017, 11, 30, 02, 05, 35),
                    BudgetItems[2], ResponsibilityCenters[3],
                    Scenarios[1], 400000),

                new PlannedOperation ("Пример операции #3", new DateTime(2016, 10, 09, 00, 00, 00), new DateTime(2017, 06, 20, 02, 05, 35),
                    BudgetItems[1], ResponsibilityCenters[1], 
                    Scenarios[1], 60000),

                new PlannedOperation ("Пример операции #4", new DateTime(2016, 05, 12, 00, 00, 00), new DateTime(2017, 10, 21, 02, 05, 35),
                    BudgetItems[0], ResponsibilityCenters[0],
                    Scenarios[1], 217000),
            };

            PlannedOperations[2].Scenarios.Add (new PlannedOperationScenario (Scenarios[0], 55000));
            PlannedOperations[2].Scenarios.Add (new PlannedOperationScenario (Scenarios[2], 70000));
            #endregion

            #region Фактические операции
            ActualOperations = new List<ActualOperation> ( )
            {
                new ActualOperation("Фактическая операция #1.1", new DateTime(2016, 12, 01, 00, 00, 00), PlannedOperations[0], 70000),
                new ActualOperation("Фактическая операция #1.2", new DateTime(2017, 01, 01, 00, 00, 00), PlannedOperations[0], 70000),
                new ActualOperation("Фактическая операция #1.3", new DateTime(2017, 02, 01, 00, 00, 00), PlannedOperations[0], 70000),
                
                new ActualOperation("Фактическая операция #2", new DateTime(2017, 05, 01, 00, 00, 00), PlannedOperations[1], 412000),

                new ActualOperation("Фактическая операция #3", new DateTime(2017, 06, 01, 00, 00, 00), PlannedOperations[2], 50000),

                new ActualOperation("Фактическая операция #4", new DateTime(2017, 04, 01, 00, 00, 00), PlannedOperations[3], 228000),
            };
            #endregion

            return new Configuration (Users, MeasurementUnits, BudgetItems, ResponsibilityCenters, Scenarios, PlannedOperations, ActualOperations);
        }

        public void ExportConfiguration ( )
        {
            _DBconnection.Open ( );

            //#region Пользователи
            //foreach (var item in Users)
            //{
            //    SQLiteCommand command = new SQLiteCommand (@"SELECT * FROM user ORDER BY id", _DBconnection);
            //}
            //#endregion

            //#region Единицы измерения
            //MeasurementUnits = new List<MeasurementUnit> ( );
            //using (SQLiteCommand command = new SQLiteCommand (@"SELECT * FROM measurement_unit ORDER BY id", _DBconnection))
            //{
            //    using (SQLiteDataReader reader = command.ExecuteReader ( ))
            //    {
            //        while (reader.Read ( ))
            //        {
            //            MeasurementUnit newItem = new MeasurementUnit (
            //                name: reader.GetString (1),
            //                designation: reader.GetString (2));

            //            if (reader.GetInt32 (0) == 0)
            //                MeasurementUnit.Default = newItem;

            //            MeasurementUnits.Add (newItem);
            //        }
            //    }
            //}

            //#endregion

            //#region Статьи бюджета
            //BudgetItems = new List<BudgetItem> ( );
            //using (SQLiteCommand command = new SQLiteCommand (@"SELECT * FROM budget_item ORDER BY id", _DBconnection))
            //{
            //    using (SQLiteDataReader reader = command.ExecuteReader ( ))
            //    {
            //        while (reader.Read ( ))
            //        {
            //            BudgetItem newItem = new BudgetItem (
            //                name: reader.GetString (1),
            //                measurementUnit: MeasurementUnits[reader.GetInt32 (2)]);

            //            if (reader.GetInt32 (0) == 0)
            //                BudgetItem.Default = newItem;

            //            BudgetItems.Add (newItem);
            //        }
            //    }
            //}
            //#endregion

            //#region ЦФО
            //ResponsibilityCenters = new List<ResponsibilityCenter> ( );
            //using (SQLiteCommand command = new SQLiteCommand (@"SELECT * FROM responsibility_center ORDER BY id", _DBconnection))
            //{
            //    using (SQLiteDataReader reader = command.ExecuteReader ( ))
            //    {
            //        while (reader.Read ( ))
            //        {
            //            ResponsibilityCenter newItem = new ResponsibilityCenter (
            //                name: reader.GetString (1));

            //            if (reader.GetInt32 (0) == 0)
            //                ResponsibilityCenter.Default = newItem;

            //            ResponsibilityCenters.Add (newItem);
            //        }
            //    }
            //}
            //#endregion

            //#region Сценарии
            //Scenarios = new List<Scenario> ( );
            //using (SQLiteCommand command = new SQLiteCommand (@"SELECT * FROM scenario ORDER BY id", _DBconnection))
            //{
            //    using (SQLiteDataReader reader = command.ExecuteReader ( ))
            //    {
            //        while (reader.Read ( ))
            //        {
            //            Scenario newItem = new Scenario (
            //                name: reader.GetString (1));

            //            if (reader.GetInt32 (0) == 0)
            //                Scenario.Default = newItem;

            //            Scenarios.Add (newItem);
            //        }
            //    }
            //}
            //#endregion

            //#region Плановые операции
            //PlannedOperations = new List<PlannedOperation> ( );

            //using (SQLiteCommand command = new SQLiteCommand (@"SELECT * FROM planned_operation ORDER BY id", _DBconnection))
            //{
            //    using (SQLiteDataReader reader = command.ExecuteReader ( ))
            //    {
            //        while (reader.Read ( ))
            //        {
            //            IList<PlannedOperationScenario> plannedOperationScenarios = new List<PlannedOperationScenario> ( );

            //            using (SQLiteCommand scenariosCommand = new SQLiteCommand (@"SELECT * FROM planned_operation_scenario WHERE planned_operation_id = " + reader.GetInt32 (0), _DBconnection))
            //            {
            //                using (SQLiteDataReader scenariosReader = scenariosCommand.ExecuteReader ( ))
            //                {
            //                    while (scenariosReader.Read ( ))
            //                    {
            //                        PlannedOperationScenario plannedOperationScenario = new PlannedOperationScenario (
            //                            scenario: Scenarios[scenariosReader.GetInt32 (1)],
            //                            value: scenariosReader.GetInt32 (2),
            //                            labourIntensity: scenariosReader.GetInt32 (3));

            //                        plannedOperationScenarios.Add (plannedOperationScenario);
            //                    }
            //                }
            //            }

            //            PlannedOperation newItem = new PlannedOperation (
            //                name: reader.GetString (1),
            //                beginDate: new DateTime (reader.GetInt64 (2)),
            //                endDate: new DateTime (reader.GetInt64 (3)),
            //                budgetItem: BudgetItems[reader.GetInt32 (4)],
            //                responsibilityCenter: ResponsibilityCenters[reader.GetInt32 (5)],
            //                plannedOperationScenarios: plannedOperationScenarios);

            //            PlannedOperations.Add (newItem);
            //        }
            //    }
            //}
            //#endregion

            //#region Фактические операции
            //ActualOperations = new List<ActualOperation> ( );
            //using (SQLiteCommand command = new SQLiteCommand (@"SELECT * FROM actual_operation ORDER BY id", _DBconnection))
            //{
            //    using (SQLiteDataReader reader = command.ExecuteReader ( ))
            //    {
            //        while (reader.Read ( ))
            //        {
            //            ActualOperation newItem = new ActualOperation (
            //                name: reader.GetString (1),
            //                plannedOperation: PlannedOperations[reader.GetInt32 (2) - 1],
            //                date: new DateTime (reader.GetInt64 (3)),
            //                value: reader.GetInt32 (4),
            //                labourIntensity: reader.GetInt32 (5));

            //            ActualOperations.Add (newItem);
            //        }
            //    }
            //}
            //#endregion

            _DBconnection.Close ( );
        }

        public Configuration GetConfiguration ( )
        {
            _DBconnection.Open ( );

            #region Пользователи
            Users = new List<User> ( );
            using (SQLiteCommand command = new SQLiteCommand (@"SELECT * FROM user ORDER BY id", _DBconnection))
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
            MeasurementUnits = new List<MeasurementUnit> ( );
            using (SQLiteCommand command = new SQLiteCommand (@"SELECT * FROM measurement_unit ORDER BY id", _DBconnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader ( ))
                {
                    while (reader.Read ( ))
                    {
                        MeasurementUnit newItem = new MeasurementUnit (
                            name: reader.GetString (1),
                            designation: reader.GetString (2));

                        if (reader.GetInt32 (0) == 0)
                            MeasurementUnit.Default = newItem;

                        MeasurementUnits.Add (newItem);
                    }
                }
            }

            #endregion

            #region Статьи бюджета
            BudgetItems = new List<BudgetItem> ( );
            using (SQLiteCommand command = new SQLiteCommand (@"SELECT * FROM budget_item ORDER BY id", _DBconnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader ( ))
                {
                    while (reader.Read ( ))
                    {
                        BudgetItem newItem = new BudgetItem (
                            name:               reader.GetString (1),
                            measurementUnit:    MeasurementUnits[reader.GetInt32 (2)]);

                        if (reader.GetInt32 (0) == 0)
                            BudgetItem.Default = newItem;

                        BudgetItems.Add (newItem);
                    }
                }
            }
            #endregion

            #region ЦФО
            ResponsibilityCenters = new List<ResponsibilityCenter> ( );
            using (SQLiteCommand command = new SQLiteCommand (@"SELECT * FROM responsibility_center ORDER BY id", _DBconnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader ( ))
                {
                    while (reader.Read ( ))
                    {
                        ResponsibilityCenter newItem = new ResponsibilityCenter (
                            name: reader.GetString (1));

                        if (reader.GetInt32 (0) == 0)
                            ResponsibilityCenter.Default = newItem;

                        ResponsibilityCenters.Add (newItem);
                    }
                }
            }
            #endregion

            #region Сценарии
            Scenarios = new List<Scenario> ( );
            using (SQLiteCommand command = new SQLiteCommand (@"SELECT * FROM scenario ORDER BY id", _DBconnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader ( ))
                {
                    while (reader.Read ( ))
                    {
                        Scenario newItem = new Scenario (
                            name: reader.GetString (1));

                        if (reader.GetInt32 (0) == 0)
                            Scenario.Default = newItem;

                        Scenarios.Add (newItem);
                    }
                }
            }
            #endregion

            #region Плановые операции
            PlannedOperations = new List<PlannedOperation> ( );

            using (SQLiteCommand command = new SQLiteCommand (@"SELECT * FROM planned_operation ORDER BY id", _DBconnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader ( ))
                {
                    while (reader.Read ( ))
                    {
                        IList<PlannedOperationScenario> plannedOperationScenarios = new List<PlannedOperationScenario> ( );

                        using (SQLiteCommand scenariosCommand = new SQLiteCommand (@"SELECT * FROM planned_operation_scenario WHERE planned_operation_id = " + reader.GetInt32 (0), _DBconnection))
                        {
                            using (SQLiteDataReader scenariosReader = scenariosCommand.ExecuteReader ( ))
                            {
                                while (scenariosReader.Read ( ))
                                {
                                    PlannedOperationScenario plannedOperationScenario = new PlannedOperationScenario (
                                        scenario:           Scenarios[scenariosReader.GetInt32 (1)],
                                        value:              scenariosReader.GetInt32 (2),
                                        labourIntensity:    scenariosReader.GetInt32 (3));

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
            ActualOperations = new List<ActualOperation> ( );
            using (SQLiteCommand command = new SQLiteCommand (@"SELECT * FROM actual_operation ORDER BY id", _DBconnection))
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

            _DBconnection.Close ( );

            return new Configuration (Users, MeasurementUnits, BudgetItems, ResponsibilityCenters, Scenarios, PlannedOperations, ActualOperations);
        }
    }
}
