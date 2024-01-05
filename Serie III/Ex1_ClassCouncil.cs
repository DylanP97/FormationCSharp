using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
public class ClassCouncil
{
    public static void SchoolMeans(string input, string output)
    {
        // Log the content of the input file to the console
        Console.WriteLine($"Notes des élèves dans le fichier class.csv :");        
        foreach (var line in File.ReadAllLines(input))
        {
            Console.WriteLine(line);
        }

        Console.WriteLine();

        // Log the content of the output file to the console
        Console.WriteLine($"Résultat avant traitement dans le fichier result.csv :");
        foreach (var line in File.ReadAllLines(output))
        {
            Console.WriteLine(line);
        }

        // Charger les données du fichier class.csv
        var studentData = File.ReadAllLines(input)
            .Skip(1) // Skip the header
            .Select(line => line.Split(','))
            .Select(parts => new
            {
                Nom = parts[0].Trim(),
                Matiere = parts[1].Trim(),
                Note = double.Parse(parts[2].Trim(), CultureInfo.InvariantCulture)
            });

        // Calculer la moyenne par matière
        var averageBySubject = studentData
            .GroupBy(student => student.Matiere)
            .Select(group => new
            {
                Matiere = group.Key,
                Moyenne = group.Average(student => student.Note)
            });

        // Charger les données existantes du fichier result.csv
        List<string> resultLines = new List<string>();
        if (File.Exists(output))
        {
            resultLines = File.ReadAllLines(output).ToList();
        }

        // Mettre à jour ou ajouter les moyennes dans le fichier result.csv
        foreach (var average in averageBySubject)
        {
            string lineToUpdate = resultLines.Find(line => line.StartsWith(average.Matiere + ","));

            if (lineToUpdate != null)
            {
                // Mettre à jour la note moyenne si la matière existe déjà dans le fichier result.csv
                int index = resultLines.IndexOf(lineToUpdate);
                resultLines[index] = $"{average.Matiere},{average.Moyenne.ToString(CultureInfo.InvariantCulture)}";
            }
            else
            {
                // Ajouter une nouvelle ligne si la matière n'existe pas encore dans le fichier result.csv
                resultLines.Add($"{average.Matiere},{average.Moyenne.ToString(CultureInfo.InvariantCulture)}");
            }
        }

        // Écrire les résultats dans le fichier result.csv
        File.WriteAllLines(output, resultLines);

        // Afficher le résultat final dans la console
        Console.WriteLine();
        Console.WriteLine("Résultat final dans le fichier result.csv :");
        foreach (var line in File.ReadAllLines(output))
        {
            Console.WriteLine(line);
        }
    }
}
