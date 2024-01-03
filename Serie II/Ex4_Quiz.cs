using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_II
{
    public struct Qcm
    {
        public string Question { get; set; }
        public string[] Answers { get; set; }
        public int Solution { get; set; }
        public int Weight { get; set; }
    }

    public static class Quiz
    {
        public static void AskQuestions(Qcm[] qcms)
        {
            int totalScore = 0;

            for (int i = 0; i < qcms.Length; i++)
            {
                Console.WriteLine($"Question {i + 1}: {qcms[i].Question}");
                for (int j = 0; j < qcms[i].Answers.Length; j++)
                {
                    Console.WriteLine($"{j + 1}. {qcms[i].Answers[j]}");
                }

                int userAnswer = AskQuestion(qcms[i]);

                if (QcmValidity(qcms[i], userAnswer))
                {
                    totalScore += qcms[i].Weight;
                    Console.WriteLine("Correct!\n");
                }
                else
                {
                    totalScore += 0;  // or totalScore += qcms[i].Weight for negative weights
                    Console.WriteLine("Incorrect!\n");
                }
            }

            Console.WriteLine($"Total Score: {totalScore}");
        }

        public static int AskQuestion(Qcm qcm)
        {
            Console.Write("Your answer: ");
            int userAnswer;
            while (!int.TryParse(Console.ReadLine(), out userAnswer) || userAnswer < 1 || userAnswer > qcm.Answers.Length)
            {
                Console.WriteLine("Invalid input. Please enter a valid option.");
                Console.Write("Your answer: ");
            }

            return userAnswer;
        }

        public static bool QcmValidity(Qcm qcm, int userAnswer)
        {
            return qcm.Solution + 1 == userAnswer;
        }
    }
}
