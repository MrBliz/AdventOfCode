using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2020
{
    public class Day2
    {
        public Day2()
        {

            int validPart1Passwords = 0;
            int validPart2Passwords = 0;
            foreach (var line in File.ReadAllLines("2020/Data/day2.txt"))
            {
                var record = BuildLine(line);
                
                if(IsValidForPartOneRules(record.lower, record.upper, record.character, record.password)) validPart1Passwords++;
               
                if(IsValidForPartTwoRules(record.lower, record.upper, record.character, record.password)) validPart2Passwords++;
            }
            
            Console.WriteLine($"{validPart1Passwords} Valid Part 1 Passwords");
            Console.WriteLine($"{validPart2Passwords} Valid Part 2 Passwords");
        }

        private static (int lower, int upper, char character, string password) BuildLine(string line)
        {
            var range = line.Split(':').First();
            var trimmed = range.Substring(0, range.Length - 2);
            var numbers = trimmed.Split('-');
            
            int lower = Convert.ToInt16(numbers.First());
            int upper = Convert.ToInt16(numbers.Last());
            char character = Convert.ToChar(range.Last());
            string password = line.Split(':').Last().Trim();
            return (lower, upper, character, password);
        }

        private bool IsValidForPartOneRules(int lower, int upper, char requiredCharacter, string password)
        {
            var letterCount = 0;
            
            password.ToList().ForEach(x =>
            {
                if (x == requiredCharacter)
                {
                    letterCount++;
                }
            });
            
            return InRange(lower, upper, letterCount);
        }
        
        private bool IsValidForPartTwoRules(int lower, int upper, char requiredCharacter, string password)
        {
            if (password[lower - 1] == requiredCharacter && password[upper - 1] == requiredCharacter) return false;
            return password[lower - 1] == requiredCharacter || password[upper - 1] == requiredCharacter;
        }

        private bool InRange(int lower, int upper, int count)
        {
            return count >= lower && count <= upper;
        }
    }
}