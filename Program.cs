using System;
using System.Collections.Generic;

namespace MilitaryStrategyBuilder
{
    // -------------------------
    // PRODUCT
    // -------------------------
    public class StrategyScenario
    {
        public List<string> Troops { get; set; } = new List<string>();
        public List<string> Resources { get; set; } = new List<string>();
        public string Map { get; set; }

        public override string ToString()
        {
            return
                "===== СЦЕНАРІЙ ВІЙСЬКОВОЇ СТРАТЕГІЇ =====\n" +
                $"Карта: {Map}\n" +
                $"Війська: {string.Join(", ", Troops)}\n" +
                $"Ресурси: {string.Join(", ", Resources)}\n";
        }
    }

    // -------------------------
    // BUILDER INTERFACE
    // -------------------------
    public interface IScenarioBuilder
    {
        void Reset();
        void SetMap(string map);
        void AddTroop(string troop);
        void AddResource(string resource);
        StrategyScenario GetResult();
    }

    // -------------------------
    // CONCRETE BUILDER
    // -------------------------
    public class ScenarioBuilder : IScenarioBuilder
    {
        private StrategyScenario _scenario = new StrategyScenario();

        public void Reset()
        {
            _scenario = new StrategyScenario();
        }

        public void SetMap(string map)
        {
            _scenario.Map = map;
        }

        public void AddTroop(string troop)
        {
            _scenario.Troops.Add(troop);
        }

        public void AddResource(string resource)
        {
            _scenario.Resources.Add(resource);
        }

        public StrategyScenario GetResult()
        {
            return _scenario;
        }
    }

    // -------------------------
    // DIRECTOR
    // -------------------------
    public class ScenarioDirector
    {
        private readonly IScenarioBuilder _builder;

        public ScenarioDirector(IScenarioBuilder builder)
        {
            _builder = builder;
        }

        public StrategyScenario BuildBasicScenario()
        {
            _builder.Reset();
            _builder.SetMap("Пустеля");
            _builder.AddTroop("Піхота");
            _builder.AddResource("Їжа");
            return _builder.GetResult();
        }

        public StrategyScenario BuildAdvancedScenario()
        {
            _builder.Reset();
            _builder.SetMap("Гори");
            _builder.AddTroop("Лучники");
            _builder.AddTroop("Кавалерія");
            _builder.AddResource("Золото");
            _builder.AddResource("Медицина");
            return _builder.GetResult();
        }

        public StrategyScenario BuildCustomScenario()
        {
            _builder.Reset();
            _builder.SetMap("Ліс");
            _builder.AddTroop("Спеціальний загін");
            _builder.AddTroop("Снайпери");
            _builder.AddResource("Боєприпаси");
            _builder.AddResource("Паливо");
            return _builder.GetResult();
        }
    }

    // -------------------------
    // PROGRAM (MAIN)
    // -------------------------
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new ScenarioBuilder();
            var director = new ScenarioDirector(builder);

            Console.WriteLine("=== БАЗОВИЙ СЦЕНАРІЙ ===");
            var basic = director.BuildBasicScenario();
            Console.WriteLine(basic);

            Console.WriteLine("=== ПРОСУНУТИЙ СЦЕНАРІЙ ===");
            var advanced = director.BuildAdvancedScenario();
            Console.WriteLine(advanced);

            Console.WriteLine("=== КАСТОМНИЙ СЦЕНАРІЙ ===");
            var custom = director.BuildCustomScenario();
            Console.WriteLine(custom);

            // Можна вручну:
            builder.Reset();
            builder.SetMap("Острів");
            builder.AddTroop("Морська піхота");
            builder.AddResource("Вода");
            builder.AddResource("Паливо");

            Console.WriteLine("=== РУЧНИЙ СЦЕНАРІЙ ===");
            Console.WriteLine(builder.GetResult());
        }
    }
}
