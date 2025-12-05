using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DocumentationBuilderPattern
{
    // ============================
    // PRODUCT (НЕЗМІННИЙ)
    // ============================
    public class Document
    {
        private readonly List<string> _sections = new();
        private readonly List<string> _footnotes = new();

        public string Title { get; }
        public ReadOnlyCollection<string> Sections => _sections.AsReadOnly();
        public ReadOnlyCollection<string> Footnotes => _footnotes.AsReadOnly();

        public Document(string title, IEnumerable<string> sections, IEnumerable<string> footnotes)
        {
            Title = title;
            _sections.AddRange(sections);
            _footnotes.AddRange(footnotes);
        }

        public override string ToString()
        {
            string output = $"==== ДОКУМЕНТ ====\nЗаголовок: {Title}\n\n--- Секції ---\n";
            foreach (var s in Sections)
                output += "• " + s + "\n";

            output += "\n--- Виноски ---\n";
            foreach (var f in Footnotes)
                output += "* " + f + "\n";

            return output;
        }
    }

    // ============================
    // BUILDER
    // ============================
    public interface IDocumentBuilder
    {
        void Reset();
        void SetTitle(string title);
        void AddSection(string text);
        void AddFootnote(string note);
        Document GetResult();
    }

    // ============================
    // CONCRETE BUILDER
    // ============================
    public class DocumentationBuilder : IDocumentBuilder
    {
        private string _title;
        private readonly List<string> _sections = new();
        private readonly List<string> _footnotes = new();

        public void Reset()
        {
            _title = "";
            _sections.Clear();
            _footnotes.Clear();
        }

        public void SetTitle(string title)
        {
            _title = title;
        }

        public void AddSection(string text)
        {
            _sections.Add(text);
        }

        public void AddFootnote(string note)
        {
            _footnotes.Add(note);
        }

        public Document GetResult()
        {
            // повертаємо копії колекцій → інкапсуляція
            return new Document(_title, new List<string>(_sections), new List<string>(_footnotes));
        }
    }

    // ============================
    // DIRECTOR
    // ============================
    public class DocumentDirector
    {
        private readonly IDocumentBuilder _builder;

        public DocumentDirector(IDocumentBuilder builder)
        {
            _builder = builder;
        }

        public Document BuildShortManual()
        {
            _builder.Reset();
            _builder.SetTitle("Короткий мануал користувача");
            _builder.AddSection("Вступ: призначення системи.");
            _builder.AddSection("Основні функції та можливості.");
            _builder.AddFootnote("Версія документа: 1.0");
            return _builder.GetResult();
        }

        public Document BuildTechnicalSpecification()
        {
            _builder.Reset();
            _builder.SetTitle("Технічна специфікація");
            _builder.AddSection("Опис архітектури.");
            _builder.AddSection("Вимоги до обладнання.");
            _builder.AddSection("Протоколи взаємодії.");
            _builder.AddFootnote("Документ створено автоматично.");
            return _builder.GetResult();
        }
    }

    // ============================
    // MAIN
    // ============================
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = new DocumentationBuilder();
            var director = new DocumentDirector(builder);

            Console.WriteLine("=== КОРОТКИЙ МАНУАЛ ===");
            Console.WriteLine(director.BuildShortManual());

            Console.WriteLine("=== ТЕХНІЧНА СПЕЦИФІКАЦІЯ ===");
            Console.WriteLine(director.BuildTechnicalSpecification());

            // Створення документа вручну
            builder.Reset();
            builder.SetTitle("Користувацький документ");
            builder.AddSection("Це перша секція.");
            builder.AddSection("Це друга секція.");
            builder.AddFootnote("Це виноска користувача.");

            Console.WriteLine("=== РУЧНИЙ ДОКУМЕНТ ===");
            Console.WriteLine(builder.GetResult());
        }
    }
}
