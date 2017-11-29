using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace PlanFactAnalysis.Model
{
    public sealed class FileManager
    {
        SQLiteConnection _connection;

        public SQLiteConnection EstablishConnection (string fileName)
        {
            if (File.Exists (Path.GetFullPath (fileName)))
            {
                _connection = new SQLiteConnection ("Data Source=" + fileName + ";Version=3;");

                return _connection;
            }

            return null;
        }

        public Configuration GetConfiguration ( )
        {
            #region Пользователи
            IList<User> users = new List<User> ( )
            {
                new User("Планировщик", "planner", "1", UserRole.Planner),
                new User("Исполнитель", "executor", "2", UserRole.Executor),
                new User("Руководитель", "manager", "3", UserRole.Manager)
            };
            #endregion

            #region Единицы измерения
            IList<MeasurementUnit> measurementUnits = new List<MeasurementUnit> ( )
            {
                new MeasurementUnit("Рубли", "руб."),
                new MeasurementUnit("Штуки", "шт."),
                new MeasurementUnit("Литры", "л."),
                new MeasurementUnit("Тонны", "т."),
            };
            #endregion

            #region Статьи бюджета
            IList<BudgetItem> budgetItems = new List<BudgetItem> ( )
            {
                new BudgetItem("Доходы от реализации продукции", measurementUnits[0]),
                new BudgetItem("Доходы от прочей деятельности", measurementUnits[0]),
                new BudgetItem("Прочее", measurementUnits[2]),
                new BudgetItem("Производство материала", measurementUnits[1]),
                new BudgetItem("Производство материала", measurementUnits[2]),
                new BudgetItem("Производство материала", measurementUnits[3]),
                new BudgetItem("Производственные расходы", measurementUnits[0]),
                new BudgetItem("Коммерческие расходы", measurementUnits[0]),
                new BudgetItem("Административные расходы", measurementUnits[0]),
                new BudgetItem("Налоги", measurementUnits[0])
            };
            #endregion

            #region ЦФО
            IList<ResponsibilityCenter> responsibilityCenters = new List<ResponsibilityCenter> ( )
            {
                new ResponsibilityCenter("Центр затрат"),
                new ResponsibilityCenter("Центр дохода"),
                new ResponsibilityCenter("Центр валового дохода"),
                new ResponsibilityCenter("Центр прибыли"),
                new ResponsibilityCenter("Центр рентабельности инвестиций")
            };
            #endregion

            #region Сценарии
            IList<Scenario> scenarios = new List<Scenario> ( )
            {
                new Scenario("Пессимистичный"),
                new Scenario("Реалистичный"),
                new Scenario("Оптимистичный"),
            };
            #endregion

            #region Плановые операции
            IList<PlannedOperationCore> plannedOperations = new List<PlannedOperationCore> ( )
            {
                new PlannedOperationCore ("Пример операции #1", new DateTime(2016, 11, 27, 00, 00, 00), new DateTime(2017, 11, 27, 02, 05, 35),
                    budgetItems[0], responsibilityCenters[1],
                    scenarios[1], 200000),

                new PlannedOperationCore ("Пример операции #2", new DateTime(2016, 12, 11, 00, 00, 00), new DateTime(2017, 11, 30, 02, 05, 35),
                    budgetItems[2], responsibilityCenters[3],
                    scenarios[1], 400000),

                new PlannedOperationCore ("Пример операции #3", new DateTime(2016, 10, 09, 00, 00, 00), new DateTime(2017, 06, 20, 02, 05, 35),
                    budgetItems[1], responsibilityCenters[1], 
                    scenarios[1], 60000),

                new PlannedOperationCore ("Пример операции #4", new DateTime(2016, 05, 12, 00, 00, 00), new DateTime(2017, 10, 21, 02, 05, 35),
                    budgetItems[0], responsibilityCenters[0],
                    scenarios[1], 217000),
            };

            plannedOperations[2].Scenarios.Add (new PlannedOperationScenario (scenarios[0], 55000));
            plannedOperations[2].Scenarios.Add (new PlannedOperationScenario (scenarios[2], 70000));
            #endregion

            #region Фактические операции
            IList<ActualOperation> actualOperations = new List<ActualOperation> ( )
            {
                new ActualOperation("Фактическая операция #1.1", new DateTime(2016, 12, 01, 00, 00, 00), plannedOperations[0], 70000),
                new ActualOperation("Фактическая операция #1.2", new DateTime(2017, 01, 01, 00, 00, 00), plannedOperations[0], 70000),
                new ActualOperation("Фактическая операция #1.3", new DateTime(2017, 02, 01, 00, 00, 00), plannedOperations[0], 70000),
                
                new ActualOperation("Фактическая операция #2", new DateTime(2017, 05, 01, 00, 00, 00), plannedOperations[1], 412000),

                new ActualOperation("Фактическая операция #3", new DateTime(2017, 06, 01, 00, 00, 00), plannedOperations[2], 50000),

                new ActualOperation("Фактическая операция #4", new DateTime(2017, 04, 01, 00, 00, 00), plannedOperations[3], 228000),
            };
            #endregion

            return new Configuration (users, measurementUnits, budgetItems, responsibilityCenters, scenarios, plannedOperations, actualOperations);
        }
    }
}
